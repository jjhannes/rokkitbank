
namespace RokkitBank.Domain
{
    public interface IAccountService
    {
        public void OpenSavingsAccount(long AccountId, long AmountToDeposit);
        // throws OpeningBalanceTooSmall

        public void OpenCurrentAccount(long AccountId);

        public void Withdraw(long AccountId, int AmountToWithdraw);
        // throws AccountNotFoundException, WithdrawalAmountTooLargeException

        public void Deposit(long AccountId, int AmountToDeposit);
        //throws AccountNotFoundException
    }
}
