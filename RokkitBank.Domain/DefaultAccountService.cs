
using RokkitBank.Contracts.Dtos;
using RokkitBank.Contracts.Entities;
using RokkitBank.DB;
using RokkitBank.Contracts.Services;
using RokkitBank.Contracts.Exceptions;

namespace RokkitBank.Domain
{
    public class DefaultAccountService : IAccountService
    {
        private readonly int _minimumSavingsAccountBalance;
        private readonly int _minimumSavingsAccountCreateDeposit;
        private readonly int _maximumCurrentAccountOverdraft;

        public DefaultAccountService(
            int? MinSavingsBalance = null,
            int? MinSavingsOpeningBalance = null,
            int? MaxCurrentOverdraft = null,
            List<Account>? Seed = null)
        {
            this._minimumSavingsAccountBalance = MinSavingsBalance ?? 1000;
            this._minimumSavingsAccountCreateDeposit = MinSavingsOpeningBalance ?? 1000;
            this._maximumCurrentAccountOverdraft = MaxCurrentOverdraft ?? 100000;

            if (Seed != null)
                AccountRepo.SeedDB(Seed);
        }

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

            SavingsAccount account = new SavingsAccount(CustomerNum, AmountToDeposit, this._minimumSavingsAccountBalance);

            try
            {
                return AccountRepo.AddAccount(account);
            }
            catch (Exception)
            {
                // Log error

                throw;
            }
        }

        public Account OpenCurrentAccount(long CustomerNum)
        {
            long customerOverdraftLimit = this.CalculateCustomerOverdraftLimit(CustomerNum);

            CurrentAccount account = new CurrentAccount(CustomerNum, 0, -customerOverdraftLimit);

            try
            {
                return AccountRepo.AddAccount(account);
            }
            catch (Exception)
            {
                // Log error

                throw;
            }
        }

        public Account Deposit(long AccountId, long AmountToDeposit)
        {
            Account? target = AccountRepo.GetAccount(AccountId);

            if (target == null)
            {
                throw new AccountNotFoundException();
            }

            try
            {
                target.Deposit(AmountToDeposit);

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
            Account? target = AccountRepo.GetAccount(AccountId);

            if (target == null)
            {
                throw new AccountNotFoundException();
            }

            target.Withdraw(AmountToWithdraw);

            return target;
        }
    }
}
