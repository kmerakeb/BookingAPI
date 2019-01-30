using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Booking.Repository.Entities;
using System.Dynamic;
using Booking.Repository.Helpers;
using System.Reflection;

namespace Booking.Repository.Instrastructure
{
    public class CustomerFactory
    {
        BookFactory bookFactory = new BookFactory();
        BusinessFactory businessFactory = new BusinessFactory();
        public CustomerFactory()
        {

        }
        public Customer CreateCustomer(DAO.Customer customer)
        {


            return new Customer()
            {
                Id = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                MiddleName = customer.MiddleName,
                Gender = customer.Gender,
                Email = customer.Email,
                CreateDate = customer.CreateDate,
                LastUpdate = customer.LastUpdate,
                Phone = customer.Phone,
                IsActive = customer.IsActive,

                Books = customer.Books == null ? new List<Book>() : customer.Books.Select(b => bookFactory.CreateBook(b)).ToList(),
                Businesses = customer.Businesses == null ? new List<Business>() : customer.Businesses.Select(b => businessFactory.CreateBusiness(b)).ToList()

            };
        }
        public DAO.Customer CreateCustomer(Customer customer)
        {
            return new DAO.Customer()
            {
                Id = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                MiddleName = customer.MiddleName,
                Gender = customer.Gender,
                Email = customer.Email,
                CreateDate = customer.CreateDate,
                LastUpdate = customer.LastUpdate,
                Phone = customer.Phone,
                IsActive = customer.IsActive,
                Books = customer.Books.Select(b => bookFactory.CreateBook(b)).ToList(),
                Businesses = customer.Businesses.Select(b => businessFactory.CreateBusiness(b)).ToList()


            };
        }


         public object CreateDataShapedObject(Customer customer, List<string> lstOfFields)
        {
            return CreateDataShapedObject(CreateCustomer(customer), lstOfFields);
        }


        public object CreateDataShapedObject(DAO.Customer customer, List<string> lstOfFields)
        {
            // work with a new instance, as we'll manipulate this list in this method
            List<string> lstOfFieldsToWorkWith = new List<string>(lstOfFields);

            if (!lstOfFieldsToWorkWith.Any())
            {
                return customer;
            }
            else
            {

                // does it include any expense-related field?
                var lstOfBookFields = lstOfFieldsToWorkWith.Where(f => f.Contains("books")).ToList();

                // if one of those fields is "books", we need to ensure the FULL expense is returned.  If
                // it's only subfields, only those subfields have to be returned.

                bool returnPartialBook = lstOfBookFields.Any() && !lstOfBookFields.Contains("books");

                // if we don't want to return the full expense, we need to know which fields
                if (returnPartialBook)
                {
                    // remove all expense-related fields from the list of fields,
                    // as we will use the CreateDateShapedObject function in BookFactory
                    // for that.

                    lstOfFieldsToWorkWith.RemoveRange(lstOfBookFields);
                    lstOfBookFields = lstOfBookFields.Select(f => f.Substring(f.IndexOf(".") + 1)).ToList();

                }
                else
                {
                    // we shouldn't return a partial expense, but the consumer might still have
                    // asked for a subfield together with the main field, ie: expense,expense.id.  We 
                    // need to remove those subfields in that case.

                    lstOfBookFields.Remove("books");
                    lstOfFieldsToWorkWith.RemoveRange(lstOfBookFields);
                }

                // create a new ExpandoObject & dynamically create the properties for this object

                // if we have an expense

                ExpandoObject objectToReturn = new ExpandoObject();
                foreach (var field in lstOfFieldsToWorkWith)
                {
                    // need to include public and instance, b/c specifying a binding flag overwrites the
                    // already-existing binding flags.

                    var fieldValue = customer.GetType()
                        .GetProperty(field, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance)
                        .GetValue(customer, null);
             

                        // add the field to the ExpandoObject
                        ((IDictionary<String, Object>)objectToReturn).Add(field, fieldValue);
  
                }

                if (returnPartialBook)
                {
                    // add a list of books, and in that, add all those books
                    List<object> books = new List<object>();
                    foreach (var book in customer.Books)
                    {
                        books.Add(bookFactory.CreateDataShapedObject(book, lstOfBookFields));
                    }

                    ((IDictionary<String, Object>)objectToReturn).Add("books", books);
                }


                return objectToReturn;
            }
        }
    }
}
