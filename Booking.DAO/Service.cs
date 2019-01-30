using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.DAO
{
    public class Service
    {
        public int Id { get; set; }
        public string ServiceType { get; set; }
        public Nullable<System.DateTime> StartTime { get; set; }
        public Nullable<System.DateTime> EndTime { get; set; }
        public DateTime ServiceDate { get; set; } 
        public Nullable<int> EmpId { get; set; }
        public string Description { get; set; }
        public TimeSpan ServiceDuration { get; set; }
        public decimal Price { get; set; }
        public int BusinessId { get; set; }

        //public virtual ICollection<BookingCancelation> BookingCancelations { get; set; }
        //public virtual ICollection<Payment> Payments { get; set; }
    }
}
