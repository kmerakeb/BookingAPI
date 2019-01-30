using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Booking.Repository.Entities;

namespace Booking.Repository.Instrastructure
{
   public class BookingCancelationFactory
    {
        public BookingCancelationFactory()
        {

        }
        public BookingCancelation CreateBookingCancelation(DAO.BookingCancelation bookingCancelation)
        {

            return new BookingCancelation()
            {
            Id = bookingCancelation.Id,
            BookId = bookingCancelation.BookId,
            CustomerId = bookingCancelation.CustomerId,
            ServiceId = bookingCancelation.ServiceId,
            BusinessId = bookingCancelation.BusinessId,
            CancelDate = bookingCancelation.CancelDate,
            Reason = bookingCancelation.Reason



    };
        }
        public DAO.BookingCancelation CreateBookingCancelation(BookingCancelation bookingCancelation)
        {
            return new DAO.BookingCancelation()
            {
                Id = bookingCancelation.Id,
                BookId = bookingCancelation.BookId,
                CustomerId = bookingCancelation.CustomerId,
                ServiceId = bookingCancelation.ServiceId,
                BusinessId = bookingCancelation.BusinessId,
                CancelDate = bookingCancelation.CancelDate,
                Reason = bookingCancelation.Reason

            };
        }
    }
}
