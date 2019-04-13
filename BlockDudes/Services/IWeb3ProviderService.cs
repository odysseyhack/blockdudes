using Nethereum.Web3;
using Nethereum.Web3.Accounts;

namespace BlockDudes.Services
{
    public interface IWeb3ProviderService
    {
        string CurrentUrl { get; set; }
        string ChainId { get; }
        IWeb3 GetWeb3();
        IWeb3 GetWeb3(Account account);
    }
}
