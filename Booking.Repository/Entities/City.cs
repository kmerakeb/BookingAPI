namespace Booking.Repository.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("City")]
    public partial class City
    {
        public City()
        {
            Addresses = new HashSet<Address>();
        }

        public int Id { get; set; }
        public string CityName { get; set; }
        public int CountryId { get; set; }
        public Nullable<System.DateTime> LastUpdate { get; set; }

        public virtual ICollection<Address> Addresses { get; set; }
        public virtual Country Country { get; set; }
    }
}