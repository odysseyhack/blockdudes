using Nethereum.ABI.FunctionEncoding.Attributes;

namespace BlockDudes.Blockchain.Models
{
    [FunctionOutput]
    public class Asset
    {
        [Parameter("uint256", "tokenId", 1)]
        public int TokenId { get; set; }

        [Parameter("string", "fileHash", 2)]
        public string FileHash { get; set; }

        [Parameter("string", "descriptionHash", 3)]
        public string DescriptionHash { get; set; }

        [Parameter("address", "owner", 4)]
        public string Owner { get; set; }
    }
}
