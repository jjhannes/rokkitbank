﻿
using RokkitBank.DB;
using RokkitBank.DB.Entities;
using RokkitBank.Domain.Contracts;
using RokkitBank.Domain.Exceptions;

namespace RokkitBank.Domain
{
    public class DefaultAccountService : IAccountService
    {
        private readonly long _minimumSavingsAccountCreateDeposit = 1000;
        private readonly int _maximumCurrentAccountOverdraft = 100000;

        private long CalculateCustomerOverdraftLimit(long CustomerNum)
        {
            // Imagine some fancy-ass credit check business logic here
            return new Random().Next(1, this._maximumCurrentAccountOverdraft);
        }

        public bool OpenSavingsAccount(long CustomerNum, long AmountToDeposit)
        {
            if (AmountToDeposit < this._minimumSavingsAccountCreateDeposit)
            {
                throw new OpeningBalanceTooSmallException(this._minimumSavingsAccountCreateDeposit);
            }

            try
            {
                AccountRepo.OpenAccount(AccountType.Savings, CustomerNum, Balance: AmountToDeposit);

                return true;
            }
            catch (Exception)
            {
                // Log error

                return false;
            }
        }

        public bool OpenCurrentAccount(long CustomerNum)
        {
            try
            {
                long customerOverdraftLimit = this.CalculateCustomerOverdraftLimit(CustomerNum);
                AccountRepo.OpenAccount(AccountType.Current, CustomerNum, Overdraft: customerOverdraftLimit);

                return true;
            }
            catch (Exception)
            {
                // Log error

                return false;
            }
        }

        public long Deposit(long AccountId, long AmountToDeposit)
        {
            throw new NotImplementedException();
        }

        public long Withdraw(long AccountId, long AmountToWithdraw)
        {
            throw new NotImplementedException();
        }
    }
}
