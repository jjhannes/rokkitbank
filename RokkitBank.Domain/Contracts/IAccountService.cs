
using RokkitBank.DB.Entities;

namespace RokkitBank.Domain.Contracts
{
    public interface IAccountService
    {
        public Account OpenSavingsAccount(long CustomerNum, long AmountToDeposit);
        // throws OpeningBalanceTooSmall

        public Account OpenCurrentAccount(long CustomerNum);

        public Account Withdraw(long AccountId, long AmountToWithdraw);
        // throws AccountNotFoundException, WithdrawalAmountTooLargeException

        public Account Deposit(long AccountId, long AmountToDeposit);
        //throws AccountNotFoundException
    }
}
