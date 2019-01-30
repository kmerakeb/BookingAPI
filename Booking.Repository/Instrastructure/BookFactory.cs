using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Booking.Repository.Entities;
using System.Dynamic;
using System.Reflection;

namespace Booking.Repository.Instrastructure
{
    public class BookFactory
    {
        //BookingCancelationFactory bookingCancelationFactory = new BookingCancelationFactory();
        //PaymentFactory paymentFactory = new PaymentFactory();
        //RebookingFactory rebookingFactory = new RebookingFactory();
        public BookFactory()
        {

        }
        public Book CreateBook(DAO.Book book)
        {


            return new Book()
            {
                Id = book.Id,
                CustomerId = book.CustomerId,
                EmpId = book.EmpId,
                Description = book.Description,
                DateCreated = book.DateCreated,
                LastUpdated = book.LastUpdated
                //BookingCancelations = book.BookingCancelations == null ? new List<BookingCancelation>() : book.BookingCancelations.Select(c => bookingCancelationFactory.CreateBookingCancelation(c)).ToList(),
                //Payments = book.Payments == null ? new List<Payment>() : book.Payments.Select(a => paymentFactory.CreatePayment(a)).ToList(),
                //Rebookings = book.Rebookings == null ? new List<Rebooking>() : book.Rebookings.Select(a => rebookingFactory.CreateRebooking(a)).ToList()



            };
        }
        public DAO.Book CreateBook(Book book)
        {
            return new DAO.Book()
            {
                Id = book.Id,
                CustomerId = book.CustomerId,
                EmpId = book.EmpId,
                Description = book.Description,
                DateCreated = book.DateCreated,
                LastUpdated = book.LastUpdated

                //BookingCancelations = book.BookingCancelations.Select(e => bookingCancelationFactory.CreateBookingCancelation(e)).ToList(),
                //Payments = book.Payments.Select(e => paymentFactory.CreatePayment(e)).ToList(),
                //Rebookings = book.Rebookings.Select(s => rebookingFactory.CreateRebooking(s)).ToList()




            };
        }

        public object CreateDataShapedObject(Book book, List<string> lstOfFields) // simple pass throu method
        {

            return CreateDataShapedObject(CreateBook(book), lstOfFields);
        }


        public object CreateDataShapedObject(DAO.Book book, List<string> lstOfFields)
        {

            if (!lstOfFields.Any())
            {
                return book;
            }
            else
            {

                // create a new ExpandoObject & dynamically create the properties for this object

                ExpandoObject objectToReturn = new ExpandoObject();
                foreach (var field in lstOfFields)
                {
                    // need to include public and instance, b/c specifying a binding flag overwrites the
                    // already-existing binding flags.

                    var fieldValue = book.GetType()
                        .GetProperty(field, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance)
                        .GetValue(book, null);

                    // add the field to the ExpandoObject
                    ((IDictionary<String, Object>)objectToReturn).Add(field, fieldValue);
                }

                return objectToReturn;
            }
        }



    }
}
