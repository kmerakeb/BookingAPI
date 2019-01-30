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
using System.Web.Http.Cors;

namespace Booking.API.Controllers
{
    [EnableCors("*","*","*")]
    public class BusinessCategoriesController : ApiController
    {
        IBookingRepository _repository;
        BusinessCategoryFactory _businessCategoryFactory = new BusinessCategoryFactory();

        public BusinessCategoriesController()
        {
            _repository = new BookingEFRepository(new
                Repository.Entities.ZetaBookingContext());
        }

        public BusinessCategoriesController(IBookingRepository repository)
        {
            _repository = repository;
        }
         [Route("api/businesscategories", Name = "BusinessCategoriesList")]
        public IHttpActionResult Get(string sort="id", string fields=null)
        {
            try
            {
                bool includeBusinesses = false;
                List<string> lstOfFields = new List<string>();
                // we will include the businesses if the fields contains businesses
                if(fields != null)
                {
                    lstOfFields = fields.ToLower().Split(',').ToList();
                    includeBusinesses = lstOfFields.Any(f => f.Contains("businesses"));
                }
                IQueryable<Repository.Entities.BusinessCategory> businessCategories = null;
                if (includeBusinesses)
                {
                    businessCategories = _repository.GetBusinessCategoriesWithBusinesses();
                }
                else
                {
                    businessCategories = _repository.GetBusinessCategories();
                }

                return Ok(businessCategories.ApplySort(sort).ToList() // returns a list of our businesscategories from entities, however we want to return it from our DAO/// we map it in the next line (We want the models from our DAO.
                    .Select(eg => _businessCategoryFactory.CreateDataShapedObject(eg, lstOfFields))); // we map the entities to there coresponding DAO Models: So, For each businesscategory we call factory and return new businesscategory model
                //for each businesscategory we call factory we say create businesscategory that accept business category entity and returns business category DAO

            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }
        public IHttpActionResult Get(int id)
        {
            try
            {
                var businessCategory = _repository.GetBusinessCategory(id);
                if (businessCategory == null)
                {
                    return NotFound();
                }
                else
                    return Ok(_businessCategoryFactory.CreateBusinessCategory(businessCategory));// map businesscategory entity with businesscategory DAO and return a status code 200
            }
            catch(Exception)
            {
                return InternalServerError();
            }
        }
     
        [Route("api/businessCategories")]
        [HttpPost]
        public IHttpActionResult Post([FromBody] DAO.BusinessCategory businessCategory)
        {
            try
            {
                if(businessCategory== null)
                {
                    return BadRequest();
                }
                var bc = _businessCategoryFactory.CreateBusinessCategory(businessCategory);
                var result = _repository.InsertBusinessCategory(bc);
                if(result.Status== BookingRepositoryActionStatus.Created)
                {
                    var newBusinessCategory = _businessCategoryFactory.CreateBusinessCategory(result.Entity);
                    return Created(Request.RequestUri + "/" + newBusinessCategory.Id.ToString(), newBusinessCategory);
                }
                return BadRequest();
            }
            catch(Exception)
            {
                return InternalServerError();
            }
        }
       public IHttpActionResult Put(int id, [FromBody]DAO.BusinessCategory businessCategory)
        {
            try
            {
                if (businessCategory == null)
                    return BadRequest();
                //map
                var bc = _businessCategoryFactory.CreateBusinessCategory(businessCategory);
                var result = _repository.UpdateBusinessCategory(bc);
                if(result.Status == BookingRepositoryActionStatus.Updated)
                {
                    // map to dao
                    var updatedBusinessCategory = _businessCategoryFactory.CreateBusinessCategory(result.Entity);
                    return Ok(updatedBusinessCategory);
                }
                else if (result.Status == BookingRepositoryActionStatus.NotFound)
                {
                    return NotFound();
                }
                return BadRequest();
            }
            catch(Exception)
            {
                return InternalServerError();
            }
        }

      public IHttpActionResult Delete(int id)
        {
            try
            {
                var result = _repository.DeleteBusinessCategory(id);
                if(result.Status == BookingRepositoryActionStatus.Deleted)
                {
                    return StatusCode(HttpStatusCode.NoContent);
                }
                else if (result.Status == BookingRepositoryActionStatus.NotFound)
                {
                    return NotFound();
                }
                return BadRequest();

            }
            catch(Exception)
            {
                return InternalServerError();
            }
        }
    }
}
