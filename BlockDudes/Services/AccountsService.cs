using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using System.Threading.Tasks;

namespace BlockDudes.Services
{
    public class AccountsService: IAccountsService
    {
        private readonly IWeb3ProviderService _web3ProviderService;

        public AccountsService(IWeb3ProviderService web3ProviderService)
        {
            _web3ProviderService = web3ProviderService;
        }

        public decimal GetAccountEtherBalanceAsync(string privateKey)
        {
            var account = new Account(privateKey);
            var web3 = _web3ProviderService.GetWeb3(account);
            var balance = web3.Eth.GetBalance.SendRequestAsync(account.Address).Result;
            return Web3.Convert.FromWei(balance.Value);
        }
    }
}
