using System.Threading.Tasks;

namespace BlockDudes.Services
{
    public interface IIpfsProviderService
    {
        Task<string> AddAsync(string hash, byte[] input);
        Task<byte[]> GetAsync(string hash);
    }
}
