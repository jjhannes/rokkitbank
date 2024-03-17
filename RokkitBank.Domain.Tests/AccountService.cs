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
            var seed = new List<Account>()
            {
                new SavingsAccount(1, 2000, 1000),
                new SavingsAccount(2, 5000, 1000),
                new CurrentAccount(3, 1000, -10000),
                new CurrentAccount(4, -5000, -20000)
            };
            this._accountService = new DefaultAccountService(Seed: seed);
        }

        [TestClass]
        public class OpenSavings()
        {
            [TestMethod]
            public void OpenSmall_ShouldThrowOpeningBalanceTooSmall()
            {
                long startingBalance = 100;
                DefaultAccountService service = new DefaultAccountService();

                Assert.ThrowsException<OpeningBalanceTooSmallException>(() => service.OpenSavingsAccount(10, startingBalance));
            }

            [TestMethod]
            public void OpenLarge_ShouldPass()
            {
                long customerNum = 10;
                long startingBalance = 1000;
                DefaultAccountService service = new DefaultAccountService();
                Account newAccount = service.OpenSavingsAccount(customerNum, startingBalance);

                Assert.IsNotNull(newAccount);
                Assert.AreEqual(customerNum, newAccount.CustomerNum);
                Assert.AreEqual(startingBalance, newAccount.CurrentBalance);
            }
        }

        [TestClass]
        public class OpenCurrent
        {
            [TestMethod]
            public void Open_ShouldPass()
            {
                long customerNum = 10;
                DefaultAccountService service = new DefaultAccountService();
                Account newAccount = service.OpenCurrentAccount(customerNum);

                Assert.IsNotNull(newAccount);
                Assert.AreEqual(customerNum, newAccount.CustomerNum);
            }
        }

        [TestClass]
        public class DepositIntoSavings
        {
            [TestMethod]
            public void DepositInvalidAccount_ShouldThrowAccountNotFound()
            {
                long invalidAccountId = 9999;
                long depositAmount = 1000;
                DefaultAccountService service = new DefaultAccountService();

                Assert.ThrowsException<AccountNotFoundException>(() => service.Deposit(invalidAccountId, depositAmount));
            }

            [TestMethod]
            public void DepositSmall_ShouldPass()
            {
                long customerNum = 10;
                long startingBalance = 1000;
                long depositAmount = 100;
                DefaultAccountService service = new DefaultAccountService();
                Account newAccount = service.OpenSavingsAccount(customerNum, startingBalance);
                newAccount = service.Deposit(newAccount.ID, depositAmount);

                Assert.IsTrue(newAccount.CurrentBalance > 0);
                Assert.AreEqual(startingBalance + depositAmount, newAccount.CurrentBalance);
            }

            [TestMethod]
            public void DepositLarge_ShouldPass()
            {
                long customerNum = 10;
                long startingBalance = 1000;
                long depositAmount = 4250;
                DefaultAccountService service = new DefaultAccountService();
                Account newAccount = service.OpenSavingsAccount(customerNum, startingBalance);
                newAccount = service.Deposit(newAccount.ID, depositAmount);

                Assert.IsTrue(newAccount.CurrentBalance > 0);
                Assert.AreEqual(startingBalance + depositAmount, newAccount.CurrentBalance);
            }
        }

        [TestClass]
        public class DepositIntoCurrent
        {
            [TestMethod]
            public void DepositInvalidAccount_ShouldThrowAccountNotFound()
            {
                long invalidAccountId = 9999;
                long depositAmount = 3000;
                DefaultAccountService service = new DefaultAccountService();

                Assert.ThrowsException<AccountNotFoundException>(() => service.Deposit(invalidAccountId, depositAmount));
            }

            [TestMethod]
            public void DepositSmall_ShouldPass()
            {
                long customerNum = 10;
                long depositAmount = 100;
                DefaultAccountService service = new DefaultAccountService();
                Account newAccount = service.OpenCurrentAccount(customerNum);
                newAccount = service.Deposit(newAccount.ID, depositAmount);

                Assert.AreEqual(depositAmount, newAccount.CurrentBalance);
            }

            [TestMethod]
            public void DepositLarge_ShouldPass()
            {
                long customerNum = 10;
                long withdrawAmount = 3000;
                long depositAmount = 100;
                DefaultAccountService service = new DefaultAccountService();
                Account newAccount = service.OpenCurrentAccount(customerNum);
                newAccount = service.Withdraw(newAccount.ID, withdrawAmount);
                newAccount = service.Deposit(newAccount.ID, depositAmount);

                Assert.AreEqual(-withdrawAmount + depositAmount, newAccount.CurrentBalance);
            }
        }

        [TestClass]
        public class WithdrawFromSavings
        {
            [TestMethod]
            public void WithdrawInvalidAccount_ShouldThrowAccountNotFound()
            {
                long invalidAccountId = 9999;
                long withdrawAmount = 1000;
                DefaultAccountService service = new DefaultAccountService();

                Assert.ThrowsException<AccountNotFoundException>(() => service.Withdraw(invalidAccountId, withdrawAmount));
            }

            [TestMethod]
            public void WithdrawSmall_ShouldPass()
            {
                long customerNum = 10;
                long startingBalance = 1000;
                long depositAmount = 3000;
                long withdrawAmount = 100;
                DefaultAccountService service = new DefaultAccountService();
                Account newAccount = service.OpenSavingsAccount(customerNum, startingBalance);
                newAccount = service.Deposit(newAccount.ID, depositAmount);
                newAccount = service.Withdraw(newAccount.ID, withdrawAmount);

                Assert.IsTrue(newAccount.CurrentBalance > 0);
                Assert.AreEqual(startingBalance + depositAmount - withdrawAmount, newAccount.CurrentBalance);
            }

            [TestMethod]
            public void WithdrawLarge_ShouldThrowWithdrawalAmountTooLarge()
            {
                long customerNum = 10;
                long startingBalance = 1000;
                long withdrawAmount = 6000;
                DefaultAccountService service = new DefaultAccountService();
                Account newAccount = service.OpenSavingsAccount(customerNum, startingBalance);

                Assert.ThrowsException<WithdrawalAmountTooLargeException>(() => service.Withdraw(newAccount.ID, withdrawAmount));
            }
        }

        [TestClass]
        public class WithdrawFromCurrent
        {
            [TestMethod]
            public void WithdrawInvalidAccount_ShouldThrowAccountNotFound()
            {
                long invalidAccountId = 9999;
                long withdrawAmount = 1000;
                DefaultAccountService service = new DefaultAccountService();

                Assert.ThrowsException<AccountNotFoundException>(() => service.Withdraw(invalidAccountId, withdrawAmount));
            }

            [TestMethod]
            public void WithdrawLessThanBalance_ShouldPass()
            {
                long customerNum = 10;
                long depositAmount = 3000;
                long withdrawAmount = 100;
                DefaultAccountService service = new DefaultAccountService();
                Account newAccount = service.OpenCurrentAccount(customerNum);
                newAccount = service.Deposit(newAccount.ID, depositAmount);
                newAccount = service.Withdraw(newAccount.ID, withdrawAmount);

                Assert.IsTrue(newAccount.CurrentBalance > 0);
                Assert.AreEqual(depositAmount - withdrawAmount, newAccount.CurrentBalance);
            }

            [TestMethod]
            public void WithdrawLessThanOverdraft_ShouldPass()
            {
                long customerNum = 10;
                DefaultAccountService service = new DefaultAccountService();
                Account newAccount = service.OpenCurrentAccount(customerNum);

                long withdrawAmount1 = (long)Math.Truncate(Math.Abs(newAccount.MinimumBalance) / 2d);
                long withdrawAmount2 = (long)Math.Truncate(withdrawAmount1 / 2d);

                newAccount = service.Withdraw(newAccount.ID, withdrawAmount1);
                newAccount = service.Withdraw(newAccount.ID, withdrawAmount2);

                Assert.AreEqual(0 - withdrawAmount1 - withdrawAmount2, newAccount.CurrentBalance);
            }

            [TestMethod]
            public void WithdrawMoreThanOverdraft_ShouldThrowWithdrawalAmountTooLarge()
            {
                long customerNum = 10;
                DefaultAccountService service = new DefaultAccountService();
                Account newAccount = service.OpenCurrentAccount(customerNum);

                long withdrawAmount = (long)Math.Truncate(Math.Abs(newAccount.MinimumBalance) / 2d);

                newAccount = service.Withdraw(newAccount.ID, withdrawAmount);

                Assert.ThrowsException<WithdrawalAmountTooLargeException>(() => service.Withdraw(newAccount.ID, withdrawAmount * 2));
            }
        }
    }
}