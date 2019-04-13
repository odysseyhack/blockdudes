using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;
using Nethereum.Hex.HexTypes;

namespace BlockDudes.Blockchain.Functions
{
    [Function("mintWithTokenHash", "bool")]
    public class UploadFunction : FunctionMessage
    {
        [Parameter("address", "to", 1)]
        public string To { get; set; }

        [Parameter("bytes32", "fileHash", 2)]
        public HexBigInteger FileHash { get; set; }

        [Parameter("bytes32", "descriptionHash", 3)]
        public HexBigInteger DescriptionHash { get; set; }
    }
}
