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

        public async Task<string> AddAsync(string hash, byte[] input)
        {
            Stream stream = new MemoryStream(input);
            var fsn = await _ipfsClient.FileSystem.AddAsync(stream);
            return hash;
        }

        public async Task<byte[]> GetAsync(string hash)
        {
            using (var stream = await _ipfsClient.FileSystem.GetAsync(hash))
            {
                byte[] buffer = new byte[16 * 1024];
                using (var ms = new MemoryStream())
                {
                    int read;
                    while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        ms.Write(buffer, 0, read);
                    }
                    return ms.ToArray();
                }
            }
        }
    }
}
