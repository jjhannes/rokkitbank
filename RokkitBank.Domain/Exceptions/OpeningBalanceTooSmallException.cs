using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RokkitBank.Domain.Exceptions
{
    public class OpeningBalanceTooSmallException : Exception
    {
        private long _minimumOpeningBalance;

        internal OpeningBalanceTooSmallException(long minimumOpeningBalance)
            : base($"Expected minimum opening balance of {minimumOpeningBalance}.")
        {
            this._minimumOpeningBalance = minimumOpeningBalance;
        }
    }
}
