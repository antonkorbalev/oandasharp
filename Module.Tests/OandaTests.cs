using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OandaConnect;
using System.Linq;

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

        [TestMethod]
        public void TestInstruments()
        {
            var data = _fxc.GetCandlesForInstrument("EUR_USD",
                DateTime.UtcNow.AddDays(-7), DateTime.UtcNow.AddDays(-1), "H1");
            Assert.IsTrue(data.Instrument == "EUR_USD");
            Assert.IsTrue(data.Granularity == "H1");
            Assert.IsTrue(data.Candles.Any());
        }
    }
}
