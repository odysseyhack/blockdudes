using Nethereum.Util;

namespace BlockDudes.Services
{
    public class HashService : IHashService
    {
        public byte[] GetHash(byte[] input)
        {
            return Sha3Keccack.Current.CalculateHash(input);
        }
    }
}
