using BlockDudes.ViewModels;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using System.Threading.Tasks;

namespace BlockDudes.Services
{
    public class Web3ProviderService : IWeb3ProviderService
    {
        public string CurrentUrl { get; set; } = "https://rinkeby.infura.io/v3/30aae6c5637740618f589120a97abeae";

        //TODO: Simple chainId workaround, this should be the ChainId from the connection, when adding the url we should get the chainId using rpc and add it here.
        public string ChainId => CurrentUrl;

        public IWeb3 GetWeb3()
        {
            if (Utils.IsValidUrl(CurrentUrl))
            {
                //var privateKey = "0x6F3CF419DFD949F29F47242FC10C87FDBD654E7C1FF0BE81DE1694D667C8A56F";
                //var ethereumAddress = "0xdE3DF391410A3A973790ad757eEBf0d6Fd0e811D";

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
