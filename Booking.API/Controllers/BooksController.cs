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
    public class BooksController : ApiController
    {
        IBookingRepository _repository;
       BookFactory _bookFactory = new BookFactory();

        public BooksController()
        {
            _repository = new BookingEFRepository(new
                Repository.Entities.ZetaBookingContext());
        }

        public BooksController(IBookingRepository repository)
        {
            _repository = repository;
        }
        [Route("api/books")]
        public IHttpActionResult Get(string sort="id")
        {
            try
            {
                var books = _repository.GetBooks();
                return Ok(books.ApplySort(sort)
                    .ToList()
                    .Select(b => _bookFactory.CreateBook(b)));
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }
        [Route("api/books/{id}")]
        public IHttpActionResult Get(int id)
        {
            try
            {
                var book = _repository.GetBook(id);
                if(book == null)
                {
                    return NotFound();
                }else
                {
                    return Ok(_bookFactory.CreateBook(book));
                }
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }
        [Route("api/books")]
        [HttpPost]
        public IHttpActionResult Post([FromBody]DAO.Book book)
        {
            try
            {
                if( book == null)
                {
                    return BadRequest();
                }
                
                    var b = _bookFactory.CreateBook(book);
                    var result = _repository.InsertBook(b);
                if (result.Status == BookingRepositoryActionStatus.Created)
                {
                    var newBook = _bookFactory.CreateBook(result.Entity);
                    return Created(Request.RequestUri + "/" + newBook.Id.ToString(), newBook);
                }
                return BadRequest();

            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }


        [Route("api/books/{id}")]
        public IHttpActionResult Put(int id, [FromBody]DAO.Book book)
        {
            try
            {
                if (book == null)
                    return BadRequest();
                //map
                var ad = _bookFactory.CreateBook(book);
                var result = _repository.UpdateBook(ad);
                if (result.Status == BookingRepositoryActionStatus.Updated)
                {
                    // map to dao
                    var updatedBook = _bookFactory.CreateBook(result.Entity);
                    return Ok(updatedBook);
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

        [Route("api/books/{id}")]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                var result = _repository.DeleteBook(id);
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
