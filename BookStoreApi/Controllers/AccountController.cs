using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Solutis.Model;
using Microsoft.AspNetCore.Authorization;
using Solutis.Services;
using Solutis.Business;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using FluentValidation.Results;

namespace Solutis.Controllers
{
    [Route("/api/account")]
    public class AccountController : ControllerBase
    {
        private readonly ILogger<AccountController> _logger;
        private IUserBusiness _userBusiness;

        public AccountController(ILogger<AccountController> logger, IUserBusiness userBusiness)
        {
            _logger = logger;
            _userBusiness = userBusiness;
        }

        /// <summary>
        /// Returns the generated token for valid users.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /login
        ///     {
        ///        "username": "elayne",
        ///        "password": "elayne123"
        ///     }
        ///
        /// </remarks>
        /// <response code = "201">Authentication created and returned successfully.</response>
        /// <response code="500">Error authenticating user</response>

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Authenticate([FromBody] UserRequest model)
        {

            LoginValidation validator = new LoginValidation(model, _userBusiness);
            ValidationResult result = validator.Validate(model);

            if(!result.IsValid) return BadRequest(result.Errors[0].ErrorMessage);
            
            var user = _userBusiness.Validate(model.Username, model.Password);
            return Ok(new Login(user));
        }

        /// <summary>
        /// Search all user by. *Requires authentication*
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/account/v1/
        ///
        /// </remarks>
        /// <returns>Search Results</returns>
        /// <response code="200">System user list </response>
        /// <response code="204">No Content</response>
        /// <response code="400">Business logic error, see return message for more info</response>
        /// <response code="401">Unauthorized. Token not present, invalid or expired</response>
        /// <response code="403">Forbidden. Resource access is denied</response>
        /// <response code="500">Due to server problems, it`s not possible to get your data now</response>

        [HttpGet]
       // [Authorize(Roles = "manager, employee")]
        public IActionResult Get()
        {

            List<User> users = _userBusiness.FindAll();
            if (users == null) return BadRequest("Unable to locate users at this time");

            return Ok(users);
        }

        /// <summary>
        /// Search specific user by id. *Requires authentication*
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/account/v1/{id}
        ///     
        ///
        /// </remarks>
        /// <param name = "id">User id.</param>
        /// <response code="200">User choice</response>
        /// <response code="204">No Content</response>
        /// <response code="400">Business logic error, see return message for more info</response>
        /// <response code="401">Unauthorized. Token not present, invalid or expired</response>
        /// <response code="403">Forbidden. Resource access is denied</response>
        /// <response code="500">Due to server problems, it`s not possible to get your data now</response>

        [HttpGet("{id}")]
        // [Authorize(Roles = "manager, employee")]
        public IActionResult Get([FromRoute] long id)
        {
            var user = _userBusiness.FindByID(id);
            if (user is null) return NotFound("Unable to locate this user");

            return Ok(user);
        }

        /// <summary>
        /// Insert/create a specific user. *Requires authentication*
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/account/v1/
        ///     {
        ///        "username": "elayne",
        ///        "password": "elayne",
        ///        "role": "develop" 
        ///     }
        ///
        /// </remarks>     
        /// <response code="200">Account created</response>
        /// <response code="204">No Content</response>
        /// <response code="400">Business logic error, see return message for more info</response>
        /// <response code="401">Unauthorized. Token not present, invalid or expired</response>
        /// <response code="403">Resource access is denied</response>
        /// <response code="500">Due to server problems, it`s not possible to get your data now</response> 

        [HttpPost]
     //   [Authorize(Roles = "manager, employee")]
        public IActionResult Post([FromBody] User user)
        {
            UserValidation validator = new UserValidation(user);
            ValidationResult result = validator.Validate(user);

            if(!result.IsValid) return BadRequest(result.Errors[0].ErrorMessage);
            
            return Ok(_userBusiness.Create(user));

        }

        /// <summary>
        /// Change information for a specific user. *Requires authentication*
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /api/account/v1/
        ///     {
        ///        "id": "2"    
        ///        "username": "elayne",
        ///        "password": "elayne",
        ///        "role": "develop" 
        ///     }
        ///
        /// </remarks>
        /// <response code="200">User information successfully changed</response>
        /// <response code="204">No Content</response>
        /// <response code="400">Business logic error, see return message for more info</response>
        /// <response code="401">Token not present, invalid or expired</response>
        /// <response code="403">Resource access is denied</response>
        /// <response code="500">Due to server problems, it`s not possible to get your data now</response>

        [HttpPut]
        // [Authorize(Roles = "manager")]
        public IActionResult Put([FromBody] User user)
        {

            UserUpdateValidation validator = new UserUpdateValidation(user, _userBusiness);
            ValidationResult result = validator.Validate(user);

            if(!result.IsValid) return BadRequest(result.Errors[0].ErrorMessage);

            return new ObjectResult( _userBusiness.Update(user));
        }


        /// <summary>
        /// Deletes a specific user. *Requires authentication level manager*
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE /api/account/v1/{id}
        ///
        /// </remarks>
        /// <param name = "id">Id user delet</param>
        /// <response code="200">User successfully deleted</response>
        /// <response code="204">No Content</response>
        /// <response code="400">Business logic error, see return message for more info</response>
        /// <response code="401">Unauthorized. Token not present, invalid or expired</response>
        /// <response code="403">Resource access is denied</response>
        /// <response code="500">Due to server problems, it`s not possible to get your data now</response>

        [HttpDelete("{id}")]
     //   [Authorize(Roles = "manager")]
        public IActionResult Delete(long id)
        {
            _userBusiness.Delete(id);
            return Ok("User delete with sucess!");
        }

    }
}