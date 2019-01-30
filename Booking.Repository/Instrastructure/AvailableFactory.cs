using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Booking.Repository.Entities;

namespace Booking.Repository.Instrastructure
{
    public class AvailableFactory
    {
        //public AvailableFactory()
        //{

        //}
        public Available CreateAvailable(DAO.Available available)
        {
            AvailableFactory availableFactory = new AvailableFactory();

            return new Available()
            {
            Id =available.Id,
            StartTime =available.StartTime,
            EndTime =available.EndTime,
            StartDate =available.StartDate,
            EndDate =available.EndDate,
            BreakStart =available.BreakStart,
            BreakEnd =available.BreakEnd   



    };
        }
        public DAO.Available CreateAvailable(Available available)
        {
            return new DAO.Available()
            {
                Id = available.Id,
                StartTime = available.StartTime,
                EndTime = available.EndTime,
                StartDate = available.StartDate,
                EndDate = available.EndDate,
                BreakStart = available.BreakStart,
                BreakEnd = available.BreakEnd


            };
        }
    }
}
