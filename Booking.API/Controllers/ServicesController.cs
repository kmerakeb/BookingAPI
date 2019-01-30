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
    public class ServicesController : ApiController
    {
        IBookingRepository _repository;
        ServiceFactory _serviceFactory = new ServiceFactory();

        public ServicesController()
        {
            _repository = new BookingEFRepository(new
                Repository.Entities.ZetaBookingContext());
        }

        public ServicesController(IBookingRepository repository)
        {
            _repository = repository;
        }
        [Route("api/services")]
        public IHttpActionResult Get(string sort="id")
        {
            try
            {
                var services = _repository.GetServices();
                return Ok(services.ApplySort(sort)
                    .ToList()
                    .Select(s => _serviceFactory.CreateService(s))); 
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }
        [Route("api/services/{id}")]
        public IHttpActionResult Get(int id)
        {
            try
            {
                var service = _repository.GetService(id);
                if(service == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(_serviceFactory.CreateService(service));
                }

            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }


        [Route("api/services")]
        [HttpPost]
        public IHttpActionResult Post([FromBody]DAO.Service service)
        {
            try
            {
                if (service == null)
                {
                    return BadRequest();
                }

                var b = _serviceFactory.CreateService(service);
                var result = _repository.InsertService(b);
                if (result.Status == BookingRepositoryActionStatus.Created)
                {
                    var newService = _serviceFactory.CreateService(result.Entity);
                    return Created(Request.RequestUri + "/" + newService.Id.ToString(), newService);
                }
                return BadRequest();

            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }


        [Route("api/services/{id}")]
        public IHttpActionResult Put(int id, [FromBody]DAO.Service service)
        {
            try
            {
                if (service == null)
                    return BadRequest();
                //map
                var ad = _serviceFactory.CreateService(service);
                var result = _repository.UpdateService(ad);
                if (result.Status == BookingRepositoryActionStatus.Updated)
                {
                    // map to dao
                    var updateService = _serviceFactory.CreateService(result.Entity);
                    return Ok(updateService);
                }
                else if (result.Status == BookingRepositoryActionStatus.NotFound)
                {
                    return NotFound();
                }
                return BadRequest();
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        [Route("api/services/{id}")]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                var result = _repository.DeleteService(id);
                if (result.Status == BookingRepositoryActionStatus.Deleted)
                {
                    return StatusCode(HttpStatusCode.NoContent);
                }
                else if (result.Status == BookingRepositoryActionStatus.NotFound)
                {
                    return NotFound();
                }
                return BadRequest();
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }
    }
}
