using Nethereum.ABI.FunctionEncoding.Attributes;

namespace BlockDudes.Services
{
    [FunctionOutput]
    public class Asset
    {
        [Parameter("uint256", "tokenId", 1)]
        public int TokenId { get; set; }

        [Parameter("bytes32", "fileHash", 2)]
        public byte[] FileHash { get; set; }

        [Parameter("bytes32", "descriptionHash", 3)]
        public byte[] DescriptionHash { get; set; }

        [Parameter("address", "owner", 4)]
        public string Owner { get; set; }
    }
}
