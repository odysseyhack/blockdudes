using BlockDudes.ViewModels;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;

namespace BlockDudes.Services
{
    public class Web3ProviderService : IWeb3ProviderService
    {
        public string CurrentUrl { get; set; } = "https://rinkeby.infura.io/v3/30aae6c5637740618f589120a97abeae";

        public string ChainId => CurrentUrl;

        public IWeb3 GetWeb3()
        {
            if (Utils.IsValidUrl(CurrentUrl))
            {
                return new Web3(CurrentUrl);
            }

            return null;
        }

        public IWeb3 GetWeb3(Account account)
        {
            if (Utils.IsValidUrl(CurrentUrl))
            {
                return new Web3(account, CurrentUrl);
            }

            return null;
        }
    }
}
