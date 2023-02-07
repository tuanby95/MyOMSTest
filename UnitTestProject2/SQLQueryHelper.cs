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
                                    WHERE [Order].[OrderedAt] = '{0}' AND [OrderStatus] != 'COMPLETED'
                                    GROUP BY [Order].[OrderedAt]", checkingDate);
            return _query;
        }

        internal static string GetNewOrderAmountByDayQuery(DateTime checkingDate)
        {
            _query = string.Format(
                                  @"SELECT COUNT(Id) AS TotalOrder
                                    FROM [Order]
                                    WHERE [Order].[OrderedAt] = '{0}'", checkingDate);
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

        internal static string GetTotalCustomerByCityQuery(string country)
        {
            _query = string.Format(
                                    @"SELECT 
                                      Zipcode, 
                                      COUNT(BuyerId) as TotalCustomer 
                                    FROM 
                                      OrderList 
                                    WHERE 
                                      Region = '{0}' 
                                    GROUP BY 
                                      Region, 
                                      ZipCode", country);
            return _query;
        }

        internal static string GetTotalNearlyOutOfStockProductsQuery()
        {
            _query = string.Format(
                                   @"SELECT 
                                          COUNT(1) as NearlyOutOfStockProducts 
                                     FROM 
                                          ProductChannel 
                                     WHERE 
                                          ProductChannel.AvaiableStock <= 10");
            return _query;
        }

        internal static string GetTotalNotSellingProductsInLastThreeMonthQuery(DateTime fromDate, DateTime toDate)
        {
            string str1 = fromDate.ToString("yyyy-MM-dd");
            string str2 = toDate.ToString("yyyy-MM-dd");
            _query = string.Format(@"
                                    SELECT 
                                      COUNT(ord.ProductId) AS Total 
                                    FROM 
                                      OrderDetail ord 
                                    WHERE 
                                      ord.ProductId NOT IN (
                                        SELECT 
                                          ordl.ProductId 
                                        FROM 
                                          OrderList ord 
                                          JOIN OrderDetail ordl ON ord.Id = ordl.OrderId 
                                        WHERE 
                                          ord.OrderedAt BETWEEN '{0}' 
                                          AND '{1}' 
                                        GROUP BY 
                                          ord.Id, 
                                          ordl.ProductId
                                      )",str1, str2
                                    );
            return _query;
        }

        internal static string GetTotalOrdersByRegionQuery(DateTime fromDate, DateTime toDate)
        {
            var str1 = fromDate.ToString("yyyy-MM-dd");
            var str2 = toDate.ToString("yyyy-MM-dd");
            _query = string.Format(@"SELECT 
                                          ord.Region, 
                                          COUNT(ord.Id) as TotalOrders 
                                     FROM 
                                          OrderList as ord 
                                     WHERE 
                                          ord.OrderedAt BETWEEN '{0}' 
                                          AND '{1}' 
                                     GROUP BY 
                                          ord.Region", str1, str2);
            return _query;
        }

        internal static string GetTotalOrdersOnChannelQuery(DateTime fromDate, DateTime toDate)
        {
            var str1 = fromDate.ToString("yyyy-MM-dd");
            var str2 = toDate.ToString("yyyy-MM-dd");
            _query = string.Format(@"SELECT 
                                          chnl.ChannelImg,
                                          chnl.ChannelName, 
                                          chnl.ChannelStatus, 
                                          COUNT(ord.Id) as TotalOrders, 
                                          SUM(ord.TotalPrice) as TotalSales 
                                      FROM 
                                          OrderList ord 
                                          JOIN Channel chnl ON ord.ChannelId = chnl.Id 
                                      WHERE 
                                          ord.OrderedAt BETWEEN '{0}' 
                                          AND '{1}' 
                                      GROUP BY 
                                          chnl.ChannelImg,
                                          chnl.ChannelName,
                                          chnl.ChannelStatus", str1, str2);
            return _query;
        }

        internal static string GetTotalOutofStockProductsQuery()
        {
            _query = string.Format(@"
                                     SELECT COUNT(1) as OutOfStockProducts
                                     FROM ProductChannel
                                     WHERE ProductChannel.AvaiableStock = 0");
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

        internal static string GetTotalSalesAndOrdersByChannelQuery()
        {
            _query = string.Format(
                                    @"
                                    SELECT 
                                      chn.ChannelImg, 
                                      chn.ChannelName, 
                                      chn.ChannelStatus, 
                                      COUNT(odl.Id) as TotalOrders, 
                                      SUM(odl.TotalPrice) as TotalSales, 
                                      chn.CreatedAt, 
                                      chn.LastUpdate 
                                    FROM 
                                      OrderList as odl 
                                      JOIN Channel as chn ON odl.ChannelId = chn.Id 
                                    GROUP BY 
                                      chn.ChannelName, 
                                      chn.ChannelImg, 
                                      chn.ChannelStatus, 
                                      chn.CreatedAt, 
                                      chn.LastUpdate");
            return _query;
        }

        internal static string GetTotalSalesByDateQuery(DateTime fromDate, DateTime toDate)
        {
            var str1 = fromDate.ToString("yyyy-MM-dd");
            var str2 = toDate.ToString("yyyy-MM-dd");

            _query = string.Format(@"
                                     SELECT 
                                      odr.OrderedAt, 
                                      (
                                        SELECT 
                                          COUNT(odr1.Id) 
                                        FROM 
                                          OrderList odr1 
                                        WHERE 
                                          odr1.OrderedAt = odr.OrderedAt
                                      ) AS TotalOrder, 
                                      (
                                        SELECT 
                                          AVG(odr2.TotalPrice) 
                                        FROM 
                                          OrderList odr2 
                                        WHERE 
                                          odr2.OrderedAt = odr.OrderedAt
                                      ) AS AVGTotalPrice, 
                                      (
                                        SELECT 
                                          SUM(odr3.TotalPrice) 
                                        FROM 
                                          OrderList odr3 
                                        WHERE 
                                          odr3.OrderStatus IN ('CANCELLED', 'FAILED') 
                                          AND odr3.OrderedAt = odr.OrderedAt
                                      ) AS ReturnPrice, 
                                      (
                                        SELECT 
                                          COUNT(rtr.Id) 
                                        FROM 
                                          ReturnList rtr 
                                        WHERE 
                                          rtr.ReturnedAt = odr.OrderedAt
                                      ) AS NumberOfReturns 
                                    FROM 
                                      OrderList odr 
                                    WHERE 
                                      odr.OrderedAt BETWEEN '{0}' 
                                      AND '{1}' 
                                    GROUP BY 
                                      odr.OrderedAt", str1, str2);
            return _query;
        }
    }
}
