using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Solutis.Model;
using Microsoft.AspNetCore.Authorization;
using Solutis.Services;
using Solutis.Business;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace Solutis.Controllers
{
    [Route("v1/account")]
    
    public class LoginController : ControllerBase
    {
        private readonly ILogger<LoginController> _logger;
        private IUserBusiness _userBusiness;

        public LoginController(ILogger<LoginController> logger, IUserBusiness userBusiness)
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
        ///        "password": "123",
        ///     }
        ///
        /// </remarks>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Authenticate([FromBody] User model)
        {
            var user = _userBusiness.Validate(model.Username, model.Password);

            if (user == null) return NotFound(new { message = "Usuário ou senha inválidos" });
            var token = "";
            await Task.Run(() => token = TokenService.GenerateToken(user));

            if (token == null) return Unauthorized();

            return new
            {
                user = user.Username,
                role = user.Role,
                createToken = (DateTime.Today).ToString(),
                token = token
            };

        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Get()
        {

            List<User> users =  _userBusiness.FindAll();
            if (users == null) return BadRequest();

            return Ok(users);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public IActionResult Get([FromRoute] long id)
        {
            var user = _userBusiness.FindByID(id);
            if(user == null) return BadRequest();

            return Ok(user);
        }

        /// <summary>
        /// Insert/create a specific user. *Requires authentication*
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /user
        ///     {
        ///        "username": "elayne",
        ///        "password": "elayne",
        ///        "role": "develop" 
        ///     }
        ///
        /// </remarks>      
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [Authorize(Roles = "manager, employee,develop")]
        public IActionResult Post([FromBody] User user)
        {
            if (user == null) return BadRequest();
            return Ok(_userBusiness.Create(user));
        }

        /// <summary>
        /// Change information for a specific user. *Requires authentication*
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /user
        ///     {
        ///        "id": "2"    
        ///        "username": "elayne",
        ///        "password": "elayne",
        ///        "role": "develop" 
        ///     }
        ///
        /// </remarks>
        [HttpPut]
        [ProducesResponseType((200), Type = typeof(User))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [Authorize(Roles = "manager, employee")]
        public IActionResult Put([FromBody] User user)
        {

            if (user == null) return BadRequest();

            var updatedUser = _userBusiness.Update(user);

            if (updatedUser == null) return BadRequest();
            return new ObjectResult(updatedUser);
        }


        /// <summary>
        /// Deletes a specific user. *Requires authentication level manager*
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE /book/v1/2
        ///
        /// </remarks>
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [Authorize(Roles = "manager")]
        public IActionResult Delete(long id)
        {
            _userBusiness.Delete(id);
            return Ok("User delete with sucess!");
        }


    }
}