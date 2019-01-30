using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.DAO
{
    public class BookingCancelation
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public int CustomerId { get; set; }
        public int ServiceId { get; set; }
        public int BusinessId { get; set; }
        public System.DateTime CancelDate { get; set; }
        public string Reason { get; set; }
    }
}
