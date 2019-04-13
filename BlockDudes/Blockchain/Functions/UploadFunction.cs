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

        [Parameter("string", "fileHash", 2)]
        public string FileHash { get; set; }

        [Parameter("string", "descriptionHash", 3)]
        public string DescriptionHash { get; set; }
    }
}
