﻿
namespace RokkitBank.Contracts.Exceptions
{
    public class AccountNotFoundException : Exception
    {
        public AccountNotFoundException()
            : base($"Account not found.")
        {
            
        }
    }
}
