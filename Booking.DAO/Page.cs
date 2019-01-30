using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.DAO
{
    public class Page
    {
        public int Id { get; set; }
        public string PageURL { get; set; }
        public int BusinessId { get; set; }
        public System.DateTime CreateDate { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
        public bool IsActive { get; set; }
        public System.DateTime PublishDate { get; set; }
    }
}
