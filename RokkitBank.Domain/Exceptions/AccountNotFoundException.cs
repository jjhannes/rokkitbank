using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RokkitBank.Domain.Exceptions
{
    public class AccountNotFoundException : Exception
    {
        internal AccountNotFoundException()
            : base($"Account not found.")
        {
            
        }
    }
}
