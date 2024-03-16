
using RokkitBank.DB;
using RokkitBank.DB.Entities;
using RokkitBank.Domain.Contracts;
using RokkitBank.Domain.Exceptions;

namespace RokkitBank.Domain
{
    public class DefaultAccountService : IAccountService
    {
        private readonly int _minimumSavingsAccountCreateDeposit = 1000;

        public void OpenSavingsAccount(long CustomerNum, long AmountToDeposit)
        {
            if (AmountToDeposit < this._minimumSavingsAccountCreateDeposit)
            {
                throw new OpeningBalanceTooSmallException(this._minimumSavingsAccountCreateDeposit);
            }

            AccountRepo.OpenAccount(AccountType.Savings, CustomerNum, AmountToDeposit);
        }

        public void OpenCurrentAccount(long CustomerNum)
        {
            throw new NotImplementedException();
        }

        public void Deposit(long AccountId, long AmountToDeposit)
        {
            throw new NotImplementedException();
        }

        public void Withdraw(long AccountId, long AmountToWithdraw)
        {
            throw new NotImplementedException();
        }
    }
}
