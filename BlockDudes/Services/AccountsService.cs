using BlockDudes.Models;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;
using Nethereum.Hex.HexTypes;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace BlockDudes.Services
{

    public class AccountsService : IAccountsService
    {
        private readonly IWeb3ProviderService _web3ProviderService;

        private AccountInfo _account { get; set; }
        private ConcurrentDictionary<string, string> _privateKeyStorage;


        public AccountsService(IWeb3ProviderService web3ProviderService)
        {
            _web3ProviderService = web3ProviderService;

            _privateKeyStorage = new ConcurrentDictionary<string, string>();
        }

        public void AddAccount(string privateKey)
        {
            var account = new Account(privateKey);
            var address = account.Address.ToLowerInvariant();
            _account = new AccountInfo { Address = address };
            _privateKeyStorage.TryAdd(address, privateKey);
        }

        public async Task<decimal> GetAccountEtherBalanceAsync()
        {
            var web3 = _web3ProviderService.GetWeb3();
            var balance = await web3.Eth.GetBalance.SendRequestAsync(_account.Address).ConfigureAwait(false);
            return Web3.Convert.FromWei(balance.Value);
        }

        public async Task<decimal> GetAccountTokenBalanceAsync(string address, string contractAddress, int numberOfDecimals = 18)
        {
            var web3 = _web3ProviderService.GetWeb3();
            var balance = await web3.Eth.GetContractQueryHandler<BalanceOfFunction>()
                .QueryAsync<BigInteger>(contractAddress, new BalanceOfFunction() { Owner = address }).ConfigureAwait(false);
            return Web3.Convert.FromWei(balance, numberOfDecimals);
        }

        public List<TransactionInfo> GetCurrentChainPendingTransactions()
        {
            return _account.GetPendingTransactions(_web3ProviderService.ChainId).ToList();
        }

        public async Task<string> SendTransactionAsync(TransactionInput transactionInput)
        {
            if (_account != null)
            {
                var hasPrivateKey = _privateKeyStorage.TryGetValue(transactionInput.From.ToLowerInvariant(), out var privateKey);
                if (hasPrivateKey)
                {

                    var web3 = _web3ProviderService.GetWeb3(new Account(privateKey));
                    var txnHash = await web3.Eth.TransactionManager.SendTransactionAsync(transactionInput).ConfigureAwait(false);
                    _account.AddOrUpdateTransaction(new TransactionInfo()
                    {
                        AccountAddress = _account.Address,
                        ChainId = _web3ProviderService.ChainId,
                        TransactionHash = txnHash,
                        Pending = true
                    });
                    return txnHash;
                }
            }

            throw new ArgumentException($@"Account address: {transactionInput.From}, not found", nameof(transactionInput));
        }

        public async Task<string> SendTransactionAsync<TFunctionMessage>(string contractAddress, TFunctionMessage functionMessage) where TFunctionMessage : FunctionMessage, new()
        {
            if (_account != null)
            {
                var hasPrivateKey = _privateKeyStorage.TryGetValue(functionMessage.FromAddress.ToLowerInvariant(), out var privateKey);
                if (hasPrivateKey)
                {

                    var web3 = _web3ProviderService.GetWeb3(new Account(privateKey));
                    var txnHash = await web3.Eth.GetContractTransactionHandler<TFunctionMessage>()
                        .SendRequestAsync(contractAddress, functionMessage).ConfigureAwait(false);
                    _account.AddOrUpdateTransaction(new TransactionInfo()
                    {
                        AccountAddress = _account.Address,
                        ChainId = _web3ProviderService.ChainId,
                        TransactionHash = txnHash,
                        Pending = true
                    });
                    return txnHash;
                }
            }
            throw new ArgumentException($@"Account address: {functionMessage.FromAddress}, not found", nameof(functionMessage));
        }

        public AccountInfo GetCurrentAccount()
        {
            return _account;
        }

        public async Task UploadAssetAsync(string fileHash, string descriptionHash)
        {
            const string contractAddress = "0x6fBb44Fc86BA6c6D2D63D44794e6F1B1da3B1894";    //todo

            var web3 = _web3ProviderService.GetWeb3();
            var address = _account.Address;

            var transferHandler = web3.Eth.GetContractTransactionHandler<UploadFunction>();

            var upload = new UploadFunction
            {
                To = address,
                FileHash = new HexBigInteger(fileHash),
                DescriptionHash = new HexBigInteger(descriptionHash),
            };

            await transferHandler.SendRequestAndWaitForReceiptAsync(contractAddress, upload);
        }

        public async Task ExchangeAssetsAsync(string accountAddress, int tokenId1, int tokenId2)
        {
            const string contractAddress = "0x6fBb44Fc86BA6c6D2D63D44794e6F1B1da3B1894";    //todo

            var web3 = _web3ProviderService.GetWeb3();
            var address = _account.Address;

            var exchangeHandler = web3.Eth.GetContractTransactionHandler<ExchangeFunction>();

            var exchange = new ExchangeFunction
            {
                User1 = address,
                User2 = accountAddress,
                TokenId1 = tokenId1,
                TokenId2 = tokenId2
            };

            await exchangeHandler.SendRequestAndWaitForReceiptAsync(contractAddress, exchange);
        }
        public Task<int> GetAllItemsLength()
        {
            throw new NotImplementedException();
        }

        public Task<Asset> GetItem(int index)
        {
            throw new NotImplementedException();
        }
    }

    public partial class BalanceOfFunction : BalanceOfFunctionBase { }

    [Function("balanceOf", "uint256")]
    public class BalanceOfFunctionBase : FunctionMessage
    {
        [Parameter("address", "_owner", 1)]
        public virtual string Owner { get; set; }
    }


    public partial class TransferFunction : TransferFunctionBase { }

    [Function("transfer", "bool")]
    public class TransferFunctionBase : FunctionMessage
    {
        [Parameter("address", "_to", 1)]
        public virtual string To { get; set; }
        [Parameter("uint256", "_value", 2)]
        public virtual BigInteger Value { get; set; }
    }
}
