using Booking.Repository.Entities;
using System;


namespace Booking.Repository
{
    public interface IBookingRepository
    {
        //Booking.Repository.Entities.Business GetBusiness(int id, int? businessCategoryId = null);

        //Booking.Repository.Entities.BusinessCategory GetBusinessCategory(int id, string userId);
        System.Linq.IQueryable<Booking.Repository.Entities.BusinessCategory> GetBusinessCategories();
        Booking.Repository.Entities.BusinessCategory GetBusinessCategory(int id);
        BookingRepositoryActionResult<Booking.Repository.Entities.BusinessCategory> InsertBusinessCategory(Booking.Repository.Entities.BusinessCategory bc);

        BookingRepositoryActionResult<Booking.Repository.Entities.BusinessCategory> UpdateBusinessCategory(Booking.Repository.Entities.BusinessCategory bc);

        BookingRepositoryActionResult<Booking.Repository.Entities.BusinessCategory> DeleteBusinessCategory(int id);

        //*******************************************************************************************************************************************************************************
        //***********************************************************************************************************************************************************
        //*******************************************************************************************************************************************
        System.Linq.IQueryable<Booking.Repository.Entities.Business> GetBusinesses(int businessCategoryId);

        BookingRepositoryActionResult<Booking.Repository.Entities.Business> DeleteBusiness(int id);

        BookingRepositoryActionResult<Booking.Repository.Entities.Business> InsertBusiness(Booking.Repository.Entities.Business b);
        BookingRepositoryActionResult<Booking.Repository.Entities.Business> UpdateBusiness(Booking.Repository.Entities.Business b);

        System.Linq.IQueryable<Booking.Repository.Entities.Business> GetBusinesses();

        Booking.Repository.Entities.Business GetBusiness(int id, int? businessCategory = null);

        System.Linq.IQueryable<Booking.Repository.Entities.BusinessCategory> GetBusinessCategoriesWithBusinesses();

        System.Linq.IQueryable<Booking.Repository.Entities.Business> GetBusinessesWithAddresses();

        System.Linq.IQueryable<Booking.Repository.Entities.Business> GetBusinessesWithServices();
        Booking.Repository.Entities.Business GetBusinessWithServices(int id); 






        //System.Linq.IQueryable<Booking.Repository.Entities.BusinessCategory> GetBusinessCategories(string userId);
        //Booking.Repository.Entities.BusinessCategoryStatus GetBusinessCategoryStatus(int id);
        //System.Linq.IQueryable<Booking.Repository.Entities.BusinessCategoryStatus> GetBusinessCategoryStatusses();
        //Booking.Repository.Entities.BusinessCategory GetBusinessCategoryWithBusinesses(int id);
        //Booking.Repository.Entities.BusinessCategory GetBusinessCategoryWithBusinesses(int id, string userId);
        //System.Linq.IQueryable<Booking.Repository.Entities.Business> GetBusinesses();
        //*******************************************************************************************************************************************************************************
        //***********************************************************************************************************************************************************
        //*******************************************************************************************************************************************
        System.Linq.IQueryable<Booking.Repository.Entities.Address> GetAddresses();
        Booking.Repository.Entities.Address GetAddress(int id);
        BookingRepositoryActionResult<Booking.Repository.Entities.Address> DeleteAddress(int id);

        BookingRepositoryActionResult<Booking.Repository.Entities.Address> InsertAddress(Booking.Repository.Entities.Address a);
        BookingRepositoryActionResult<Booking.Repository.Entities.Address> UpdateAddress(Booking.Repository.Entities.Address a); 


        //*******************************************************************************************************************************************************************************
                            //***********************************************************************************************************************************************************
                                            //*******************************************************************************************************************************************
        System.Linq.IQueryable<Booking.Repository.Entities.Payment> GetPayments();
        //*******************************************************************************************************************************************************************************
        //***********************************************************************************************************************************************************
        //*******************************************************************************************************************************************
        System.Linq.IQueryable<Booking.Repository.Entities.Available> GetAvailables();
        //*******************************************************************************************************************************************************************************
        //***********************************************************************************************************************************************************
        //*******************************************************************************************************************************************
        System.Linq.IQueryable<Booking.Repository.Entities.Customer> GetCustomers();

