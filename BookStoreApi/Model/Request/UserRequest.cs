using Solutis.Model.Base;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Solutis.Model
{
    public class UserRequest
    {
        
        [Column("user_name")]
        [MaxLength(50)]
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }

        [Column("password")]
        [MaxLength(50)]
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

    }
}