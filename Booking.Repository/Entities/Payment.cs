namespace Booking.Repository.Entities
{
    using System;
    using System.Collections.Generic;

    public partial class Payment
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public int BusinessId { get; set; }
        public int ServiceId { get; set; }
        public Nullable<decimal> PaymentAmount { get; set; }
        public Nullable<System.DateTime> PaymentDate { get; set; }

        //public virtual Book Book { get; set; }
        //public virtual Business Business { get; set; }
        //public virtual Service Service { get; set; }
    }
}