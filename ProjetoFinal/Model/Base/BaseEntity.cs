using System.ComponentModel.DataAnnotations.Schema;

namespace Solutis.Model.Base
{
    public class BaseEntity
    {
        [Column("id")]
        public long Id { set; get; }
    }
}