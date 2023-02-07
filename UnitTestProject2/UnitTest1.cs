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

        [TestMethod]
        public void GetTotalOrdersByRegionTest()
        {
            DateTime fromDate = new DateTime(2023, 01, 24);
            DateTime toDate = new DateTime(2023, 01, 30);
            var result = DashboardService.GetTotalOrdersByRegion(fromDate, toDate);
            Assert.IsTrue(result.Count > 0);
        }

        [TestMethod]
        public void GetTotalOrdersOnChannelTest()
        {
            DateTime fromDate = new DateTime(2023, 01, 24);
            DateTime toDate = new DateTime(2023, 01, 30);
            var result = DashboardService.GetTotalOrdersOnChannel(fromDate, toDate);
            Assert.IsTrue(result.Count > 0);
        }

        [TestMethod]
        public void GetTotalOutOfStockProductsTest()
        {
            var result = DashboardService.GetTotalOutOfStockProducts();
            Assert.IsTrue(result == 0);
        }

        [TestMethod]
        public void GetTotalNearlyOutOfStockProductsTest()
        {
            var result = DashboardService.GetTotalNearlyOutOfStockProducts();
            Assert.IsTrue(result == 0);
        }

        [TestMethod]
        public void GetTotalNotSellingProductsInLastThreeMonthTest()
        {
            DateTime fromDate = new DateTime(2023, 01, 24);
            DateTime toDate = new DateTime(2023, 03, 24);
            var result = DashboardService.GetTotalNotSellingProductsInLastThreeMonth(fromDate, toDate);
            Assert.IsTrue(result > 3);
        }

        [TestMethod]
        public void GetTotalSalesByDateTest()
        {
            DateTime fromDate = new DateTime(2023, 01, 24);
            DateTime toDate = new DateTime(2023, 04, 24);

            var result = DashboardService.GetTotalSalesByDate(fromDate, toDate);
            Assert.IsTrue(result.Count > 0);
        }

        [TestMethod]
        public void GetTotalCustomersByCityTest()
        {
            string country = "Vietnam";

            var result = DashboardService.GetTotalCustomerByCity(country);
            Assert.IsTrue(result.Count > 0);
        }

        [TestMethod]
        public void GetTotalSalesAndOrdersByChannelTest()
        {
            var result = DashboardService.GetTotalSalesAndOrdersByChannel();
            Assert.IsTrue(result.Count > 0);
        }
    }
}
