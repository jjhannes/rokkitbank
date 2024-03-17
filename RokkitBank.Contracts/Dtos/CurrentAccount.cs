using RokkitBank.Contracts.Entities;
using RokkitBank.Contracts.Exceptions;

namespace RokkitBank.Contracts.Dtos
{
    public class CurrentAccount : Account
    {
        public CurrentAccount(long CustomerNum, long CurrentBalance, long MinimumBalance)
            : base(CustomerNum, CurrentBalance, MinimumBalance)
        {
            this.Type = AccountType.Current;
        }
    }
}
