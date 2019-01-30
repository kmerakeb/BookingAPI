namespace Booking.Repository.Entities
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ZetaBookingContext : DbContext
    {
        public ZetaBookingContext()
            : base("name=ZetaBookingContext")
        {
        }

        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<Available> Availables { get; set; }
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<BookingCancelation> BookingCancelations { get; set; }
        public virtual DbSet<Business> Businesses { get; set; }
        public virtual DbSet<BusinessCategory> BusinessCategories { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Page> Pages { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<Rebooking> Rebookings { get; set; }
        public virtual DbSet<Service> Services { get; set; }
        public virtual DbSet<ServiceItem> ServiceItems { get; set; }
        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{

        //    modelBuilder.Entity<Customer>()
        //        .Property(e => e.FirstName);

        //    modelBuilder.Entity<BusinessCategory>()
        //        .HasMany(e => e.Businesses)
        //        .WithRequired(e => e.BusinessCategory).WillCascadeOnDelete();
        //    //.WillCascadeOnDelete(false);

        //    modelBuilder.Entity<Business>();


        //}
    }
}
