using BlockDudes.Models;
using Nethereum.Contracts;
using Nethereum.RPC.Eth.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlockDudes.Services
{
    public interface IAccountsService
    {
        Task<decimal> GetAccountEtherBalanceAsync();
        void AddAccount(string privateKey);
        AccountInfo GetCurrentAccount();
        Task<string> SendTransactionAsync(TransactionInput transactionInput);
        Task<string> SendTransactionAsync<TFunctionMessage>(string contractAddress, TFunctionMessage functionMessage) where TFunctionMessage : FunctionMessage, new();
        List<TransactionInfo> GetCurrentChainPendingTransactions();
        Task<decimal> GetAccountTokenBalanceAsync(string address, string contractAddress, int numberOfDecimals = 18);
    }
}
