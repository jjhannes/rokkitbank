
using RokkitBank.DB;
using RokkitBank.DB.Entities;
using RokkitBank.Domain.Contracts;
using RokkitBank.Domain.Exceptions;

namespace RokkitBank.Domain
{
    public class DefaultAccountService : IAccountService
    {
        private readonly long _minimumSavingsAccountCreateDeposit = 1000;
        private readonly int _maximumCurrentAccountOverdraft = 100000;

        private long CalculateCustomerOverdraftLimit(long CustomerNum)
        {
            // Imagine some fancy-ass credit check business logic here
            return new Random().Next(1, this._maximumCurrentAccountOverdraft);
        }

        public bool OpenSavingsAccount(long CustomerNum, long AmountToDeposit)
        {
            if (AmountToDeposit < this._minimumSavingsAccountCreateDeposit)
            {
                throw new OpeningBalanceTooSmallException(this._minimumSavingsAccountCreateDeposit);
            }

            try
            {
                AccountRepo.OpenAccount(AccountType.Savings, CustomerNum, Balance: AmountToDeposit);

                return true;
            }
            catch (Exception)
            {
                // Log error

                return false;
            }
        }

        public bool OpenCurrentAccount(long CustomerNum)
        {
            try
            {
                long customerOverdraftLimit = this.CalculateCustomerOverdraftLimit(CustomerNum);
                AccountRepo.OpenAccount(AccountType.Current, CustomerNum, Overdraft: customerOverdraftLimit);

                return true;
            }
            catch (Exception)
            {
                // Log error

                return false;
            }
        }

        public long Deposit(long AccountId, long AmountToDeposit)
        {
            Account target = AccountRepo.GetAccount(AccountId);

            if (target == null)
            {
                throw new AccountNotFoundException();
            }

            return AccountRepo.Deposit(target, AmountToDeposit).Balance;
        }

        public long Withdraw(long AccountId, long AmountToWithdraw)
        {
            Account target = AccountRepo.GetAccount(AccountId);

            if (target == null)
            {
                throw new AccountNotFoundException();
            }

            if (target.Type == AccountType.Savings)
            {
                if (target.Balance >= AmountToWithdraw)
                {
                    AccountRepo.Withdraw();

                    return target.Balance;
                }
            }

            throw new WithdrawalAmountTooLargeException();
        }
    }
}
