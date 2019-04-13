using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;

namespace BlockDudes.Services
{
    [Function("exchangeTokens")]
    public class ExchangeFunction : FunctionMessage
    {
        [Parameter("address", "_user1", 1)]
        public string User1 { get; set; }

        [Parameter("address", "_user2", 2)]
        public string User2 { get; set; }

        [Parameter("uint256", "_id1", 3)]
        public int TokenId1 { get; set; }

        [Parameter("uint256", "_id2", 4)]
        public int TokenId2 { get; set; }
    }
}
