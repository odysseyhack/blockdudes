using System.Threading.Tasks;

namespace BlockDudes.Services
{
    public interface IIpfsProviderService
    {
        Task<string> AddAsync(byte[] input);
        Task<byte[]> GetAsync(string hash);
        Task<string> AddTextAsync(string test);
        Task<string> GetTextAsync(string hash);
    }
}
