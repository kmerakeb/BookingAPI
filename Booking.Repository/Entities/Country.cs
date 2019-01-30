namespace Booking.Repository.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Country")]

    public partial class Country
    {
        public Country()
        {
            Cities = new HashSet<City>();
        }

        public int Id { get; set; }
        public string CountryName { get; set; }
        public Nullable<System.DateTime> LastUpdate { get; set; }

        public virtual ICollection<City> Cities { get; set; }
    }
}
