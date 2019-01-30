namespace Booking.Repository.Entities
{
    using System;
    using System.Collections.Generic;

    public partial class Rebooking
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public Nullable<System.DateTime> RebookDate { get; set; }
        public string Description { get; set; }

        //public virtual Book Book { get; set; }
    }
}
