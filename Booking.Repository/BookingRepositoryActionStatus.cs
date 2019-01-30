using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Repository
{
    public enum BookingRepositoryActionStatus
    {
            Ok,
            Created,
            Updated,
            NotFound,
            Deleted,
            NothingModified,
            Error
    }
}
