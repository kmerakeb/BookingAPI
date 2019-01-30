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
    public class CitiesController : ApiController
    {
        IBookingRepository _repository;
        CityFactory _cityFactory = new CityFactory();

        public CitiesController()
        {
            _repository = new BookingEFRepository(new
                Repository.Entities.ZetaBookingContext());
        }

        public CitiesController(IBookingRepository repository)
        {
            _repository = repository;
        }

        public IHttpActionResult Get(string sort = "id")
        {
            try
            {
                var cities = _repository.GetCities();

                return Ok(cities.ApplySort(sort).ToList()
                    .Select(ct => _cityFactory.CreateCity(ct)));

            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }
        // returns one city

        public IHttpActionResult Get(int id)
        {
            try
            {
                var city = _repository.GetCity(id);
                if(city == null)
                {
                    return NotFound();
                }else
                {
                    return Ok(_cityFactory.CreateCity(city));
                }
            }
            catch (Exception)
            {

                return InternalServerError();
            }
        }
       
    }
}
