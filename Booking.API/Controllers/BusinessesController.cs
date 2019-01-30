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
    [EnableCors("*", "*", "*")]
    [RoutePrefix("api")]
    public class BusinessesController : ApiController
    {
        IBookingRepository _repository;
        BusinessFactory _businessFactory = new BusinessFactory();

        public BusinessesController()
        {
            _repository = new BookingEFRepository(new
                Repository.Entities.ZetaBookingContext());
        }

        public BusinessesController(IBookingRepository repository)
        {
            _repository = repository;
        }

        [Route("businesscategories/{businessCategoryId}/businesses", Name = "BusinessesForCategory")]
        public IHttpActionResult Get(int businessCategoryId, string sort ="businessName", string fields = null)
        {
            try
            {
                List<string> lstOfFields = new List<string>();

                if(fields != null)
                {
                    lstOfFields = fields.ToLower().Split(',').ToList();
                }

                var businesses = _repository.GetBusinesses(businessCategoryId);
                if(businesses == null)
                {
                    return NotFound();
                }
                var businessesResult = businesses.ApplySort(sort).ToList().Select(b => _businessFactory.CreateDataShapedObject(b, lstOfFields));
                return Ok(businessesResult);

            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }
        [Route("businesscategories/{businessCategoryId}/businesses/{id}")]
        [Route("businesses/{id}")]
        public IHttpActionResult Get(int id, int? businessCategoryId = null)
        {
            try
            {
                Repository.Entities.Business business = null;
                    if(businessCategoryId == null)
                {
                    business = _repository.GetBusiness(id);
                }
                else
                {
                    var categoryBusinesses = _repository.GetBusinesses((int)businessCategoryId);
                    // if the category doesn't exist, we should't try to get the businesses
                    if( categoryBusinesses != null)
                    {
                        business = categoryBusinesses.FirstOrDefault(bc => bc.id == id);
                    }
                }
                    if(business != null)
                {
                    var returnValue = _businessFactory.CreateBusiness(business);
                    return Ok(returnValue);
                }
                else
                {
                    return NotFound();
                }
            }
            catch(Exception)
            {
                return InternalServerError();
            }
        }
        [Route("businesses")]
        public IHttpActionResult Get(string sort = "id", string fields = null)
        {
            try
            {
                bool includeAddresses = false;
                List<string> lstOfFields = new List<string>();
                if (fields != null)
                {
                    lstOfFields = fields.ToLower().Split(',').ToList();
                    includeAddresses = lstOfFields.Any(f => f.Contains("addresses"));
                }
                IQueryable<Repository.Entities.Business> businesses = null;
                if (includeAddresses)
                {
                    businesses = _repository.GetBusinessesWithAddresses();
                }
                else
                {
                    businesses = _repository.GetBusinesses();
                }
                //var businesses = _repository.GetBusinesses();

                return Ok(businesses.ApplySort(sort).ToList() // returns a list of our businesscategories from entities, however we want to return it from our DAO/// we map it in the next line (We want the models from our DAO.
                    .Select(b => _businessFactory.CreateDataShapedObject(b, lstOfFields))); // we map the entities to there coresponding DAO Models: So, For each businesscategory we call factory and return new businesscategory model
                //for each businesscategory we call factory we say create businesscategory that accept business category entity and returns business category DAO

            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }



        [Route("businesses/{id}")]
        public IHttpActionResult Put(int id, [FromBody]DAO.Business business)
        {
            try
            {
                if (business == null)
                    return BadRequest();
                //map
                var bc = _businessFactory.CreateBusiness(business);
                var result = _repository.UpdateBusiness(bc);
                if (result.Status == BookingRepositoryActionStatus.Updated)
                {
                    // map to dao
                    var updatedBusiness = _businessFactory.CreateBusiness(result.Entity);
                    return Ok(updatedBusiness);
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

        [Route("businesses")]
        [HttpPost]
        public IHttpActionResult Post([FromBody]DAO.Business business)
        {
            try
            {
                if(business == null)
                {
                    return BadRequest();
                }
                // map
                var b = _businessFactory.CreateBusiness(business);
                var result = _repository.InsertBusiness(b);
                if(result.Status == BookingRepositoryActionStatus.Created)
                {
                    // map to dto
                    var newB = _businessFactory.CreateBusiness(result.Entity);
                    return Created<DAO.Business>(Request.RequestUri + "/" + newB.Id.ToString(), newB);
                }
                return BadRequest();
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }
        [Route("businesses/{id}")]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                var result = _repository.DeleteBusiness(id);
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
