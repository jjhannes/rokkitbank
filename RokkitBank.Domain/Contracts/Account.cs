
namespace RokkitBank.Domain.Contracts
{
    public enum AccountType
    {
        Savings,
        Current
    }

    public class Account
    {
        public int ID { get; set; }

        public AccountType Type { get; set; }

        public int CustomerNum { get; set; }

        public int Balance { get; set; } = 0;

        public int Overdraft { get; set; } = 0;
    }
}
