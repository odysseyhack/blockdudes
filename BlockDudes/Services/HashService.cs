using Nethereum.Util;
using System.Text;

namespace BlockDudes.Services
{
    public class HashService : IHashService
    {
        public string GetHash(byte[] input)
        {
            var hash = Sha3Keccack.Current.CalculateHash(input);
            var hex = new StringBuilder(hash.Length * 2);
            foreach (byte b in hash)
            {
                hex.AppendFormat("{0:x2}", b);
            }
            return hex.ToString();
        }
    }
}
