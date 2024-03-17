using RokkitBank.Contracts.Exceptions;

namespace RokkitBank.Contracts.Entities
{
    public class SavingsAccount : Account
    {
        public SavingsAccount(long CustomerNum, long CurrentBalance, long MinimumBalance)
            : base(CustomerNum, CurrentBalance, MinimumBalance)
        {
            this.Type = AccountType.Savings;
        }
    }
}
