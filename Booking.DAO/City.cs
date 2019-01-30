using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.DAO
{
    public class City
    {
        public int Id { get; set; }
        public string CityName { get; set; }
        public int CountryId { get; set; }
        public Nullable<System.DateTime> LastUpdate { get; set; }

        public virtual ICollection<Address> Addresses { get; set; }
    }
}
