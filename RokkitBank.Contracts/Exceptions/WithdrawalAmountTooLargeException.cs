
namespace RokkitBank.Contracts.Exceptions
{
    public class WithdrawalAmountTooLargeException : Exception
    {
        public WithdrawalAmountTooLargeException()
            : base($"Insufficient funds.")
        {
            
        }
    }
}
