namespace Booking.Repository.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Business")]
    public partial class Business
    {
        public Business()
        {
            //BookingCancelations = new HashSet<BookingCancelation>();
            Addresses = new HashSet<Address>();
            Services = new HashSet<Service>();
        }

        public int id { get; set; }
        public string BusinessName { get; set; }
        public string BusinessType { get; set; }
        public string Description { get; set; }
        public int BusinessCategoryId { get; set; }
        public Nullable<System.DateTime> LastUpdate { get; set; }
        public System.DateTime DateCreated { get; set; }
        public Nullable<System.DateTime> openingHour { get; set; }
        public Nullable<System.DateTime> ClosingHour { get; set; }
        public int CustomerId { get; set; } 
        public virtual Customer Customer { get; set; }


        //public virtual Address Address { get; set; }
        //public virtual ICollection<BookingCancelation> BookingCancelations { get; set; }
        //public virtual ICollection<Payment> Payments { get; set; }
        //public virtual Page Page { get; set; }
        public virtual BusinessCategory BusinessCategory { get; set; }

        //public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<Address> Addresses { get; set; }

        public virtual ICollection<Service> Services { get; set; }
    }
}