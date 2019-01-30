using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Booking.Repository.Entities;

namespace Booking.Repository.Instrastructure
{
    public class RebookingFactory
    {
        public RebookingFactory()
        {

        }
        public Rebooking CreateRebooking(DAO.Rebooking rebooking)
        {
            return new Rebooking()
            {
                Id = rebooking.Id,
                BookId = rebooking.BookId,
                RebookDate = rebooking.RebookDate,
                Description = rebooking.Description

            };
        }
        public DAO.Rebooking CreateRebooking(Rebooking rebooking)
        {
            return new DAO.Rebooking()
            {
                Id = rebooking.Id,
                BookId = rebooking.BookId,
                RebookDate = rebooking.RebookDate,
                Description = rebooking.Description

            };
        }
    }
}
