using Booking.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Booking.Repository
{
    public class BookingEFRepository : Booking.Repository.IBookingRepository
    {

        // TODO: in a later stage, everything must be user-specific, eg: the
        // userid must always be passed in!  Don't do this in the first stage,
        // this allows us to show what can go wrong if you don't include the
        // user check.

        ZetaBookingContext _ctx;

        public BookingEFRepository(ZetaBookingContext ctx)
        {
            _ctx = ctx;

            // Disable lazy loading - if not, related properties are auto-loaded when
            // they are accessed for the first time, which means they'll be included when
            // we serialize (b/c the serialization process accesses those properties).  
            // 
            // We don't want that, so we turn it off.  We want to eagerly load them (using Include)
            // manually.

          _ctx.Configuration.LazyLoadingEnabled = false;

        }
        //********************************************************************************************************************BUSINESSCATEGORY , Get, Post ,  PUt / Patch Methodes************************************************************************************************
        public BusinessCategory GetBusinessCategory(int id)
        {
            return _ctx.BusinessCategories.FirstOrDefault(eg => eg.Id == id);
        }

        public IQueryable<BusinessCategory> GetBusinessCategories()
        {
            return _ctx.BusinessCategories;
        }

        public BookingRepositoryActionResult<BusinessCategory> InsertBusinessCategory(BusinessCategory bc)
        {
            try
            {
                _ctx.BusinessCategories.Add(bc);
                var result = _ctx.SaveChanges();
                if (result > 0)
                {
                    return new BookingRepositoryActionResult<BusinessCategory>(bc, BookingRepositoryActionStatus.Created);
                }
                else
                {
                    return new BookingRepositoryActionResult<BusinessCategory>(bc, BookingRepositoryActionStatus.NothingModified, null);
                }
            }
            catch (Exception ex)
            {
                return new BookingRepositoryActionResult<BusinessCategory>(null, BookingRepositoryActionStatus.Error, ex);
            }
        }

        public BookingRepositoryActionResult<BusinessCategory> UpdateBusinessCategory(BusinessCategory eg)
        {
            try
            {

                // you can only update when an expensegroup already exists for this id

                var existingEG = _ctx.BusinessCategories.FirstOrDefault(exg => exg.Id == eg.Id);

                if (existingEG == null)
                {
                    return new BookingRepositoryActionResult<BusinessCategory>(eg, BookingRepositoryActionStatus.NotFound);
                }

                // change the original entity status to detached; otherwise, we get an error on attach
                // as the entity is already in the dbSet

                // set original entity state to detached
                _ctx.Entry(existingEG).State = EntityState.Detached;

                // attach & save
                _ctx.BusinessCategories.Attach(eg);

                // set the updated entity state to modified, so it gets updated.
                _ctx.Entry(eg).State = EntityState.Modified;


                var result = _ctx.SaveChanges();
                if (result > 0)
                {
                    return new BookingRepositoryActionResult<BusinessCategory>(eg, BookingRepositoryActionStatus.Updated);
                }
                else
                {
                    return new BookingRepositoryActionResult<BusinessCategory>(eg, BookingRepositoryActionStatus.NothingModified, null);
                }

            }
            catch (Exception ex)
            {
                return new BookingRepositoryActionResult<BusinessCategory>(null, BookingRepositoryActionStatus.Error, ex);
            }

        }


        public BookingRepositoryActionResult<BusinessCategory> DeleteBusinessCategory(int id)
        {
            try
            {

                var eg = _ctx.BusinessCategories.Where(e => e.Id == id).FirstOrDefault();
                if (eg != null)
                {
                    // also remove all businesses linked to this expensegroup

                    _ctx.BusinessCategories.Remove(eg);

                    _ctx.SaveChanges();
                    return new BookingRepositoryActionResult<BusinessCategory>(null, BookingRepositoryActionStatus.Deleted);
                }
                return new BookingRepositoryActionResult<BusinessCategory>(null, BookingRepositoryActionStatus.NotFound);
            }
            catch (Exception ex)
            {
                return new BookingRepositoryActionResult<BusinessCategory>(null, BookingRepositoryActionStatus.Error, ex);
            }
        }

        public IQueryable<BusinessCategory> GetBusinessCategoriesWithBusinesses()
        {
            return _ctx.BusinessCategories.Include("Businesses");
        }
        public IQueryable<Business> GetBusinessesWithAddresses()
        {
            return _ctx.Businesses.Include("Addresses");
        }
        //********************************************************************************************************************Business , Get, Post ,  PUt / Patch Methodes************************************************************************************************

        //public IQueryable<Business> GetBusinesses()
        //{
        //    return _ctx.Businesses;
        //}

        public IQueryable<Business> GetBusinesses(int businessCategoryId)
        {
            // if the business category exists, we return the businesses for that category.
            // if it doesn't exist, we return null, so we can throw a not found exception

            var correctCategory = _ctx.BusinessCategories.FirstOrDefault(bc => bc.Id == businessCategoryId);
            if (correctCategory != null)
            {
                return _ctx.Businesses.Where(b => b.BusinessCategoryId == businessCategoryId);
            }
            else
            {
                return null;
            }
        }

        public BookingRepositoryActionResult<Business> UpdateBusiness(Business b)
        {
            try
            {

                // you can only update when an expense already exists for this id

                var existingBusiness = _ctx.Businesses.FirstOrDefault(exp => exp.id == b.id);

                if (existingBusiness == null)
                {
                    return new BookingRepositoryActionResult<Business>(b , BookingRepositoryActionStatus.NotFound);
                }

                // change the original entity status to detached; otherwise, we get an error on attach
                // as the entity is already in the dbSet

                // set original entity state to detached
                _ctx.Entry(existingBusiness).State = EntityState.Detached;

                // attach & save
                _ctx.Businesses.Attach(b);

                // set the updated entity state to modified, so it gets updated.
                _ctx.Entry(b).State = EntityState.Modified;


                var result = _ctx.SaveChanges();
                if (result > 0)
                {
                    return new BookingRepositoryActionResult<Business>(b, BookingRepositoryActionStatus.Updated);
                }
                else
                {
                    return new BookingRepositoryActionResult<Business>(b, BookingRepositoryActionStatus.NothingModified, null);
                }
            }
            catch (Exception ex)
            {
                return new BookingRepositoryActionResult<Business>(null, BookingRepositoryActionStatus.Error, ex);
            }

        }

        public BookingRepositoryActionResult<Business> DeleteBusiness(int id)
        {
            try
            {
                var exp = _ctx.Businesses.Where(b => b.id == id).FirstOrDefault();
                if (exp != null)
                {
                    _ctx.Businesses.Remove(exp);
                    _ctx.SaveChanges();
                    return new BookingRepositoryActionResult<Business>(null, BookingRepositoryActionStatus.Deleted);
                }
                return new BookingRepositoryActionResult<Business>(null, BookingRepositoryActionStatus.NotFound);
            }
            catch (Exception ex)
            {
                return new BookingRepositoryActionResult<Business>(null, BookingRepositoryActionStatus.Error, ex);
            }
        }

        public BookingRepositoryActionResult<Business> InsertBusiness(Business b)
        {
            try
            {
                _ctx.Businesses.Add(b);
                var result = _ctx.SaveChanges();
                if (result > 0)
                {
                    return new BookingRepositoryActionResult<Business>(b, BookingRepositoryActionStatus.Created);
                }
                else
                {
                    return new BookingRepositoryActionResult<Business>(b, BookingRepositoryActionStatus.NothingModified, null);
                }

            }
            catch (Exception ex)
            {
                return new BookingRepositoryActionResult<Business>(null, BookingRepositoryActionStatus.Error, ex);
            }
        }

        public Business GetBusiness(int id, int? businessCategoryId = null)
        {
            return _ctx.Businesses.FirstOrDefault(b => b.id == id &&
                (businessCategoryId == null || businessCategoryId == b.BusinessCategoryId));
        }
        public IQueryable<Business> GetBusinesses()
        {
            return _ctx.Businesses;
        }

        public Business GetBusinessWithAddresses(int id)
        {
            return _ctx.Businesses.Include("Addresses").FirstOrDefault(eg => eg.id == id);
        }
        public IQueryable<Business> GetBusinessesWithServices() 
        {
            return _ctx.Businesses.Include("Services");
        }

        public Business GetBusinessWithServices(int id)
        {
            return _ctx.Businesses.Include("Services").FirstOrDefault(eg => eg.id == id);
        }

        //********************************************************************************************************************Address , Get, Post ,  PUt / Patch Methodes************************************************************************************************

        public IQueryable<Address> GetAddresses()
        {
            return _ctx.Addresses;
        }

        public Address GetAddress(int id)
        {
            return _ctx.Addresses.FirstOrDefault(eg => eg.Id == id);
        }
        public BookingRepositoryActionResult<Address> UpdateAddress(Address a)
        {
            try
            {

                // you can only update when an expense already exists for this id

                var existingBusiness = _ctx.Addresses.FirstOrDefault(exp => exp.Id == a.Id);

                if (existingBusiness == null)
                {
                    return new BookingRepositoryActionResult<Address>(a, BookingRepositoryActionStatus.NotFound); 
                }

                // change the original entity status to detached; otherwise, we get an error on attach
                // as the entity is already in the dbSet

                // set original entity state to detached
                _ctx.Entry(existingBusiness).State = EntityState.Detached;

                // attach & save
                _ctx.Addresses.Attach(a);

                // set the updated entity state to modified, so it gets updated.
                _ctx.Entry(a).State = EntityState.Modified;


                var result = _ctx.SaveChanges();
                if (result > 0)
                {
                    return new BookingRepositoryActionResult<Address>(a, BookingRepositoryActionStatus.Updated);
                }
                else
                {
                    return new BookingRepositoryActionResult<Address>(a, BookingRepositoryActionStatus.NothingModified, null);
                }
            }
            catch (Exception ex)
            {
                return new BookingRepositoryActionResult<Address>(null, BookingRepositoryActionStatus.Error, ex);
            }

        }

        public BookingRepositoryActionResult<Address> DeleteAddress(int id)
        {
            try
            {
                var exp = _ctx.Addresses.Where(a => a.Id == id).FirstOrDefault();
                if (exp != null)
                {
                    _ctx.Addresses.Remove(exp);
                    _ctx.SaveChanges();
                    return new BookingRepositoryActionResult<Address>(null, BookingRepositoryActionStatus.Deleted);
                }
                return new BookingRepositoryActionResult<Address>(null, BookingRepositoryActionStatus.NotFound);
            }
            catch (Exception ex)
            {
                return new BookingRepositoryActionResult<Address>(null, BookingRepositoryActionStatus.Error, ex);
            }
        }

        public BookingRepositoryActionResult<Address> InsertAddress(Address a)
        {
            try
            {
                _ctx.Addresses.Add(a);
                var result = _ctx.SaveChanges();
                if (result > 0)
                {
                    return new BookingRepositoryActionResult<Address>(a, BookingRepositoryActionStatus.Created);
                }
                else
                {
                    return new BookingRepositoryActionResult<Address>(a, BookingRepositoryActionStatus.NothingModified, null);
                }

            }
            catch (Exception ex)
            {
                return new BookingRepositoryActionResult<Address>(null, BookingRepositoryActionStatus.Error, ex);
            }
        }
        //********************************************************************************************************************Customer , Get, Post ,  PUt / Patch Methodes************************************************************************************************

        public IQueryable<Customer> GetCustomers()
        {
            return _ctx.Customers;
        }
        public Customer GetCustomer(int id)
        {
            return _ctx.Customers.FirstOrDefault(eg => eg.Id == id);
        }
        public BookingRepositoryActionResult<Customer> UpdateCustomer(Customer c)
        {
            try
            {

                // you can only update when an expense already exists for this id

                var existingCustomer = _ctx.Customers.FirstOrDefault(cs => cs.Id == c.Id);

                if (existingCustomer == null)
                {
                    return new BookingRepositoryActionResult<Customer>(c, BookingRepositoryActionStatus.NotFound);
                }

                // change the original entity status to detached; otherwise, we get an error on attach
                // as the entity is already in the dbSet

                // set original entity state to detached
                _ctx.Entry(existingCustomer).State = EntityState.Detached;

                // attach & save
                _ctx.Customers.Attach(c);

                // set the updated entity state to modified, so it gets updated.
                _ctx.Entry(c).State = EntityState.Modified;


                var result = _ctx.SaveChanges();
                if (result > 0)
                {
                    return new BookingRepositoryActionResult<Customer>(c, BookingRepositoryActionStatus.Updated);
                }
                else
                {
                    return new BookingRepositoryActionResult<Customer>(c, BookingRepositoryActionStatus.NothingModified, null);
                }
            }
            catch (Exception ex)
            {
                return new BookingRepositoryActionResult<Customer>(null, BookingRepositoryActionStatus.Error, ex);
            }

        }

        public BookingRepositoryActionResult<Customer> DeleteCustomer(int id)
        {
            try
            {
                var exp = _ctx.Customers.Where(c => c.Id == id).FirstOrDefault();
                if (exp != null)
                {
                    _ctx.Customers.Remove(exp);
                    _ctx.SaveChanges();
                    return new BookingRepositoryActionResult<Customer>(null, BookingRepositoryActionStatus.Deleted);
                }
                return new BookingRepositoryActionResult<Customer>(null, BookingRepositoryActionStatus.NotFound);
            }
            catch (Exception ex)
            {
                return new BookingRepositoryActionResult<Customer>(null, BookingRepositoryActionStatus.Error, ex);
            }
        }


        public BookingRepositoryActionResult<Customer> InsertCustomer(Customer c)
        {
            try
            {
                _ctx.Customers.Add(c);
                var result = _ctx.SaveChanges();
                if (result > 0)
                {
                    return new BookingRepositoryActionResult<Customer>(c, BookingRepositoryActionStatus.Created);
                }
                else
                {
                    return new BookingRepositoryActionResult<Customer>(c, BookingRepositoryActionStatus.NothingModified, null);
                }

            }
            catch (Exception ex)
            {
                return new BookingRepositoryActionResult<Customer>(null, BookingRepositoryActionStatus.Error, ex);
            }
        }

        public IQueryable<Customer> GetCustomersWithBooks()
        {
            return _ctx.Customers.Include("Books");
        }

        //********************************************************************************************************************Payment , Get, Post ,  PUt / Patch Methodes************************************************************************************************


        public IQueryable<Payment> GetPayments()
        {
            return _ctx.Payments;
        }
        //********************************************************************************************************************book , Get, Post ,  PUt / Patch Methodes************************************************************************************************

        public IQueryable<Book> GetBooks()
        {
            return _ctx.Books;
        }
        public Book GetBook(int id)
        {
            return _ctx.Books.FirstOrDefault(eg => eg.Id == id);
        }

        public BookingRepositoryActionResult<Book> UpdateBook(Book b)
        {
            try
            {

                // you can only update when an expense already exists for this id

                var existingBooks = _ctx.Books.FirstOrDefault(exp => exp.Id == b.Id);

                if (existingBooks == null)
                {
                    return new BookingRepositoryActionResult<Book>(b, BookingRepositoryActionStatus.NotFound);
                }

                // change the original entity status to detached; otherwise, we get an error on attach
                // as the entity is already in the dbSet

                // set original entity state to detached
                _ctx.Entry(existingBooks).State = EntityState.Detached;

                // attach & save
                _ctx.Books.Attach(b);

                // set the updated entity state to modified, so it gets updated.
                _ctx.Entry(b).State = EntityState.Modified;


                var result = _ctx.SaveChanges();
                if (result > 0)
                {
                    return new BookingRepositoryActionResult<Book>(b, BookingRepositoryActionStatus.Updated);
                }
                else
                {
                    return new BookingRepositoryActionResult<Book>(b, BookingRepositoryActionStatus.NothingModified, null);
                }
            }
            catch (Exception ex)
            {
                return new BookingRepositoryActionResult<Book>(null, BookingRepositoryActionStatus.Error, ex);
            }

        }

        public BookingRepositoryActionResult<Book> DeleteBook(int id)
        {
            try
            {
                var exp = _ctx.Books.Where(b => b.Id == id).FirstOrDefault();
                if (exp != null)
                {
                    _ctx.Books.Remove(exp);
                    _ctx.SaveChanges();
                    return new BookingRepositoryActionResult<Book>(null, BookingRepositoryActionStatus.Deleted);
                }
                return new BookingRepositoryActionResult<Book>(null, BookingRepositoryActionStatus.NotFound);
            }
            catch (Exception ex)
            {
                return new BookingRepositoryActionResult<Book>(null, BookingRepositoryActionStatus.Error, ex);
            }
        }

        public BookingRepositoryActionResult<Book> InsertBook(Book b)
        {
            try
            {
                _ctx.Books.Add(b);
                var result = _ctx.SaveChanges();
                if (result > 0)
                {
                    return new BookingRepositoryActionResult<Book>(b, BookingRepositoryActionStatus.Created);
                }
                else
                {
                    return new BookingRepositoryActionResult<Book>(b, BookingRepositoryActionStatus.NothingModified, null);
                }

            }
            catch (Exception ex)
            {
                return new BookingRepositoryActionResult<Book>(null, BookingRepositoryActionStatus.Error, ex);
            }
        }
        //*********************************************************************************************************************BookingCancelation
        public IQueryable<BookingCancelation> GetBookingCancelations()
        {
            return _ctx.BookingCancelations;
        }
        //********************************************************************************************************************city , Get, Post ,  PUt / Patch Methodes************************************************************************************************

        public IQueryable<City> GetCities()
        {
            return _ctx.Cities;
        }
        public City GetCity(int id)
        {
            return _ctx.Cities.FirstOrDefault(eg => eg.Id == id);
        }
        //********************************************************************************************************************country , Get, Post ,  PUt / Patch Methodes************************************************************************************************

        public IQueryable<Country> GetCountries()
        {
            return _ctx.Countries;
        }
        public Country GetCountry(int id)
        {
            return _ctx.Countries.FirstOrDefault(eg => eg.Id == id);
        }
        //********************************************************************************************************************Employee , Get, Post ,  PUt / Patch Methodes************************************************************************************************

        public IQueryable<Employee> GetEmployees()
        {
            return _ctx.Employees;
        }
        public Employee GetEmployee(int id)
        {
            return _ctx.Employees.FirstOrDefault(eg => eg.Id == id);
        }
        //********************************************************************************************************************Pages , Get, Post ,  PUt / Patch Methodes************************************************************************************************

        public IQueryable<Page> GetPages()
        {
            return _ctx.Pages;
        }
        public Page GetPage(int id)
        {
            return _ctx.Pages.FirstOrDefault(eg => eg.Id == id);
        }
        //********************************************************************************************************************Rebooking , Get, Post ,  PUt / Patch Methodes************************************************************************************************

        public IQueryable<Rebooking> GetRebookings()
        {
            return _ctx.Rebookings;
        }
        //********************************************************************************************************************Service , Get, Post ,  PUt / Patch Methodes************************************************************************************************

        public IQueryable<Service> GetServices()
        {
            return _ctx.Services;
        }
        public Service GetService(int id)
        {
            return _ctx.Services.FirstOrDefault(eg => eg.Id == id);
        }

        public BookingRepositoryActionResult<Service> UpdateService(Service s)
        {
            try
            {

                // you can only update when an expense already exists for this id

                var existingService = _ctx.Services.FirstOrDefault(exp => exp.Id == s.Id);

                if (existingService == null)
                {
                    return new BookingRepositoryActionResult<Service>(s, BookingRepositoryActionStatus.NotFound);
                }

                // change the original entity status to detached; otherwise, we get an error on attach
                // as the entity is already in the dbSet

                // set original entity state to detached
                _ctx.Entry(existingService).State = EntityState.Detached;

                // attach & save
                _ctx.Services.Attach(s);

                // set the updated entity state to modified, so it gets updated.
                _ctx.Entry(s).State = EntityState.Modified;


                var result = _ctx.SaveChanges();
                if (result > 0)
                {
                    return new BookingRepositoryActionResult<Service>(s, BookingRepositoryActionStatus.Updated);
                }
                else
                {
                    return new BookingRepositoryActionResult<Service>(s, BookingRepositoryActionStatus.NothingModified, null);
                }
            }
            catch (Exception ex)
            {
                return new BookingRepositoryActionResult<Service>(null, BookingRepositoryActionStatus.Error, ex);
            }

        }

        public BookingRepositoryActionResult<Service> DeleteService(int id)
        {
            try
            {
                var exp = _ctx.Services.Where(b => b.Id == id).FirstOrDefault();
                if (exp != null)
                {
                    _ctx.Services.Remove(exp);
                    _ctx.SaveChanges();
                    return new BookingRepositoryActionResult<Service>(null, BookingRepositoryActionStatus.Deleted);
                }
                return new BookingRepositoryActionResult<Service>(null, BookingRepositoryActionStatus.NotFound);
            }
            catch (Exception ex)
            {
                return new BookingRepositoryActionResult<Service>(null, BookingRepositoryActionStatus.Error, ex);
            }
        }

        public BookingRepositoryActionResult<Service> InsertService(Service s)
        {
            try
            {
                _ctx.Services.Add(s);
                var result = _ctx.SaveChanges();
                if (result > 0)
                {
                    return new BookingRepositoryActionResult<Service>(s, BookingRepositoryActionStatus.Created);
                }
                else
                {
                    return new BookingRepositoryActionResult<Service>(s, BookingRepositoryActionStatus.NothingModified, null);
                }

            }
            catch (Exception ex)
            {
                return new BookingRepositoryActionResult<Service>(null, BookingRepositoryActionStatus.Error, ex);
            }
        }
        //********************************************************************************************************************Availables , Get, Post ,  PUt / Patch Methodes************************************************************************************************

        public IQueryable<Available> GetAvailables()
        {
            return _ctx.Availables;
        }
        //********************************************************************************************************************ServiceItem , Get, Post ,  PUt / Patch Methodes************************************************************************************************

        public IQueryable<ServiceItem> GetServiceItems()
        {
            return _ctx.ServiceItems;
        }



        //public BusinessCategory GetBusinessCategory(int id, string userId)
        //{
        //    return _ctx.BusinessCategories.FirstOrDefault(eg => eg.Id == id && eg.UserId == userId);
        //}

        //public BusinessCategory GetBusinessCategoryWithBusinesses(int id, string userId)
        //{
        //    return _ctx.BusinessCategories.Include("Businesses").FirstOrDefault(eg => eg.Id == id && eg.UserId == userId);
        //}

        //public BusinessCategory GetBusinessCategoryWithBusinesses(int id)
        //{
        //    return _ctx.BusinessCategories.Include("Businesses").FirstOrDefault(eg => eg.Id == id);
        //}



   


        ////public IQueryable<BusinessCategory> GetBusinessCategories(string userId)
        ////{
        ////    return _ctx.BusinessCategories.Where(eg => eg.UserId == userId);
        ////}


       

       

       


       

    }
}
