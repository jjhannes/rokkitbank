
namespace RokkitBank.DB.Entities
{
    public enum AccountType
    {
        Savings,
        Current
    }

    public class Account
    {
        public long ID { get; set; }

        public AccountType Type { get; set; }

        public long CustomerNum { get; set; }

        public long Balance { get; set; } = 0;

        public long Overdraft { get; set; } = 0;
    }
}
