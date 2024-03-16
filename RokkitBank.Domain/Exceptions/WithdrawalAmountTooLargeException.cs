using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RokkitBank.Domain.Exceptions
{
    public class WithdrawalAmountTooLargeException : Exception
    {
        internal WithdrawalAmountTooLargeException()
            : base($"Insufficient funds.")
        {
            
        }
    }
}
