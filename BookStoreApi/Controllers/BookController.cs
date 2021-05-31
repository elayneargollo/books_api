using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Solutis.Business;
using Microsoft.AspNetCore.Authorization;
using Solutis.Data.VO;
using Solutis.Model;
using System.Collections.Generic;
using Solutis.Repositories;

namespace Solutis.Controllers
{
    [Produces("application/json")]
    [ApiVersion("1")]
    [Route("api/[controller]/v{version:apiVersion}")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly ILogger<BookController> _logger;
        private IBookBusiness _bookBusiness;
        private IBookRepository _bookRepository;

        public BookController(ILogger<BookController> logger, IBookBusiness bookBusiness, IBookRepository bookRepository)
        {
            _logger = logger;
            _bookBusiness = bookBusiness;
            _bookRepository = bookRepository;

        }

        /// <summary>
        /// Returns a list of books.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/book/v1
        ///     
        /// </remarks>  
        /// <returns>Search Results</returns>
        /// <response code="200">List of books successfully returns</response>
        /// <response code="204">No Content</response>
        /// <response code="400">Business logic error, see return message for more info</response>
        /// <response code="401">Unauthorized. Token not present, invalid or expired</response>
        /// <response code="500">Due to server problems, it`s not possible to get your data now</response>

        [HttpGet]
     //   [AllowAnonymous]
        public IActionResult Get()
        {

            List<BookVO> books = new List<BookVO>();
            books = _bookBusiness.FindAll();

            if (books == null) return BadRequest();

            foreach(var item in books)
            {
                item.Imagem = _bookRepository.GetImagBook(item);
            }

            return Ok(books);

        }

        /// <summary>
        /// Search specific book by id.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/book/v1/{id}
        ///
        /// </remarks>  
        /// <returns>Book with chosen id </returns>
        /// <param name = "id">Book id.</param>
        /// <response code="200">Book successfully found and returned</response>
        /// <response code="204">No Content</response>
        /// <response code="400">Business logic error, see return message for more info</response>
        /// <response code="401">Unauthorized. Token not present, invalid or expired</response>
        /// <response code="403">Resource access is denied</response>
        /// <response code="500">Due to server problems, it`s not possible to get your data now</response>

        [HttpGet("{id}")]
        [AllowAnonymous]
        public IActionResult Get([FromRoute] long id)
        {
            var book = _bookBusiness.FindByID(id);
            if (book == null) return BadRequest("Couldn't find this book");

            Book livro = new Book();
            livro = _bookRepository.GetInfoOneBook(book);

            if (livro == null) return BadRequest("Could not insert new information for this ISBN");

            return Ok(livro);
        }

        /// <summary>
        /// Insert/create a specific book. *Requires authentication*
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/book/v1
        ///     {
        ///        "isbn": "9788575224083",
        ///        "price": 45,
        ///        "category": "programming" ,
        ///        "title":"Introdução à programação com Python – 2ª edição",
        ///        "amount":2
        ///     }
        ///
        /// </remarks>      
        /// <returns>Book created</returns>
        /// <response code="200">Book successfully inserted</response>
        /// <response code="204">No Content</response>
        /// <response code="400">Business logic error or duplicate ISBN, see return message for more information</response>
        /// <response code="401">Unauthorized. Token not present, invalid or expired</response>
        /// <response code="403">Resource access is denied</response>
        /// <response code="500">Due to server problems, it`s not possible to get your data now</response>

        [HttpPost]
        [Authorize(Roles = "stockist, manager")]
        public IActionResult Post([FromBody] BookVO book)
        {
            if (book == null) return BadRequest("We were unable to insert this book.");
            BookVO books = _bookBusiness.Create(book);

            if (books == null) return BadRequest("We were unable to insert this book.");
            
            return Ok(_bookBusiness.Create(book));
        }

        /// <summary>
        /// Change information for a specific book. *Requires authentication*
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /api/book/v1
        ///     {
        ///        "id": "2"    
        ///        "isbn": "9788502180260",
        ///        "price": 55,60,
        ///        "category": "Programação",
        ///        "amount":2
        ///     }
        ///
        /// </remarks>
        /// <returns>Book with updated information</returns>
        /// <response code="200">Success in updating book information</response>
        /// <response code="204">No Content</response>
        /// <response code="400">Business logic error, see return message for more info</response>
        /// <response code="401">Unauthorized. Token not present, invalid or expired</response>
        /// <response code="403">Resource access is denied</response>
        /// <response code="500">Due to server problems, it`s not possible to get your data now</response>

        [HttpPut]
        [Authorize(Roles = "stockist, manager")]
        public IActionResult Put([FromBody] BookVO book)
        {

            if (book == null) return BadRequest("Check book information");

            var updatedBook = _bookBusiness.Update(book);

            if (updatedBook == null) return BadRequest("It was not possible to update the data in this book.");
            return new ObjectResult(updatedBook);
        }

        /// <summary>
        /// Deletes a specific book. *Requires authentication level manager*
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE /api/book/v1/{id}
        ///
        /// </remarks>
        /// <returns>Action confirmation message</returns>
        ///<param name = "id">Book id.</param>
        /// <response code="200">Book successfully deleted</response>
        /// <response code="204">No Content</response>
        /// <response code="400">Business logic error, see return message for more info</response>
        /// <response code="401">Unauthorized. Token not present, invalid or expired</response>
        /// <response code="403">Resource access is denied</response>
        /// <response code="500">Due to server problems, it`s not possible to get your data now</response>

        [HttpDelete("{id}")]
    //    [Authorize(Roles = "stockist, manager")]
        public IActionResult Delete(long id)
        {
            _bookBusiness.Delete(id);
            return Ok("Book delete with sucess!");
        }
    }
}
