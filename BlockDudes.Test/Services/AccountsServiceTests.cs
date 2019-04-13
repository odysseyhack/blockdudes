using BlockDudes.Services;
using Moq;
using Nethereum.Contracts.Services;
using Nethereum.Hex.HexTypes;
using Nethereum.RPC.Eth;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using System.Numerics;
using System.Threading.Tasks;
using Xunit;

namespace BlockDudes.Test.Services
{
    public class AccountsServiceTests
    {
        private readonly Mock<IWeb3ProviderService> _webProviderService;
        private readonly Mock<IWeb3> _web3;
        private readonly AccountsService _accountsService;

        private readonly string _testPrivateKey = "2883119BA1063D34CDEC5E50A78D8859FD300B4D847EB19728921D34585C885B";

        public AccountsServiceTests()
        {
            _web3 = new Mock<IWeb3>();
            _webProviderService = new Mock<IWeb3ProviderService>();
            _webProviderService
                .Setup(s => s.GetWeb3(It.IsAny<Account>()))
                .Returns<IWeb3>(s => _web3.Object);
            _webProviderService
                .Setup(s => s.GetWeb3())
                .Returns(_web3.Object);
            _accountsService = new AccountsService(_webProviderService.Object);

            _accountsService.AddAccount(_testPrivateKey);
        }

        [Fact]
        public void GetAccountEtherBalanceAsync_ReturnBalance()
        {
            // arrange
            decimal expectedBalance = 0.000000000000000115M;
            var balance = new Mock<IEthGetBalance>();
            balance
                .Setup(b => b.SendRequestAsync(It.IsAny<string>(), It.IsAny<object>()))
                .Returns<string, bool>((d, c) => Task.FromResult(new HexBigInteger("0x73")));
            var eth = new Mock<IEthApiContractService>();
            eth.Setup(e => e.GetBalance).Returns(balance.Object);
            _web3.Setup(w => w.Eth).Returns(eth.Object);

            // act
            var actualBalance = _accountsService.GetAccountEtherBalanceAsync().GetAwaiter().GetResult();

            // assert
            Assert.Equal(expectedBalance, actualBalance);
        }
    }
}
