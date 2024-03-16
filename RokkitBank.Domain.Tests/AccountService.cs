using RokkitBank.Domain.Exceptions;

namespace RokkitBank.Domain.Tests
{
    [TestClass]
    public class AccountService
    {
        [TestClass]
        public class OpenSavings()
        {
            [TestMethod]
            public void OpenSmall_ShouldThrowOpeningBalanceTooSmall()
            {
                DefaultAccountService service = new DefaultAccountService();

                bool accountCreated = false;

                Assert.ThrowsException<OpeningBalanceTooSmallException>(() => accountCreated = service.OpenSavingsAccount(10, 100));
            }

            [TestMethod]
            public void OpenLarge_ShouldPass()
            {
                DefaultAccountService service = new DefaultAccountService();
                bool accountCreated = service.OpenSavingsAccount(10, 1000);

                Assert.IsTrue(accountCreated);
            }
        }

        [TestClass]
        public class OpenCurrent
        {
            [TestMethod]
            public void Open_ShouldPass()
            {
                DefaultAccountService service = new DefaultAccountService();
                bool accountCreated = service.OpenCurrentAccount(10);

                Assert.IsTrue(accountCreated);
            }
        }

        [TestClass]
        public class DepositIntoSavings
        {
            [TestMethod]
            public void DepositInvalidAccount_ShouldThrowAccountNotFound()
            {
                Assert.Fail();
            }

            [TestMethod]
            public void DepositSmall_ShouldPass()
            {
                DefaultAccountService service = new DefaultAccountService();
                long balance = service.Deposit(1, 100);

                Assert.IsTrue(balance > 0);
                Assert.AreEqual(2100, balance);
            }

            [TestMethod]
            public void DepositLarge_ShouldPass()
            {
                DefaultAccountService service = new DefaultAccountService();
                long balance = service.Deposit(2, 4250);

                Assert.IsTrue(balance > 0);
                Assert.AreEqual(9250, balance);
            }
        }

        [TestClass]
        public class DepositIntoCurrent
        {
            [TestMethod]
            public void DepositInvalidAccount_ShouldThrowAccountNotFound()
            {
                Assert.Fail();
            }

            [TestMethod]
            public void DepositSmall_ShouldPass()
            {
                DefaultAccountService service = new DefaultAccountService();
                long balance = service.Deposit(3, 100);

                Assert.AreEqual(1100, balance);
            }

            [TestMethod]
            public void DepositLarge_ShouldPass()
            {
                DefaultAccountService service = new DefaultAccountService();
                long balance = service.Deposit(4, 3975);

                Assert.AreEqual(-1025, balance);
            }
        }

        [TestClass]
        public class WithdrawFromSavings
        {
            [TestMethod]
            public void WithdrawInvalidAccount_ShouldThrowAccountNotFound()
            {
                Assert.Fail();
            }

            [TestMethod]
            public void WithdrawSmall_ShouldPass()
            {
                DefaultAccountService service = new DefaultAccountService();
                long balance = service.Withdraw(1, 100);

                Assert.IsTrue(balance > 0);
                Assert.AreEqual(1900, balance);
            }

            [TestMethod]
            public void WithdrawLarge_ShouldThrowWithdrawalAmountTooLarge()
            {
                DefaultAccountService service = new DefaultAccountService();
                
                Assert.ThrowsException<WithdrawalAmountTooLargeException>(() => service.Withdraw(2, 6000));
            }
        }

        [TestClass]
        public class WithdrawFromCurrent
        {
            [TestMethod]
            public void WithdrawInvalidAccount_ShouldThrowAccountNotFound()
            {
                Assert.Fail();
            }

            [TestMethod]
            public void WithdrawLessThanBalance_ShouldPass()
            {
                DefaultAccountService service = new DefaultAccountService();
                long balance = service.Withdraw(3, 100);

                Assert.IsTrue(balance > 0);
                Assert.AreEqual(900, balance);
            }

            [TestMethod]
            public void WithdrawLessThanOverdraft_ShouldPass()
            {
                Assert.Fail();
            }

            [TestMethod]
            public void WithdrawMoreThanOverdraft_ShouldThrowWithdrawalAmountTooLarge()
            {
                Assert.Fail();
            }
        }
    }
}