using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.DAO
{
    public class Address
    {
        public int Id { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public int CityId { get; set; }
        public string Distric { get; set; }
        public string ZipCode { get; set; }
        public string Phone { get; set; }
        public int BusinessId { get; set; }
        public Nullable<System.DateTime> LastUpdate { get; set; }

    }
}
