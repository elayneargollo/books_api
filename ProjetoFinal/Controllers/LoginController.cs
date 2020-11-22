using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Solutis.Model;
using Microsoft.AspNetCore.Authorization;
using Solutis.Services;
using Solutis.Business;
using Microsoft.Extensions.Logging;
using System;

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
        public async Task<ActionResult<dynamic>> Authenticate([FromBody]User model)
        {
            var user = _userBusiness.Validate(model.Username, model.Password);

            if (user == null) return NotFound(new { message = "Usuário ou senha inválidos" });
           // var token = TokenService.GenerateToken(user);
            var token = "";
            await Task.Run(() => token =  TokenService.GenerateToken(user));
        
            if(token==null) return Unauthorized();

            return new
            {
                user = user.Username,
                role = user.Role,
                createToken = (DateTime.Today).ToString(),
                token = token
            };
        
        }
    }
}