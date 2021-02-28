using Solutis.Model.Base;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using Solutis.Services;

namespace Solutis.Model
{

    public class Login
    {
        public string Username { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
        public string CreateToken { get; set; }

        public Login(User user)
        {
            this.Username = user.Username;
            this.Role = user.Role;
            this.Token = (DateTime.Today).ToString();
            this.CreateToken = SecurityService.GenerateToken(user);
        }

    }
}