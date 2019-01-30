using Booking.Repository;
using Booking.Repository.Instrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Routing;
using System.Web;
using booking.API.Helpers;

namespace Booking.API.Controllers
{
    public class AddressesController : ApiController
    {
        IBookingRepository _repository;
        AddressFactory _addressFactory = new AddressFactory();
        const int maxPageSize = 12;
        public AddressesController()
        {
            _repository = new BookingEFRepository(new
                Repository.Entities.ZetaBookingContext());
        }

        public AddressesController(IBookingRepository repository)
        {
            _repository = repository;
        }
        [Route("api/addresses", Name ="AddressesList")]
        public IHttpActionResult Get(string sort="id",int page = 1,int pageSize = 5)
        {
            try
            {
                var addresses = _repository.GetAddresses().ApplySort(sort);

                if(pageSize > maxPageSize)
                {
                    pageSize = maxPageSize;
                }

                // calculate metadata
                var totalCount = addresses.Count();
                var totalPages = (int)Math.Ceiling((double)totalCount/pageSize);

                // Create URIs for nex and previous pages 
                // we can use a urlhelper class that is available here
                var urlHelper = new UrlHelper(Request);
                var prevLink = page > 1 ? urlHelper.Link("AddressesList",
                    new
                    {
                        page = page - 1,
                        pagSize = pageSize,
                        sort = sort
                     }
                    ):"";
                var nextLink = page < totalPages ? urlHelper.Link("addressesList",
                    new
                    {
                        page = page + 1,
                        pagSize = pageSize,
                        sort = sort
                    }
                    ) : "";
                // Create a pagination object
                var paginationHeader = new
                {
                    currentPage = page,
                    pageSize = pageSize,
                    totalCount = totalCount,
                    totalPages = totalPages,
                    previousPageLink = prevLink,
                    nextPageLink = nextLink
                };
                // add the pagination object to the header
                HttpContext.Current.Response.Headers.Add("X-Pagination", Newtonsoft.Json.JsonConvert.SerializeObject(paginationHeader));

                return Ok(addresses
                    .Skip(pageSize * (page - 1))
                    .Take(pageSize)
                    .ToList()
                    .Select(eg => _addressFactory.CreateAddress(eg)));
                    

                //return Ok(addresses.ApplySort(sort).ToList()
                //    .Select(eg => _addressFactory.CreateAddress(eg)));

            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }
        [Route("api/addresses/{id}")]
        public IHttpActionResult Get(int id)
        {
            try
            {
                var address = _repository.GetAddress(id);
                if(address == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(_addressFactory.CreateAddress(address));
                }
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }


        [Route("api/addresses")]
        [HttpPost]
        public IHttpActionResult Post([FromBody]DAO.Address address)
        {
            try
            {
                if (address == null)
                {
                    return BadRequest();
                }
                var a = _addressFactory.CreateAddress(address);
                var result = _repository.InsertAddress(a);
                if (result.Status == BookingRepositoryActionStatus.Created)
                {
                    var newAddress = _addressFactory.CreateAddress(result.Entity);
                    return Created(Request.RequestUri + "/" + newAddress.Id.ToString(), newAddress);
                }
                return BadRequest();
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }
       

       [Route("api/addresses/{id}")]
        public IHttpActionResult Put(int id, [FromBody]DAO.Address address)
        {
            try
            {
                if (address == null)
                    return BadRequest();
                //map
                var ad = _addressFactory.CreateAddress(address);
                var result = _repository.UpdateAddress(ad);
                if (result.Status == BookingRepositoryActionStatus.Updated)
                {
                    // map to dao
                    var updatedAddress = _addressFactory.CreateAddress(result.Entity);
                    return Ok(updatedAddress);
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

        [Route("api/addresses/{id}")]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                var result = _repository.DeleteAddress(id);
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
