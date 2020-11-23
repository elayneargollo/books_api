using Solutis.Model.Base;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
namespace Solutis.Model
{
    [Table("user")]

    public class User : BaseEntity
    {
        
        [Column("user_name")]
        public string Username { get; set; }
        
    //    [JsonIgnore]
        [Column("password")]
        public string Password { get; set; }
        
        [Column("role")]
        public string Role { get; set; }
    }
}