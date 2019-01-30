using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Booking.Repository.Entities;

namespace Booking.Repository.Instrastructure
{
    public class CityFactory
    {
        AddressFactory addressFactory = new AddressFactory();
        public CityFactory()
        {

        }

        public City CreateCity(DAO.City city)
        {
            return new City()
            {
                Id = city.Id,
                CityName = city.CityName,
                CountryId = city.CountryId,
                LastUpdate = city.LastUpdate,

                Addresses = city.Addresses == null ? new List<Address>() : city.Addresses.Select(b => addressFactory.CreateAddress(b)).ToList()

            };
        }
        public DAO.City CreateCity(City city)
        {
            return new DAO.City()
            {
                Id = city.Id,
                CityName = city.CityName,
                CountryId = city.CountryId,
                LastUpdate = city.LastUpdate,

                Addresses = city.Addresses.Select(b => addressFactory.CreateAddress(b)).ToList()

            };
        }

    }
}
