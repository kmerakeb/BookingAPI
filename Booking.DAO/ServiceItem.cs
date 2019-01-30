using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.DAO
{
    public class ServiceItem
    {
        public int Id { get; set; }
        public string ItemName { get; set; }
        public Nullable<System.DateTime> ItemStartTime { get; set; }
        public Nullable<System.DateTime> ItemEndTime { get; set; }
        public string ItemDescription { get; set; }
    }
}
