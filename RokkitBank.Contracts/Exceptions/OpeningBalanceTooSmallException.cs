
namespace RokkitBank.Contracts.Exceptions
{
    public class OpeningBalanceTooSmallException : Exception
    {
        private long _minimumOpeningBalance;

        public OpeningBalanceTooSmallException(long minimumOpeningBalance)
            : base($"Expected minimum opening balance of {minimumOpeningBalance}.")
        {
            this._minimumOpeningBalance = minimumOpeningBalance;
        }
    }
}
