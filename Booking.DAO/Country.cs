using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.DAO
{
    public class Country
    {
        public int Id { get; set; }
        public string CountryName { get; set; }
        public Nullable<System.DateTime> LastUpdate { get; set; }

        public virtual ICollection<City> Cities { get; set; }
    }
}
