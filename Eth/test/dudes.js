const { shouldFail, expectEvent } = require('openzeppelin-test-helpers');
const { expect } = require('chai');

const dudes = artifacts.require('ERC721Dudes.sol');

let user0;
let user1;
let curveContract;
let mainContract;
let totalSupply;
let payoutInDays;
let publish;

contract('dudes', (accounts) => {
    before(async () => {
        user0 = accounts[0];
        user1 = accounts[1];
        mainContract = await dudes.new({ from: user0 });
    });

    describe('Non Fungible Token', function () {
        it('should mint NFT', async () => {
            // act
            const { logs } = await mainContract.mintWithTokenHash(
                user0,
                "fileHash",
                "descHash",
                { from: user0 });

            // assert
            expect(await mainContract.getAllItemsLength()).to.be.bignumber.equal('1');
        });

        it('should exchange NFT', async () => {
            // arrange
            await mainContract.mintWithTokenHash(
                user0,
                "fileHash",
                "descHash",
                { from: user0 });

                await mainContract.mintWithTokenHash(
                    user1,
                    "fileHash",
                    "descHash",
                    { from: user1 });

            // act
            const { logs } = await mainContract.exchangeTokens(
                user0,
                user1,
                1,
                2,
                { from: user0 });

            // assert
            expect(await mainContract.getItem(0).owner).equal(user0);
        });
    });
});