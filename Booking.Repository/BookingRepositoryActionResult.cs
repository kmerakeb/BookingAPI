using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Repository
{

     public class BookingRepositoryActionResult<T> where T : class
    {
        public T Entity { get; private set; }
        public BookingRepositoryActionStatus Status { get; private set; }

        public Exception Exception { get; private set; }


        public BookingRepositoryActionResult(T entity, BookingRepositoryActionStatus status)
        {
            Entity = entity;
            Status = status;
        }

        public BookingRepositoryActionResult(T entity, BookingRepositoryActionStatus status, Exception exception) : this(entity, status)
        {
            Exception = exception;
        }

    }
}
