using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;

namespace BlockDudes.Blockchain.Functions
{
    [Function("getAllItemsLength", "uint")]
    public class GetAllItemsLengthFunction : FunctionMessage
    {
    }
}
