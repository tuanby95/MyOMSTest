using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject2
{
    public static class SQLQueryHelper
    {
        private static string _query = "default";
        public static string GetTotalReturnQuery(DateTime fromDate, DateTime toDate)
        {
            var str1 = fromDate.ToString("yyyy-MM-dd");
            var str2 = toDate.ToString("yyyy-MM-dd");

            _query = string.Format(
                                  @"SELECT COUNT(Id)
                                    FROM [Return]
                                    WHERE [ReturnDate] BETWEEN '{0}' AND '{1}'", str1, str2);
            return _query;
        }
        public static string GetTotalReturnQuery()
        {
            _query = string.Format(
                                  @"SELECT COUNT(Id)
                                    FROM [Return]");
            return _query;
        }

        internal static string GetIssueOrderAmountByDayQuery(DateTime checkingDate)
        {
            _query = string.Format(
                                  @"SELECT COUNT(OrderStatus) AS TotalOrder
                                    FROM [Order]
                                    WHERE [Order].[Date] = '{0}' AND [OrderStatus] != 'COMPLETED'
                                    GROUP BY [Order].[Date]", checkingDate);
            return _query;
        }

        internal static string GetNewOrderAmountByDayQuery(DateTime checkingDate)
        {
            _query = string.Format(
                                  @"SELECT COUNT(Id) AS TotalOrder
                                    FROM [Order]
                                    WHERE [Order].[Date] = '{0}'", checkingDate);
            return _query;
        }

        internal static string GetTotalChannelsQuery(string channelStatus)
        {
            _query = string.Format(
                                  @"SELECT COUNT(Status) AS TotalChannel
                                    FROM [Channel]
                                    WHERE [Status] = '{0}'", channelStatus);
            return _query;
        }

        internal static string GetTotalReturnOverTimeQuery(DateTime fromDate, DateTime toDate)
        {
            var str1 = fromDate.ToString("yyyy-MM-dd");
            var str2 = toDate.ToString("yyyy-MM-dd");

            _query = string.Format(
                                  @"DECLARE @TotalReturn float
                                    SET 
                                        @TotalReturn = (
                                            SELECT COUNT(Id) AS TotalSale
                                            FROM [Return]
                                            WHERE [ReturnDate] BETWEEN '{0}' AND '{1}')

                                    SELECT rtrn.ReturnStatus
	                                       ,ROUND((COUNT(rtrn.Id)/@TotalReturn) * 100, 0) AS Percentages 
                                    FROM  [Return] AS rtrn
                                    WHERE rtrn.[ReturnDate] BETWEEN '{0}' AND '{1}'
                                    GROUP BY rtrn.ReturnStatus", str1, str2);   
            return _query;
        }
    }
}
