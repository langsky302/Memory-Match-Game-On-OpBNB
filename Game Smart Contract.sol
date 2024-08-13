// SPDX-License-Identifier: MIT
pragma solidity ^0.8.17;

contract opBNBGame {
    mapping(address => bool) private opBNBKey;
    mapping(address => uint256) private opBNBToken;
    mapping(address => uint256) private doubleTime;

    // Events
    event opBNBKeyGiven(address indexed player);
    event opBNBTokenClaimed(address indexed player, uint256 newCount);
    event doubleTimeClaimed(address indexed player, uint256 newClaim);

    function hasOpBNBKey(address player) external view returns (bool) {
        return opBNBKey[player];
    }

    function claimOpBNBKey(address player) external {
        opBNBKey[player] = true;
        emit opBNBKeyGiven(player);
    }

    function getOpBNBToken(address player) external view returns (uint256) {
        return opBNBToken[player];
    }

    function giveOpBNBToken(address player) external {
        opBNBToken[player] += 1;
        emit opBNBTokenClaimed(player, opBNBToken[player]);
    }

    function getDoubleTime(address player) external view returns (uint256) {
        return doubleTime[player];
    }

    function giveDoubleTime(address player) external {
        doubleTime[player] += 1;
        emit doubleTimeClaimed(player, doubleTime[player]);
    }
}
