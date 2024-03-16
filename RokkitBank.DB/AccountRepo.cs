
using RokkitBank.Domain.Contracts;

namespace RokkitBank.DB
{
    public class AccountRepo
    {
        private static List<Account> _accounts;
        private static int _accountIdIncrementer;

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
                    Balance = 4000,
                    Overdraft = 20000
                }
            };

            AccountRepo._accountIdIncrementer = AccountRepo._accounts.Count;
        }

        private static int GetIncrementedAccountId()
        {
            AccountRepo._accountIdIncrementer++;

            return AccountRepo._accountIdIncrementer;
        }

        public static Account OpenAccount(int ID)
        {
            Account newAccount = new Account()
            {
                ID = ID,
                Type = AccountType.Current,
                CustomerNum = 1,
                Balance = 0,
                Overdraft = 0
            };

            AccountRepo._accounts.Add(newAccount);

            return newAccount;
        }
    }
}
