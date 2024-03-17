
using RokkitBank.Contracts.Exceptions;

namespace RokkitBank.Contracts.Entities
{
    public enum AccountType
    {
        Savings,
        Current
    }

    public abstract class Account
    {
        public long ID { get; set; }

        public AccountType Type { get; set; }

        public long CustomerNum { get; set; }

        public long CurrentBalance { get; set; } = 0;

        public long MinimumBalance { get; set; } = 0;

        public Account(long CustomerNum, long CurrentBalance, long MinimumBalance)
        {
            this.CustomerNum = CustomerNum;
            this.CurrentBalance = CurrentBalance;
            this.MinimumBalance = MinimumBalance;
        }

        public virtual Account Deposit(long Amount)
        {
            this.CurrentBalance += Amount;

            return this;
        }

        public virtual Account Withdraw(long Amount)
        {
            if (this.CurrentBalance - Amount > this.MinimumBalance)
            {
                this.CurrentBalance -= Amount;

                return this;
            }

            throw new WithdrawalAmountTooLargeException();
        }
    }
}
