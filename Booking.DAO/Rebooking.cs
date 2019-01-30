using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.DAO
{
    public class Rebooking
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public Nullable<System.DateTime> RebookDate { get; set; }
        public string Description { get; set; }

   }
}
