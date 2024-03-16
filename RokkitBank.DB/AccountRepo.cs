
using RokkitBank.DB.Entities;

namespace RokkitBank.DB
{
    public static class AccountRepo
    {
        private static List<Account> _accounts;
        private static long _accountIdIncrementer;

        static AccountRepo()
        {
            AccountRepo._accounts = new List<Account>()
            {
                new Account() 
                { 
                    ID = 1, 
                    Type = AccountType.Savings,
                    CustomerNum = 1,
                    Balance = 2000
                },
                new Account()
                {
                    ID = 2,
                    Type = AccountType.Savings,
                    CustomerNum = 2,
                    Balance = 5000
                },
                new Account()
                {
                    ID = 3,
                    Type = AccountType.Current,
                    CustomerNum = 3,
                    Balance = 1000,
                    Overdraft = 10000
                },
                new Account() 
                { 
                    ID = 4,
                    Type = AccountType.Current,
                    CustomerNum = 4,
                    Balance = -5000,
                    Overdraft = 20000
                }
            };

            AccountRepo._accountIdIncrementer = AccountRepo._accounts.Count;
        }

        private static long GetIncrementedAccountId()
        {
            AccountRepo._accountIdIncrementer++;

            return AccountRepo._accountIdIncrementer;
        }

        public static Account GetAccount(long ID)
        {
            return AccountRepo._accounts.FirstOrDefault(a => a.ID == ID);
        }

        public static Account OpenAccount(AccountType Type, long CustomerNum, long Balance = 0, long Overdraft = 0)
        {
            Account newAccount = new Account()
            {
                ID = AccountRepo.GetIncrementedAccountId(),
                Type = Type,
                CustomerNum = CustomerNum,
                Balance = Balance,
                Overdraft = Overdraft
            };

            AccountRepo._accounts.Add(newAccount);

            return newAccount;
        }

        public static Account Deposit(Account Target, long Amount)
        {
            Target.Balance += Amount;

            return Target;
        }

        public static Account Withdraw(Account Target, long Amount)
        {
            Target.Balance -= Amount;

            return Target;
        }
    }
}
