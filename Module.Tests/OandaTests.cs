using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OandaConnect;

namespace Module.Tests
{
    [TestClass]
    public class OandaConnectTests
    {
        private readonly FxConnect _fxc = new FxConnect(TestCredentials.Token, OandaEnv.Practice);

        [TestMethod]
        public void TestAccountInfo()
        {
            var accNum = TestCredentials.AccNum;
            var account= _fxc.GetAccountSummary(accNum);
            Assert.AreEqual(accNum, account.Id);
            Assert.IsNotNull(account.Currency);
        }
    }
}
