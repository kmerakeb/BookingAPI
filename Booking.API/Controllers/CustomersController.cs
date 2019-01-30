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
    public class CustomersController : ApiController
    {
        IBookingRepository _repository;
        CustomerFactory _customerFactory = new CustomerFactory();

        public CustomersController()
        {
            _repository = new BookingEFRepository(new
                Repository.Entities.ZetaBookingContext());
        }

        public CustomersController(IBookingRepository repository)
        {
            _repository = repository;
        }
        [Route("api/customers", Name = "BooksList")]
        public IHttpActionResult Get(string sort ="id", string fields=null)
        {
            try
            {
                 bool includeBooks = false;
                List<string> lstOfFields = new List<string>();
                // we will include the books if the fields contains businesses
                if(fields != null)
                {
                    lstOfFields = fields.ToLower().Split(',').ToList();
                    includeBooks = lstOfFields.Any(f => f.Contains("books"));
                }
                IQueryable<Repository.Entities.Customer> customers = null;
                if (includeBooks)
                {
                    customers = _repository.GetCustomersWithBooks();
                }
                else
                {
                    customers = _repository.GetCustomers();
                }

                return Ok(customers.ApplySort(sort).ToList() // returns a list of our businesscategories from entities, however we want to return it from our DAO/// we map it in the next line (We want the models from our DAO.
                    .Select(eg => _customerFactory.CreateDataShapedObject(eg, lstOfFields))); // we map the entities to there coresponding DAO Models: So, For each businesscategory we call factory and return new businesscategory model
                //for each businesscategory we call factory we say create businesscategory that accept business category entity and returns business category DAO
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }
        [Route("api/customers/{id}")]
        public IHttpActionResult Get(int id)
        {
            try
            {
                var customer = _repository.GetCustomer(id);
                if(customer == null)
                {
                    return NotFound();
                }else
                {
                    return Ok(_customerFactory.CreateCustomer(customer));
                }
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        


        [Route("api/customers")]
        [HttpPost]
        public IHttpActionResult Post([FromBody]DAO.Customer customer)
        {
            try
            {
                if (customer == null)
                {
                    return BadRequest();
                }
                var c = _customerFactory.CreateCustomer(customer);
                var result = _repository.InsertCustomer(c);
                if (result.Status == BookingRepositoryActionStatus.Created)
                {
                    var newCustomer = _customerFactory.CreateCustomer(result.Entity);
                    return Created(Request.RequestUri + "/" + newCustomer.Id.ToString(), newCustomer);
                }
                return BadRequest();
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }


        [Route("api/customers/{id}")]
        public IHttpActionResult Put(int id, [FromBody]DAO.Customer customer)
        {
            try
            {
                if (customer == null)
                    return BadRequest();
                //map
                var ad = _customerFactory.CreateCustomer(customer);
                var result = _repository.UpdateCustomer(ad);
                if (result.Status == BookingRepositoryActionStatus.Updated)
                {
                    // map to dao
                    var updatedCustomer = _customerFactory.CreateCustomer(result.Entity);
                    return Ok(updatedCustomer);
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

        [Route("api/customers/{id}")]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                var result = _repository.DeleteCustomer(id);
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
