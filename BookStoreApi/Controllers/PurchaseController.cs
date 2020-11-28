using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Solutis.Business;
using Microsoft.AspNetCore.Authorization;
using Solutis.Data.VO;
using System.Collections.Generic;

namespace Solutis.Controllers
{
    [Produces("application/json")]
    [ApiVersion("1")]
    [Route("api/[controller]/v{version:apiVersion}")]
    [ApiController]
    public class PurchaseController : ControllerBase
    {

        private readonly ILogger<PurchaseController> _logger;
        private IPurchaseBusiness _purchaseBusiness;


        public PurchaseController(ILogger<PurchaseController> logger, IPurchaseBusiness purchaseBusiness)
        {
            _logger = logger;
            _purchaseBusiness = purchaseBusiness;

        }

        /// <summary>
        /// Returns a list of purchase. *Requires authentication*
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /purchase/book/v1/
        ///     
        /// </remarks>  
        /// <returns>All purchases made</returns>
        /// <response code="200">System shopping list</response>
        /// <response code="204">No Content</response>
        /// <response code="400">Business logic error, see return message for more info</response>
        /// <response code="401">Unauthorized. Token not present, invalid or expired</response>
        /// <response code="403">Resource access is denied</response>
        /// <response code="500">Due to server problems, it`s not possible to get your data now</response>

        [HttpGet]
        [Authorize(Roles = "seller, manager")]
        public IActionResult Get()
        {

            List<PurchaseVO> purchase = new List<PurchaseVO>();
            purchase = _purchaseBusiness.FindAll();
            if (purchase == null) return BadRequest();

            return Ok(purchase);

        }

        /// <summary>
        /// Search specific purchase by id. *Requires authentication*
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /purchase/book/v1/{id}
        ///     
        /// </remarks>  
        ///<param name = "id">Purchase id.</param>
        /// <returns>Search Results</returns>
        /// <response code="200">Purchase data with chosen id</response>
        /// <response code="204">No Content</response>
        /// <response code="400">Business logic error, see return message for more info</response>
        /// <response code="401">Unauthorized. Token not present, invalid or expired</response>
        /// <response code="403">Resource access is denied</response>
        /// <response code="500">Due to server problems, it`s not possible to get your data now</response>

        [HttpGet("{id}")]
        [Authorize(Roles = "seller, manager")]
        public IActionResult Get([FromRoute] long id)
        {

            var purchase = _purchaseBusiness.FindByID(id);
            if (purchase == null) return BadRequest("Couldn't find this purchase");

            return Ok(purchase);
        }

        /// <summary>
        /// Insert a new purchase. *Requires authentication*
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST api/purchase/v1/
        ///     {
        ///        "address": "Rua Otaviano Costa",
        ///        "email": "elayne@gmail.com",
        ///        "smartphone": 991789549,
        ///        "idUser": 1,
        ///        "idBook": 1
        ///     }
        ///
        /// </remarks>
        /// <returns>Purchase created</returns>
        /// <response code="200">I successfully created a new purchase</response>
        /// <response code="204">No Content</response>
        /// <response code="400">Business logic error, see return message for more info</response>
        /// <response code="401">Unauthorized. Token not present, invalid or expired</response>
        /// <response code="403">Resource access is denied</response>
        /// <response code="500">Due to server problems, it`s not possible to get your data now</response>

        [HttpPost]
        [Authorize(Roles = "manager, employee")]
        public IActionResult Post([FromBody] PurchaseVO purchase)
        {
            if (purchase == null) return BadRequest("Not making this purchase");
            return Ok(_purchaseBusiness.Create(purchase));
        }

        /// <summary>
        /// Change information for a specific purchase. *Requires authentication*
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT api/purchase/v1/
        ///     {
        ///        "idUser": "2"    
        ///        "idBook": "13",
        ///        "Address": "Rua Otaviano Costa",
        ///        "Email": "elayne@teste.com.br",
        ///        "Smartphone": 99199765 
        ///     }
        ///
        /// </remarks>
        /// <returns>Returns purchase changed</returns>
        /// <response code="200">Purchase changed successfully</response>
        /// <response code="204">No Content</response>
        /// <response code="400">Business logic error, see return message for more info</response>
        /// <response code="401">Unauthorized. Token not present, invalid or expired</response>
        /// <response code="403">Resource access is denied</response>
        /// <response code="500">Due to server problems, it`s not possible to get your data now</response>

        [HttpPut]
        [Authorize(Roles = "manager, employee")]
        public IActionResult Put([FromBody] PurchaseVO purchase)
        {

            if (purchase == null) return BadRequest("Check purchase information");

            var updatedPurchase = _purchaseBusiness.Update(purchase);

            if (updatedPurchase == null) return BadRequest("It was not possible to update the data in this purchase.");
            return new ObjectResult(updatedPurchase);
        }

        /// <summary>
        /// Deletes a specific purchase. *Requires authentication level manager*
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE api/purchase/v1/{id}
        ///
        /// </remarks>
        ///<param name = "id">Purchase id.</param>
        /// <returns>Successful purchase message deleted</returns>
        /// <response code="200">Purchase successfully deleted</response>
        /// <response code="204">No Content</response>
        /// <response code="400">Business logic error, see return message for more info</response>
        /// <response code="401">Unauthorized. Token not present, invalid or expired</response>
        /// <response code="403">Resource access is denied</response>
        /// <response code="500">Due to server problems, it`s not possible to get your data now</response>

        [HttpDelete("{id}")]
        [Authorize(Roles = "manager")]
        public IActionResult Delete(long id)
        {
            _purchaseBusiness.Delete(id);
            return Ok("Purchase delete with sucess!");
        }
    }
}
