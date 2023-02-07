using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject2
{
    public class DashboardTotalSalesItem : DashboardItem
    {
        public float AverageTotalPrice { get; set; }
        public long? TotalReturnPrice { get; set; }
        public long ReturnOrderAmount { get; set; }
    }
}
