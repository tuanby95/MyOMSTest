using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject2
{
    public class DashboardSaleChannelItem : DashboardItem
    {
        public long TotalSale { get; set; }
        public string ChannelName { get; set; }
        public string ChannelImg { get; set; }
    }
}
