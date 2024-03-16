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
                Assert.Fail();
            }

            [TestMethod]
            public void OpenLarge_ShouldPass()
            {
                Assert.Fail();
            }
        }

        [TestClass]
        public class OpenCurrent
        {
            [TestMethod]
            public void OpenSmall_ShouldPass()
            {
                Assert.Fail();
            }

            [TestMethod]
            public void OpenLarge_ShouldPass()
            {
                Assert.Fail();
            }
        }

        [TestClass]
        public class DepositIntoSavings
        {
            [TestMethod]
            public void DepositSmall_ShouldPass()
            {
                Assert.Fail();
            }

            [TestMethod]
            public void DepositLarge_ShouldPass()
            {
                Assert.Fail();
            }
        }

        [TestClass]
        public class DepositIntoCurrent
        {
            [TestMethod]
            public void DepositSmall_ShouldPass()
            {
                Assert.Fail();
            }

            [TestMethod]
            public void DepositLarge_ShouldPass()
            {
                Assert.Fail();
            }
        }

        [TestClass]
        public class WithdrawFromSavings
        {
            [TestMethod]
            public void WithdrawSmall_ShouldPass()
            {
                Assert.Fail();
            }

            [TestMethod]
            public void WithdrawLarge_ShouldThrowWithdrawalAmountTooLarge()
            {
                Assert.Fail();
            }
        }

        [TestClass]
        public class WithdrawFromCurrent
        {
            [TestMethod]
            public void WithdrawLessThanBalance_ShouldPass()
            {
                Assert.Fail();
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