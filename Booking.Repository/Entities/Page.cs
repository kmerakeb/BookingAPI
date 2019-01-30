namespace Booking.Repository.Entities
{
    using System;
    using System.Collections.Generic;

    public partial class Page
    {
        public int Id { get; set; }
        public string PageURL { get; set; }
        public int BusinessId { get; set; }
        public System.DateTime CreateDate { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
        public bool IsActive { get; set; }
        public System.DateTime PublishDate { get; set; }

        public virtual Business Business { get; set; }
    }
}