
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

        public Account OpenSavingsAccount(long CustomerNum, long AmountToDeposit)
        {
            if (AmountToDeposit < this._minimumSavingsAccountCreateDeposit)
            {
                throw new OpeningBalanceTooSmallException(this._minimumSavingsAccountCreateDeposit);
            }

            try
            {
                return AccountRepo.OpenAccount(AccountType.Savings, CustomerNum, Balance: AmountToDeposit);
            }
            catch (Exception)
            {
                // Log error

                throw;
            }
        }

        public Account OpenCurrentAccount(long CustomerNum)
        {
            try
            {
                long customerOverdraftLimit = this.CalculateCustomerOverdraftLimit(CustomerNum);
                
                return AccountRepo.OpenAccount(AccountType.Current, CustomerNum, Overdraft: customerOverdraftLimit);
            }
            catch (Exception)
            {
                // Log error

                throw;
            }
        }

        public Account Deposit(long AccountId, long AmountToDeposit)
        {
            Account target = AccountRepo.GetAccount(AccountId);

            if (target == null)
            {
                throw new AccountNotFoundException();
            }

            try
            {
                AccountRepo.Deposit(target, AmountToDeposit);

                return target;
            }
            catch (Exception)
            {
                // Log error

                throw;
            }
        }

        public Account Withdraw(long AccountId, long AmountToWithdraw)
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
                    AccountRepo.Withdraw(target, AmountToWithdraw);

                    return target;
                }
            }
            else if (target.Type == AccountType.Current)
            {
                if (target.Balance - AmountToWithdraw > (-target.Overdraft))
                {
                    AccountRepo.Withdraw(target, AmountToWithdraw);

                    return target;
                }
            }

            throw new WithdrawalAmountTooLargeException();
        }
    }
}
