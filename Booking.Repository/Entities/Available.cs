namespace Booking.Repository.Entities
{
    using System;
    using System.Collections.Generic;

    public partial class Available
    {
        public int Id { get; set; }
        public System.TimeSpan StartTime { get; set; }
        public System.TimeSpan EndTime { get; set; }
        public System.DateTime StartDate { get; set; }
        public System.DateTime EndDate { get; set; }
        public Nullable<System.DateTime> BreakStart { get; set; }
        public Nullable<System.DateTime> BreakEnd { get; set; }

    }
}