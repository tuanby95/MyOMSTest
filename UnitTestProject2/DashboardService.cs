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
        private static long GetTotalByConditionInternal(string sql)
        {
            var respond = SqlHelper.ExecuteScalar(_connectionString, sql, CommandType.Text);
            long result;
            long.TryParse(respond + " ", out result);
            return result;
        }

        internal static long GetIssueOrderAmountByDay(DateTime checkingDate)
        {
            var sql = SQLQueryHelper.GetIssueOrderAmountByDayQuery(checkingDate);
            return GetTotalByConditionInternal(sql);
        }

    }
}
