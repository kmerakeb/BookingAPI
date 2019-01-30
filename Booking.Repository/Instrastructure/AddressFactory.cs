using Booking.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Repository.Instrastructure
{
    public class AddressFactory
    {
        //BusinessFactory businessFactory = new BusinessFactory();
        public AddressFactory()
        {

        }
        public Address CreateAddress(DAO.Address address)
        {
            return new Address()
            {
                Id = address.Id,
                Address1 = address.Address1,
                Address2 = address.Address2,
                CityId = address.CityId,
                Distric = address.Distric,
                ZipCode = address.ZipCode,
                Phone = address.Phone,
                BusinessId = address.BusinessId,
                LastUpdate = address.LastUpdate

                //Businesses = address.Businesses == null ? new List<Business>() : address.Businesses.Select(b => businessFactory.CreateBusiness(b)).ToList()

            };
        }
        public DAO.Address CreateAddress(Address address)
        {
            return new DAO.Address()
            {
                Id = address.Id,
                Address1 = address.Address1,
                Address2 = address.Address2,
                CityId = address.CityId,
                Distric = address.Distric,
                ZipCode = address.ZipCode,
                Phone = address.Phone,
                BusinessId = address.BusinessId,
                LastUpdate = address.LastUpdate

                //Businesses = address.Businesses.Select(b => businessFactory.CreateBusiness(b)).ToList()

            };
        }
    }
}

