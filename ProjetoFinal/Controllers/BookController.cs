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
    [Route("api/[controller]/v{version:apiVersion}")] // api/person/v1
    [ApiController]
    public class BookController : ControllerBase
    {

        private readonly ILogger<BookController> _logger;
        private IBookBusiness _bookBusiness;
        private IBookRepository _repository;

        public BookController(ILogger<BookController> logger, IBookBusiness bookBusiness, IBookRepository repository)
        {
            _logger = logger;
            _bookBusiness = bookBusiness;
            _repository = repository;

        }


        /// <summary>
        /// Search all book by.
        /// </summary>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Get()
        {

            List<BookVO> books = new List<BookVO>();
            books = _bookBusiness.FindAll();
            if(books == null) return BadRequest();

            List<Book> livros = new List<Book>();
            livros = _repository.getInfoAllBook(books);
            if(livros == null) return BadRequest();

             return Ok(livros);

        }

        /// <summary>
        /// Search specific book by id.
        /// </summary>
        [HttpGet("{id}")]
        [AllowAnonymous]
        public IActionResult Get([FromRoute] long id)
        {
            var book = _bookBusiness.FindByID(id);
            if(book == null) return BadRequest();

            Book livro = new Book();
            livro = _repository.getInfoOneBook(book);
            if(livro == null) return BadRequest();

             return Ok(livro);
        }

        /// <summary>
        /// Insert/create a specific book. *Requires authentication*
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /book
        ///     {
        ///        "isbn": "9788502180260",
        ///        "price": "45",
        ///        "category": "Programação" 
        ///     }
        ///
        /// </remarks>      
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [Authorize(Roles = "manager, employee")]
        public IActionResult Post([FromBody] BookVO book)
        {
            if (book == null) return BadRequest();
            return Ok(_bookBusiness.Create(book));
        }

        /// <summary>
        /// Change information for a specific book. *Requires authentication*
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /book
        ///     {
        ///        "id": "2"    
        ///        "isbn": "9788502180260",
        ///        "price": "55,60",
        ///        "category": "Programação" 
        ///     }
        ///
        /// </remarks>
        [HttpPut]
        [ProducesResponseType((200), Type = typeof(BookVO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [Authorize(Roles = "manager, employee")]
        public IActionResult Put([FromBody] BookVO book)
        {

            if (book == null) return BadRequest();

            var updatedBook = _bookBusiness.Update(book);

            if (updatedBook == null) return BadRequest();
            return new ObjectResult(updatedBook);
        }

        /// <summary>
        /// Deletes a specific book. *Requires authentication level manager*
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE /book/v1/2
        ///     {
        ///        "id": "2"    
        ///        "isbn": "9788502180260",
        ///        "price": "55,60",
        ///        "category": "Programação" 
        ///     }
        ///
        /// </remarks>
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [Authorize(Roles = "manager")]
        public IActionResult Delete(long id)
        {
            _bookBusiness.Delete(id);
            return Ok("Book delete with sucess!");
        }
    }
}
