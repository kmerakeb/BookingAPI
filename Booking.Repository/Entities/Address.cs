namespace Booking.Repository.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Address")]

    public partial class Address
    {
        public Address()
        {
        //    Businesses = new HashSet<Business>();
        }

        public int Id { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public int CityId { get; set; }
        public string Distric { get; set; }
        public string ZipCode { get; set; }
        public string Phone { get; set; }
        public int BusinessId { get; set; }
        public Nullable<System.DateTime> LastUpdate { get; set; }
    

        public virtual City City { get; set; }
        public virtual Business Business { get; set; }
    }
}
