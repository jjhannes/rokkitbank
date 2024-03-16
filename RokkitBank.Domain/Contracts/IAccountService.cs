
namespace RokkitBank.Domain.Contracts
{
    public interface IAccountService
    {
        public bool OpenSavingsAccount(long CustomerNum, long AmountToDeposit);
        // throws OpeningBalanceTooSmall

        public bool OpenCurrentAccount(long CustomerNum);

        public long Withdraw(long AccountId, long AmountToWithdraw);
        // throws AccountNotFoundException, WithdrawalAmountTooLargeException

        public long Deposit(long AccountId, long AmountToDeposit);
        //throws AccountNotFoundException
    }
}
