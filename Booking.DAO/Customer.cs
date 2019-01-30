using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.DAO
{
    public class Customer
    {
 
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public System.DateTime CreateDate { get; set; }
        public System.DateTime LastUpdate { get; set; }
        public string Phone { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public virtual ICollection<Book> Books { get; set; }
        public virtual ICollection<Business> Businesses { get; set; }


    }
}
