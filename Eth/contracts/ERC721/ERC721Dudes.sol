pragma solidity ^0.5.4;

import "openzeppelin-solidity/contracts/math/SafeMath.sol";
import "openzeppelin-solidity/contracts/utils/Address.sol";
import "openzeppelin-solidity/contracts/introspection/IERC165.sol";
import "openzeppelin-solidity/contracts/token/ERC721/ERC721.sol";

// A sample implementation of core ERC1155 function.
contract ERC721Dudes is ERC721
{
    struct ItemStruct {
        uint256 tokenId;
        string fileHash;
        string descriptionHash;
        address owner;
    }

    using SafeMath for uint256;
    using Address for address;

    // owner => (operator => approved)
    mapping (address => mapping(address => bool)) internal operatorApproval;
    //mapping(address => UserStruct) ownedItems;
    mapping(uint256 => uint) itemIndexes;

    ItemStruct[] allItems;
    uint allItemsLength;
    uint256 nonse;

    function safeTransferFrom(address _from, address _to, uint256 _id) public {

        require(_to != address(0x0), "_to must be non-zero.");
        //require(_from == msg.sender || operatorApproval[_from][msg.sender] == true, "Need operator approval for 3rd party transfers.");

        
        uint index = itemIndexes[_id];

        allItems[index].owner = _to;
    }

    function exchangeTokens(address _user1, address _user2, uint256 _id1, uint256 _id2) public {

        safeTransferFrom(_user1, _user2, _id1);
        safeTransferFrom(_user2, _user1, _id2);
    }

    function getItem(uint index) public view returns (uint256, string memory, string memory, address) {
        return (allItems[index].tokenId, allItems[index].fileHash, allItems[index].descriptionHash, allItems[index].owner);
    }

    function getAllItemsLength() public view returns (uint) {
        return allItemsLength;
    }

    /**
        @notice Enable or disable approval for a third party ("operator") to manage all of the caller's tokens.
        @dev MUST emit the ApprovalForAll event on success.
        @param _operator  Address to add to the set of authorized operators
        @param _approved  True if the operator is approved, false to revoke approval
    */
    function setApprovalForAll(address _operator, bool _approved) public {
        operatorApproval[msg.sender][_operator] = _approved;
        emit ApprovalForAll(msg.sender, _operator, _approved);
    }

    /**
        @notice Queries the approval status of an operator for a given owner.
        @param _owner     The owner of the Tokens
        @param _operator  Address of authorized operator
        @return           True if the operator is approved, false if not
    */
    function isApprovedForAll(address _owner, address _operator) public view returns (bool) {
        return operatorApproval[_owner][_operator];
    }

    function mintWithTokenHash(address to, string memory fileHash, string memory descriptionHash) public returns (bool) {

        nonse++;

        uint256 tokenId = nonse;
        
        _mint(to, tokenId);

        ItemStruct memory item = ItemStruct(tokenId, fileHash, descriptionHash, to);

        allItems.push(item);
        itemIndexes[tokenId] = allItemsLength;
        allItemsLength++;

        return true;
    }

    // function deleteUserAsset(address user, uint256 tokenId) private {
    //     UserStruct storage u = ownedItems[user];
    //     uint rowToDelete = u.assetPointers[tokenId];
    //     u.assets[rowToDelete] = u.assets[u.assets.length-1];
    //     u.hashes[rowToDelete] = u.hashes[u.hashes.length-1];
    //     u.assets.length--;
    //     u.hashes.length--;
    // }

    // function addUserAsset(address user, uint256 tokenId, bytes32 value) private {
    //     ownedItems[user].assets.push(tokenId);
    //     ownedItems[user].hashes.push(value);
    //     ownedItems[user].assetPointers[tokenId] = ownedItems[user].assets.length - 1;
    // }
}