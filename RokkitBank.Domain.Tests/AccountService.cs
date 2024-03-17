using RokkitBank.Contracts.Dtos;
using RokkitBank.Contracts.Entities;
using RokkitBank.Contracts.Exceptions;

namespace RokkitBank.Domain.Tests
{
    [TestClass]
    public class AccountService
    {
        private readonly DefaultAccountService _accountService;

        public AccountService()
        {
            int minSavingsBalance = 1000;
            int minSavingsOpeningBalance = 1000;
            int maxCurrentOverdraft = 100000;
            var seed = new List<Account>()
            {
                new SavingsAccount(1, 2000, minSavingsBalance),
                new SavingsAccount(2, 5000, minSavingsBalance),
                new CurrentAccount(3, 1000, -10000),
                new CurrentAccount(4, -5000, -20000)
            };
            this._accountService = new DefaultAccountService(
                MinSavingsBalance: minSavingsBalance,
                MinSavingsOpeningBalance: minSavingsOpeningBalance,
                MaxCurrentOverdraft: maxCurrentOverdraft,
                Seed: seed);
        }

        [TestMethod]
        public void OpenSavings_TooSmall_ShouldThrowOpeningBalanceTooSmall()
        {
            long startingBalance = 100;

            Assert.ThrowsException<OpeningBalanceTooSmallException>(() => this._accountService.OpenSavingsAccount(10, startingBalance));
        }

        [TestMethod]
        public void OpenSavings_LargeEnough_ShouldPass()
        {
            long customerNum = 10;
            long startingBalance = 1000;

            Account newAccount = this._accountService.OpenSavingsAccount(customerNum, startingBalance);

            Assert.IsNotNull(newAccount);
            Assert.AreEqual(customerNum, newAccount.CustomerNum);
            Assert.AreEqual(startingBalance, newAccount.CurrentBalance);
        }

        [TestMethod]
        public void OpenCurrent_ShouldPass()
        {
            long customerNum = 10;

            Account newAccount = this._accountService.OpenCurrentAccount(customerNum);

            Assert.IsNotNull(newAccount);
            Assert.AreEqual(customerNum, newAccount.CustomerNum);
        }

        [TestMethod]
        public void DepositIntoSavings_InvalidAccount_ShouldThrowAccountNotFound()
        {
            long invalidAccountId = 9999;
            long depositAmount = 1000;

            Assert.ThrowsException<AccountNotFoundException>(() => this._accountService.Deposit(invalidAccountId, depositAmount));
        }

        [TestMethod]
        public void DepositIntoSavings_Small_ShouldPass()
        {
            long customerNum = 10;
            long startingBalance = 1000;
            long depositAmount = 100;

            Account newAccount = this._accountService.OpenSavingsAccount(customerNum, startingBalance);
            newAccount = this._accountService.Deposit(newAccount.ID, depositAmount);

            Assert.IsTrue(newAccount.CurrentBalance > 0);
            Assert.AreEqual(startingBalance + depositAmount, newAccount.CurrentBalance);
        }

        [TestMethod]
        public void DepositIntoSavings_Large_ShouldPass()
        {
            long customerNum = 10;
            long startingBalance = 1000;
            long depositAmount = 4250;

            Account newAccount = this._accountService.OpenSavingsAccount(customerNum, startingBalance);
            newAccount = this._accountService.Deposit(newAccount.ID, depositAmount);

            Assert.IsTrue(newAccount.CurrentBalance > 0);
            Assert.AreEqual(startingBalance + depositAmount, newAccount.CurrentBalance);
        }

        [TestMethod]
        public void DepositIntoCurrent_InvalidAccount_ShouldThrowAccountNotFound()
        {
            long invalidAccountId = 9999;
            long depositAmount = 3000;

            Assert.ThrowsException<AccountNotFoundException>(() => this._accountService.Deposit(invalidAccountId, depositAmount));
        }

        [TestMethod]
        public void DepositIntoCurrent_Small_ShouldPass()
        {
            long customerNum = 10;
            long depositAmount = 100;

            Account newAccount = this._accountService.OpenCurrentAccount(customerNum);
            newAccount = this._accountService.Deposit(newAccount.ID, depositAmount);

            Assert.AreEqual(depositAmount, newAccount.CurrentBalance);
        }