        Booking.Repository.Entities.Customer GetCustomer(int id);
        BookingRepositoryActionResult<Booking.Repository.Entities.Customer> DeleteCustomer(int id);

        BookingRepositoryActionResult<Booking.Repository.Entities.Customer> InsertCustomer(Booking.Repository.Entities.Customer c);
        BookingRepositoryActionResult<Booking.Repository.Entities.Customer> UpdateCustomer(Booking.Repository.Entities.Customer c);
        System.Linq.IQueryable<Booking.Repository.Entities.Customer> GetCustomersWithBooks();  

        //*******************************************************************************************************************************************************************************
        //***********************************************************************************************************************************************************
        //*******************************************************************************************************************************************
        System.Linq.IQueryable<Booking.Repository.Entities.Book> GetBooks();
        Booking.Repository.Entities.Book GetBook(int id);

        BookingRepositoryActionResult<Booking.Repository.Entities.Book> DeleteBook(int id);

        BookingRepositoryActionResult<Booking.Repository.Entities.Book> InsertBook(Booking.Repository.Entities.Book b);
        BookingRepositoryActionResult<Booking.Repository.Entities.Book> UpdateBook(Booking.Repository.Entities.Book b);
        //*******************************************************************************************************************************************************************************
        //***********************************************************************************************************************************************************
        //*******************************************************************************************************************************************
        System.Linq.IQueryable<Booking.Repository.Entities.BookingCancelation> GetBookingCancelations();
        //*******************************************************************************************************************************************************************************
        //***********************************************************************************************************************************************************
        //*******************************************************************************************************************************************
        System.Linq.IQueryable<Booking.Repository.Entities.City> GetCities();
        Booking.Repository.Entities.City GetCity(int id);
        //*******************************************************************************************************************************************************************************
        //***********************************************************************************************************************************************************
        //*******************************************************************************************************************************************
        Booking.Repository.Entities.Country GetCountry(int id);
        System.Linq.IQueryable<Booking.Repository.Entities.Country> GetCountries();
        //*******************************************************************************************************************************************************************************
        //***********************************************************************************************************************************************************
        //*******************************************************************************************************************************************
        System.Linq.IQueryable<Booking.Repository.Entities.Employee> GetEmployees();
        Booking.Repository.Entities.Employee GetEmployee(int id);
        //*******************************************************************************************************************************************************************************
        //***********************************************************************************************************************************************************
        //*******************************************************************************************************************************************
        System.Linq.IQueryable<Booking.Repository.Entities.Page> GetPages();
        Booking.Repository.Entities.Page GetPage(int id);
        //*******************************************************************************************************************************************************************************
        //***********************************************************************************************************************************************************
        //*******************************************************************************************************************************************
        System.Linq.IQueryable<Booking.Repository.Entities.Rebooking> GetRebookings();
        //*******************************************************************************************************************************************************************************
        //***********************************************************************************************************************************************************
        //*******************************************************************************************************************************************
        System.Linq.IQueryable<Booking.Repository.Entities.Service> GetServices();
        Booking.Repository.Entities.Service GetService(int id);

        BookingRepositoryActionResult<Booking.Repository.Entities.Service> DeleteService(int id);

        BookingRepositoryActionResult<Booking.Repository.Entities.Service> InsertService(Booking.Repository.Entities.Service s);
        BookingRepositoryActionResult<Booking.Repository.Entities.Service> UpdateService(Booking.Repository.Entities.Service s);

        //*******************************************************************************************************************************************************************************
        //***********************************************************************************************************************************************************
        //*******************************************************************************************************************************************
        System.Linq.IQueryable<Booking.Repository.Entities.ServiceItem> GetServiceItems();



    
       
        
    }
}
