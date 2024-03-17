using RokkitBank.Contracts.Entities;
using System.Runtime.CompilerServices;

namespace RokkitBank.DB
{
    public static class AccountRepo
    {
        private static List<Account> _accounts = new List<Account>();
        private static long _accountIdIncrementer;

        static AccountRepo()
        {
            
        }

        public static void SeedDB(List<Account> Seed)
        {
            foreach (Account account in Seed)
                AccountRepo.AddAccount(account);
        }

        private static long GetIncrementedAccountId()
        {
            AccountRepo._accountIdIncrementer++;

            return AccountRepo._accountIdIncrementer;
        }

        public static Account? GetAccount(long ID)
        {
            return AccountRepo._accounts.FirstOrDefault(a => a.ID == ID);
        }

        public static Account AddAccount(Account newAccount)
        {
            newAccount.ID = AccountRepo.GetIncrementedAccountId();

            AccountRepo._accounts.Add(newAccount);

            return newAccount;
        }
    }
}
