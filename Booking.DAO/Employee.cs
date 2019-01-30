using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.DAO
{
    public class Employee
    {
        public int Id { get; set; }
        public string EmpFirstName { get; set; }
        public string EmpLastName { get; set; }
        public int EmpZip { get; set; }
        public decimal EmpRate { get; set; }
        public string EmpPhone { get; set; }
        public int BusinessId { get; set; }

        //public virtual ICollection<Address> Addresses { get; set; }
        //public virtual ICollection<Book> Books { get; set; }
        //public virtual ICollection<Service> Services { get; set; }
    }
}
