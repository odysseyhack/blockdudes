using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using System.Threading.Tasks;

namespace BlockDudes.Services
{
    public interface IWeb3ProviderService
    {
        string CurrentUrl { get; set; }
        string ChainId { get; }
        Task<Web3> GetWeb3();
        Web3 GetWeb3(Account account);
    }
}
