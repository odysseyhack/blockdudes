pragma solidity ^0.5.4;

import "openzeppelin-solidity/contracts/math/SafeMath.sol";
import "openzeppelin-solidity/contracts/utils/Address.sol";
import "openzeppelin-solidity/contracts/introspection/IERC165.sol";
import "openzeppelin-solidity/contracts/token/ERC721/ERC721.sol";
// import "./IERC721Receiver.sol";

// A sample implementation of core ERC1155 function.
contract ERC721Dudes is ERC721
{
    using SafeMath for uint256;
    using Address for address;

    bytes4 constant public ERC1155_RECEIVED = 0xf23a6e61;
    bytes4 constant public ERC1155_BATCH_RECEIVED = 0xbc197c81;

    // owner => (operator => approved)
    mapping (address => mapping(address => bool)) internal operatorApproval;

event TransferSingle(address indexed _operator, address indexed _from, address indexed _to, uint256 _id, uint256 _value);

/////////////////////////////////////////// ERC1155 //////////////////////////////////////////////
    /**
        @notice Transfers value amount of an _id from the _from address to the _to addresses specified. Each parameter array should be the same length, with each index correlating.
        @dev MUST emit TransferSingle event on success.
        Caller must be approved to manage the _from account's tokens (see isApprovedForAll).
        MUST Throw if `_to` is the zero address.
        MUST Throw if `_id` is not a valid token ID.
        MUST Throw on any other error.
        When transfer is complete, this function MUST check if `_to` is a smart contract (code size > 0). If so, it MUST call `onERC1155Received` on `_to`
        and revert if the return value is not `bytes4(keccak256("onERC1155Received(address,address,uint256,uint256,bytes)"))`.
        @param _from    Source addresses
        @param _to      Target addresses
        @param _id      ID of the token type
        @param _value   Transfer amount
        @param _data    Additional data with no specified format, sent in call to `_to`
    */
    // function safeTransferFrom(address _from, address _to, uint256 _id, uint256 _value, bytes calldata _data) external {

    //     require(_to != address(0x0), "_to must be non-zero.");
    //     require(_from == msg.sender || operatorApproval[_from][msg.sender] == true, "Need operator approval for 3rd party transfers.");


    //     emit TransferSingle(msg.sender, _from, _to, _id, _value);

    //     require(IERC721Receiver(_to).onERC721Received(msg.sender, _from, _id, _data) == ERC1155_RECEIVED, "Receiver contract did not accept the transfer.");
    // }

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

    function mintWithTokenHash(address to, uint256 tokenId) public returns (bool) {
        _mint(to, tokenId);
        return true;
    }
}