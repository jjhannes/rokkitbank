
namespace RokkitBank.Domain.Contracts
{
    public interface IAccountService
    {
        public void OpenSavingsAccount(long CustomerNum, long AmountToDeposit);
        // throws OpeningBalanceTooSmall

        public void OpenCurrentAccount(long CustomerNum);

        public void Withdraw(long AccountId, long AmountToWithdraw);
        // throws AccountNotFoundException, WithdrawalAmountTooLargeException

        public void Deposit(long AccountId, long AmountToDeposit);
        //throws AccountNotFoundException
    }
}
