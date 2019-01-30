namespace Booking.Repository.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ServiceItem")]
    public partial class ServiceItem
    {
        public int Id { get; set; }
        public string ItemName { get; set; }
        public Nullable<System.DateTime> ItemStartTime { get; set; }
        public Nullable<System.DateTime> ItemEndTime { get; set; }
        public string ItemDescription { get; set; }
    }
}