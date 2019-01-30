namespace Booking.Repository.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Service")]
    public partial class Service
    {
        public Service()
        {
          
        }

        public int Id { get; set; }
        public string ServiceType { get; set; }
        public Nullable<System.DateTime> StartTime { get; set; }
        public Nullable<System.DateTime> EndTime { get; set; }
        public DateTime ServiceDate { get; set; } 
        public Nullable<int> EmpId { get; set; }
        public string Description { get; set; }
        public TimeSpan ServiceDuration { get; set; }
        public decimal Price { get; set; }
        public int BusinessId { get; set; } 
        public virtual Business Business { get; set; }


        //public virtual ICollection<BookingCancelation> BookingCancelations { get; set; }
        //public virtual Employee Employee { get; set; }
        //public virtual ICollection<Payment> Payments { get; set; }
    }
}