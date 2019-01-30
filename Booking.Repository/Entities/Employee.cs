namespace Booking.Repository.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Employee")]
    public partial class Employee
    {
        //public Employee()
        //{
        //    Addresses = new HashSet<Address>();
        //    Books = new HashSet<Book>();
        //    Services = new HashSet<Service>();
        //}

        public int Id { get; set; }
        public string EmpFirstName { get; set; }
        public string EmpLastName { get; set; }
        public int EmpZip { get; set; }
        public decimal EmpRate { get; set; }
        public string EmpPhone { get; set; }
        public int BusinessId { get; set; }

        //public virtual ICollection<Address> Addresses { get; set; }
        //public virtual ICollection<Book> Books { get; set; }
        //public virtual Business Business { get; set; }
        //public virtual ICollection<Service> Services { get; set; }
    }
}
