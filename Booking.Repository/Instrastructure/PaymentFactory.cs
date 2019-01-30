using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Booking.Repository.Entities;

namespace Booking.Repository.Instrastructure
{
   public class PaymentFactory
    {
        public PaymentFactory()
        {

        }
        public Payment CreatePayment(DAO.Payment payment)
        {


            return new Payment()
            {
                 Id = payment.Id,
                 BookId = payment.BookId,
                 BusinessId = payment.BusinessId,
                 ServiceId = payment.ServiceId,
                 PaymentAmount = payment.PaymentAmount,
                 PaymentDate = payment.PaymentDate

    };
        }
        public DAO.Payment CreatePayment(Payment payment)
        {
            return new DAO.Payment()
            {
                Id = payment.Id,
                BookId = payment.BookId,
                BusinessId = payment.BusinessId,
                ServiceId = payment.ServiceId,
                PaymentAmount = payment.PaymentAmount,
                PaymentDate = payment.PaymentDate

            };
        }
    }
}
