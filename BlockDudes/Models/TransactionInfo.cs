﻿using Nethereum.RPC.Eth.DTOs;

namespace BlockDudes.Models
{
    public class TransactionInfo
    {
        public string AccountAddress { get; set; }
        public string ChainId { get; set; }
        public TransactionReceipt TransactionReceipt { get; set; }
        public string TransactionHash { get; set; }
        public bool Pending { get; set; }
    }
}
