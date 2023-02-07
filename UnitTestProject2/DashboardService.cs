using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using static UnitTestProject2.DataLogic;
using static UnitTestProject2.SQLQueryHelper;

namespace UnitTestProject2
{
    public static class DashboardService
    {
        private static readonly string _connectionString = "Data Source=.;Initial Catalog=OMSDb;Integrated Security=True";

        internal static long GetTotalReturnByMonth(DateTime fromDate, DateTime toDate)
        {
            var sql = SQLQueryHelper.GetTotalReturnQuery(fromDate, toDate);
            return GetTotalByConditionInternal(sql);
        }

        internal static List<DashboardItem> GetTotalReturnByOverTime(DateTime fromDate, DateTime toDate)
        {
            var sql = SQLQueryHelper.GetTotalReturnOverTimeQuery(fromDate, toDate);
            return GetTotalListByConditionInternal(sql);
        }

        internal static long GetTotalChannels(string channelStatus)
        {
            var sql = SQLQueryHelper.GetTotalChannelsQuery(channelStatus);
            return GetTotalByConditionInternal(sql);
        }

        public static long GetNewOrderAmountByDay(DateTime checkingDate)
        {
            var sql = SQLQueryHelper.GetNewOrderAmountByDayQuery(checkingDate);
            return GetTotalByConditionInternal(sql);
        }

        internal static long GetIssueOrderAmountByDay(DateTime checkingDate)
        {
            var sql = SQLQueryHelper.GetIssueOrderAmountByDayQuery(checkingDate);
            return GetTotalByConditionInternal(sql);
        }

        internal static List<DashboardItem> GetTotalOrdersByRegion(DateTime fromDate, DateTime toDate)
        {
            var sql = SQLQueryHelper.GetTotalOrdersByRegionQuery(fromDate, toDate);
            return GetTotalListByConditionInternal(sql);
        }

        internal static List<DashboardSaleChannelItem> GetTotalOrdersOnChannel(DateTime fromDate, DateTime toDate)
        {
            var sql = SQLQueryHelper.GetTotalOrdersOnChannelQuery(fromDate, toDate);
            return GetTotalListOrderByConditionInternal(sql);
        }

        internal static long GetTotalOutOfStockProducts()
        {
            var sql = SQLQueryHelper.GetTotalOutofStockProductsQuery();
            return GetTotalByConditionInternal(sql);
        }

        internal static long GetTotalNearlyOutOfStockProducts()
        {
            var sql = SQLQueryHelper.GetTotalNearlyOutOfStockProductsQuery();
            return GetTotalByConditionInternal(sql);
        }

        internal static long GetTotalNotSellingProductsInLastThreeMonth(DateTime fromDate, DateTime toDate)
        {
            var sql = SQLQueryHelper.GetTotalNotSellingProductsInLastThreeMonthQuery(fromDate, toDate);
            return GetTotalByConditionInternal(sql);
        }

        internal static List<DashboardItem> GetTotalCustomerByCity(string country)
        {
            var sql = SQLQueryHelper.GetTotalCustomerByCityQuery(country);
            return GetTotalListByConditionInternal(sql);
        }

        internal static List<DashboardTotalSalesItem> GetTotalSalesByDate(DateTime fromDate, DateTime toDate)
        {
            var sql = SQLQueryHelper.GetTotalSalesByDateQuery(fromDate, toDate);
            return GetTotalSalesListByConditionInternal(sql);
        }

        private static long? ConvertToLong(object v)
        {
            if (v == null || v.ToString() == String.Empty)
            {
                return 0;
            }
            return long.Parse(v.ToString());
        }

        private static long GetTotalByConditionInternal(string sql)
        {
            var respond = SqlHelper.ExecuteScalar(_connectionString, sql, CommandType.Text);
            long result;
            long.TryParse(respond + " ", out result);
            return result;
        }

        private static List<DashboardTotalSalesItem> GetTotalSalesListByConditionInternal(string sql)
        {
            var reader = SqlHelper.ExecuteReader(_connectionString, sql, CommandType.Text);
            var result = new List<DashboardTotalSalesItem>();
            while (reader.Read())
            {
                var item = new DashboardTotalSalesItem
                {
                    DisplayText = reader[0] + "",
                    Value = long.Parse(reader[1] + ""),
                    AverageTotalPrice = float.Parse(reader[2] + ""),
                    TotalReturnPrice = ConvertToLong(reader[3]),
                    ReturnOrderAmount = long.Parse(reader[4] + "")
                };
                result.Add(item);
            }
            return result;
        }

        private static List<DashboardSaleChannelItem> GetTotalListOrderByConditionInternal(string sql)
        {
            var reader = SqlHelper.ExecuteReader(_connectionString, sql, CommandType.Text);
            var result = new List<DashboardSaleChannelItem>();
            while (reader.Read())
            {
                var item = new DashboardSaleChannelItem
                {
                    ChannelImg = reader[0] + "",
                    ChannelName = reader[1] + "",
                    DisplayText = reader[2] + "",
                    Value = long.Parse(reader[3] + ""),
                    TotalSale = long.Parse(reader[4] + "")
                };
                result.Add(item);
            }
            return result;
        }

        private static List<DashboardItem> GetTotalListByConditionInternal(string sql)
        {
            var reader = SqlHelper.ExecuteReader(_connectionString, sql, CommandType.Text);
            var result = new List<DashboardItem>();
            while(reader.Read())
            {
                var item = new DashboardItem
                {
                    DisplayText = reader[0] + "",
                    Value = long.Parse(reader[1] + "")
                };
                result.Add(item);
            }
            return result;
        }

        internal static List<DashboardChannelItem> GetTotalSalesAndOrdersByChannel()
        {
            string sql = SQLQueryHelper.GetTotalSalesAndOrdersByChannelQuery();
            return GetTotalChannelByCondition(sql);
        }

        private static List<DashboardChannelItem> GetTotalChannelByCondition(string sql)
        {
            var reader = SqlHelper.ExecuteReader(_connectionString, sql, CommandType.Text);
            var result = new List<DashboardChannelItem>();
            while (reader.Read())
            {
                var item = new DashboardChannelItem
                {
                    ChannelImg = reader[0] + "",
                    DisplayText = reader[1] + "",
                    Status = reader[2] + "",
                    Value = long.Parse(reader[3] + ""),
                    TotalSales = long.Parse(reader[4] + ""),
                    CreateTime = reader[5] + "",
                    UpdateTime = reader[6] + ""
                };
                result.Add(item);
            }
            return result;
        }
    }
}
