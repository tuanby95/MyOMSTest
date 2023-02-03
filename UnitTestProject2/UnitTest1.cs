using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data.SqlClient;

namespace UnitTestProject2
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void GetTotalReturnByMonthTest()
        {
            DateTime fromDate = new DateTime(2023, 1, 1);
            DateTime toDate = new DateTime(2023, 1, 31);

            var result = DashboardService.GetTotalReturnByMonth(fromDate, toDate);
            Assert.IsTrue(result >= 0);
        }

        [TestMethod]
        public void GetTotalReturnByOverTimeTest()
        {
            DateTime fromDate = new DateTime(2023, 1, 1);
            DateTime toDate = new DateTime(2023, 1, 31);

            var result = DashboardService.GetTotalReturnByOverTime(fromDate, toDate);
            Assert.IsTrue(result.Count > 0);
        }

        [TestMethod]
        public void GetNewOrderAmountByDayTest()
        {
            DateTime checkingDate = new DateTime(2023, 1, 28);

            var result = DashboardService.GetNewOrderAmountByDay(checkingDate);
            Assert.IsTrue(result == 4);
        }

        [TestMethod]
        public void GetIssueOrderAmountByDayTest()
        {
            DateTime checkingDate = new DateTime(2023, 1, 28);

            var result = DashboardService.GetIssueOrderAmountByDay(checkingDate);
            Assert.IsTrue(result >= 1);
        }

        [TestMethod]
        public void GetTotalActiveChannelsTest()
        {
            string channelStatus = "ACITVE";
            var result = DashboardService.GetTotalChannels(channelStatus);
            Assert.IsTrue(result >= 0);
        }

        [TestMethod]
        public void GetTotalInactiveChannelsTest()
        {
            string channelStatus = "INACITVE";
            var result = DashboardService.GetTotalChannels(channelStatus);
            Assert.IsTrue(result >= 0);
        }
    }
}
