using Ipfs.Http;
using System.IO;
using System.Threading.Tasks;

namespace BlockDudes.Services
{
    public class IpfsProviderService : IIpfsProviderService
    {
        private readonly IpfsClient _ipfsClient;

        public IpfsProviderService(IpfsClient ipfsClient)
        {
            _ipfsClient = ipfsClient;
        }

        public async Task<string> AddAsync(byte[] input)
        {
            var stream = new MemoryStream(input);
            var fsn = await _ipfsClient.FileSystem.AddAsync(stream).ConfigureAwait(false);
            return fsn.Id.Hash.ToString();
        }

        public async Task<string> AddTextAsync(string test)
        {
            var fsn = await _ipfsClient.FileSystem.AddTextAsync(test).ConfigureAwait(false);
            return fsn.Id.Hash.ToString();
        }

        public async Task<byte[]> GetAsync(string hash)
        {
            using (var stream = await _ipfsClient.FileSystem.ReadFileAsync(hash).ConfigureAwait(false))
            {
                using (var ms = new MemoryStream())
                {
                    stream.CopyTo(ms);
                    return ms.ToArray();
                }
            }
        }

        public async Task<string> GetTextAsync(string hash)
        {
            return await _ipfsClient.FileSystem.ReadAllTextAsync(hash).ConfigureAwait(false);
        }
    }
}
