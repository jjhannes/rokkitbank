
namespace RokkitBank.Domain
{
    public interface IAccountService
    {
        public void OpenSavingsAccount(long accountId, long amountToDeposit);

        public void OpenCurrentAccount(long accountId);

        public void Withdraw(long accountId, int amountToWithdraw);
        // throws AccountNotFoundException, WithdrawalAmountTooLargeException;

        public void Deposit(long accountId, int amountToDeposit);
        //throws AccountNotFoundException;
    }
}
