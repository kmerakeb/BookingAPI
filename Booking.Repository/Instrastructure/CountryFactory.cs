using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Booking.Repository.Entities;

namespace Booking.Repository.Instrastructure
{
    
    public class CountryFactory
    {
        CityFactory cityFactory = new CityFactory();
        public CountryFactory()
        {

        }

        public Country CreateCountry(DAO.Country country)
        {
            return new Country()
            {
                Id = country.Id,
                CountryName = country.CountryName,
                LastUpdate = country.LastUpdate,
                Cities = country.Cities == null ? new List<City>() : country.Cities.Select(b => cityFactory.CreateCity(b)).ToList()

            };
        }
        public DAO.Country CreateCountry(Country country)
        {
            return new DAO.Country()
            {
                Id = country.Id,
                CountryName = country.CountryName,
                LastUpdate = country.LastUpdate,

                Cities = country.Cities.Select(b => cityFactory.CreateCity(b)).ToList()

            };
        }

    }
}
