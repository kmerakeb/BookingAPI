namespace Booking.Repository.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BusinessStatus")]

    public partial class BusinessStatus
    {
        public BusinessStatus()
        {
            Businesses = new HashSet<Business>();
        }

        public int Id { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Business> Businesses { get; set; }
    }
}