
using RokkitBank.Domain.Contracts;
using RokkitBank.Domain.Exceptions;

namespace RokkitBank.Domain
{
    public class DefaultAccountService : IAccountService
    {
        private readonly int _minimumSavingsAccountCreateDeposit = 1000;

        public void OpenSavingsAccount(long AccountId, long AmountToDeposit)
        {
            if (AmountToDeposit < this._minimumSavingsAccountCreateDeposit)
            {
                throw new OpeningBalanceTooSmallException(this._minimumSavingsAccountCreateDeposit);
            }

            throw new NotImplementedException();
        }

        public void OpenCurrentAccount(long AccountId)
        {
            throw new NotImplementedException();
        }

        public void Deposit(long AccountId, int AmountToDeposit)
        {
            throw new NotImplementedException();
        }

        public void Withdraw(long AccountId, int AmountToWithdraw)
        {
            throw new NotImplementedException();
        }
    }
}
