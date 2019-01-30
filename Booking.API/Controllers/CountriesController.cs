using Booking.Repository;
using Booking.Repository.Instrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
//using Booking.API.Helpers;
using System.Web.Http.Routing;
using System.Web;
using booking.API.Helpers;

namespace Booking.API.Controllers
{
    public class CountriesController : ApiController
    {
        IBookingRepository _repository;
        CountryFactory _countryFactory = new CountryFactory();

        public CountriesController()
        {
            _repository = new BookingEFRepository(new
                Repository.Entities.ZetaBookingContext());
        }

        public CountriesController(IBookingRepository repository)
        {
            _repository = repository;
        }

        public IHttpActionResult Get(string sort = "id")
        {
            try
            {
                var countries = _repository.GetCountries();

                return Ok(countries.ApplySort(sort).ToList()
                    .Select(ct => _countryFactory.CreateCountry(ct)));

            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }
        // returns one country

        public IHttpActionResult Get(int id)
        {
            try
            {
                var country = _repository.GetCountry(id);
                if (country == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(_countryFactory.CreateCountry(country));
                }
            }
            catch (Exception)
            {

                return InternalServerError();
            }
        }

    }
}
