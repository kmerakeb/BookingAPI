using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.DAO
{
    public class Business
    {
        public int Id { get; set; } 
        public string BusinessName { get; set; }
        public string BusinessType { get; set; }
        public string Description { get; set; }
        public int BusinessCategoryId { get; set; }
       // public int AddressId { get; set; }
        public Nullable<System.DateTime> LastUpdate { get; set; }
        public System.DateTime DateCreated { get; set; }
        public Nullable<System.DateTime> openingHour { get; set; }
        public Nullable<System.DateTime> ClosingHour { get; set; }
        public int CustomerId { get; set; }


        //public virtual ICollection<BookingCancelation> BookingCancelations { get; set; }
        //public virtual ICollection<Payment> Payments { get; set; }
        public virtual ICollection<Address> Addresses { get; set; }

        public virtual ICollection<Service> Services { get; set; }
    }
}