        [TestMethod]
        public void DepositIntoCurrent_Large_ShouldPass()
        {
            long customerNum = 10;
            long withdrawAmount = 3000;
            long depositAmount = 100;

            Account newAccount = this._accountService.OpenCurrentAccount(customerNum);
            newAccount = this._accountService.Withdraw(newAccount.ID, withdrawAmount);
            newAccount = this._accountService.Deposit(newAccount.ID, depositAmount);

            Assert.AreEqual(-withdrawAmount + depositAmount, newAccount.CurrentBalance);
        }

        [TestMethod]
        public void WithdrawFromSavings_InvalidAccount_ShouldThrowAccountNotFound()
        {
            long invalidAccountId = 9999;
            long withdrawAmount = 1000;

            Assert.ThrowsException<AccountNotFoundException>(() => this._accountService.Withdraw(invalidAccountId, withdrawAmount));
        }

        [TestMethod]
        public void WithdrawFromSavings_SmallEnough_ShouldPass()
        {
            long customerNum = 10;
            long startingBalance = 1000;
            long depositAmount = 3000;
            long withdrawAmount = 100;

            Account newAccount = this._accountService.OpenSavingsAccount(customerNum, startingBalance);
            newAccount = this._accountService.Deposit(newAccount.ID, depositAmount);
            newAccount = this._accountService.Withdraw(newAccount.ID, withdrawAmount);

            Assert.IsTrue(newAccount.CurrentBalance > 0);
            Assert.AreEqual(startingBalance + depositAmount - withdrawAmount, newAccount.CurrentBalance);
        }

        [TestMethod]
        public void WithdrawFromSavings_TooLarge_ShouldThrowWithdrawalAmountTooLarge()
        {
            long customerNum = 10;
            long startingBalance = 1000;
            long withdrawAmount = 6000;

            Account newAccount = this._accountService.OpenSavingsAccount(customerNum, startingBalance);

            Assert.ThrowsException<WithdrawalAmountTooLargeException>(() => this._accountService.Withdraw(newAccount.ID, withdrawAmount));
        }

        [TestMethod]
        public void WithdrawFromCurrent_InvalidAccount_ShouldThrowAccountNotFound()
        {
            long invalidAccountId = 9999;
            long withdrawAmount = 1000;

            Assert.ThrowsException<AccountNotFoundException>(() => this._accountService.Withdraw(invalidAccountId, withdrawAmount));
        }

        [TestMethod]
        public void WithdrawFromCurrent_LessThanBalance_ShouldPass()
        {
            long customerNum = 10;
            long depositAmount = 3000;
            long withdrawAmount = 100;

            Account newAccount = this._accountService.OpenCurrentAccount(customerNum);
            newAccount = this._accountService.Deposit(newAccount.ID, depositAmount);
            newAccount = this._accountService.Withdraw(newAccount.ID, withdrawAmount);

            Assert.IsTrue(newAccount.CurrentBalance > 0);
            Assert.AreEqual(depositAmount - withdrawAmount, newAccount.CurrentBalance);
        }

        [TestMethod]
        public void WithdrawFromCurrent_LessThanOverdraft_ShouldPass()
        {
            long customerNum = 10;

            Account newAccount = this._accountService.OpenCurrentAccount(customerNum);

            long withdrawAmount1 = (long)Math.Truncate(Math.Abs(newAccount.MinimumBalance) / 2d);
            long withdrawAmount2 = (long)Math.Truncate(withdrawAmount1 / 2d);

            newAccount = this._accountService.Withdraw(newAccount.ID, withdrawAmount1);
            newAccount = this._accountService.Withdraw(newAccount.ID, withdrawAmount2);

            Assert.AreEqual(0 - withdrawAmount1 - withdrawAmount2, newAccount.CurrentBalance);
        }

        [TestMethod]
        public void WithdrawFromCurrent_MoreThanOverdraft_ShouldThrowWithdrawalAmountTooLarge()
        {
            long customerNum = 10;

            Account newAccount = this._accountService.OpenCurrentAccount(customerNum);

            long withdrawAmount = (long)Math.Truncate(Math.Abs(newAccount.MinimumBalance) / 2d);

            newAccount = this._accountService.Withdraw(newAccount.ID, withdrawAmount);

            Assert.ThrowsException<WithdrawalAmountTooLargeException>(() => this._accountService.Withdraw(newAccount.ID, withdrawAmount * 2));
        }
    }
}