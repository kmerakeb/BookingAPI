namespace Booking.Repository.Entities
{
    using System;
    using System.Collections.Generic;

    public partial class BookingCancelation
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public int CustomerId { get; set; }
        public int ServiceId { get; set; }
        public int BusinessId { get; set; }
        public System.DateTime CancelDate { get; set; }
        public string Reason { get; set; }

        public virtual Book Book { get; set; }
        public virtual Business Business { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Service Service { get; set; }
    }
}
