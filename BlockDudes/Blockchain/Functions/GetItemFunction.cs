using BlockDudes.Blockchain.Models;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;

namespace BlockDudes.Blockchain.Functions
{
    [Function("getItem", typeof(Asset))]
    public class GetItemFunction : FunctionMessage
    {
        [Parameter("uint", "index", 1)]
        public int Index { get; set; }
    }
}
