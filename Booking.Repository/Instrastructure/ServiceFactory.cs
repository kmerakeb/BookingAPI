using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Booking.Repository.Entities;

namespace Booking.Repository.Instrastructure
{
    public class ServiceFactory
    {
        //BookingCancelationFactory bookingCancelationFactory = new BookingCancelationFactory();
        //PaymentFactory paymentFactory = new PaymentFactory();
        public ServiceFactory()
        {

        }
        public Service CreateService(DAO.Service service)
        {
            
            return new Service()
            {
                Id = service.Id,
                ServiceType = service.ServiceType,
                StartTime = service.StartTime,
                EndTime = service.EndTime,
                ServiceDate = service.ServiceDate, 
                EmpId = service.EmpId,
                Description = service.Description,
                ServiceDuration = service.ServiceDuration,
                Price = service.Price,
                BusinessId = service.BusinessId

        //BookingCancelations = service.BookingCancelations == null ? new List<BookingCancelation>() : service.BookingCancelations.Select( c => bookingCancelationFactory.CreateBookingCancelation(c)).ToList(),
        //Payments = service.Payments == null ? new List<Payment>() : service.Payments.Select( p => paymentFactory.CreatePayment(p)).ToList()

    };
        }
        public DAO.Service CreateService(Service service)
        {
            return new DAO.Service()
            {
                Id = service.Id,
                ServiceType = service.ServiceType,
                StartTime = service.StartTime,
                EndTime = service.EndTime,
                ServiceDate = service.ServiceDate,
                EmpId = service.EmpId,
                Description = service.Description,
                ServiceDuration = service.ServiceDuration,
                Price = service.Price,
                BusinessId = service.BusinessId

                //BookingCancelations = service.BookingCancelations.Select( b => bookingCancelationFactory.CreateBookingCancelation(b)).ToList(),
                //Payments = service.Payments.Select(p => paymentFactory.CreatePayment(p)).ToList()


            };
        }
    }
}
