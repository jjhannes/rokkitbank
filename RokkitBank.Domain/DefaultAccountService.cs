
using RokkitBank.DB;
using RokkitBank.DB.Entities;
using RokkitBank.Domain.Contracts;
using RokkitBank.Domain.Exceptions;

namespace RokkitBank.Domain
{
    public class DefaultAccountService : IAccountService
    {
        private readonly int _minimumSavingsAccountCreateDeposit = 1000;

        public bool OpenSavingsAccount(long CustomerNum, long AmountToDeposit)
        {
            if (AmountToDeposit < this._minimumSavingsAccountCreateDeposit)
            {
                throw new OpeningBalanceTooSmallException(this._minimumSavingsAccountCreateDeposit);
            }

            try
            {
                AccountRepo.OpenAccount(AccountType.Savings, CustomerNum, AmountToDeposit);

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
            throw new NotImplementedException();
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
