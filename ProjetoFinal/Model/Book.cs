using System.ComponentModel.DataAnnotations.Schema;
using Solutis.Model.Base;
using System.Collections.Generic;
namespace Solutis.Model
{
    [Table("books")]
    public class Book : BaseEntity
    {

        [Column("isbn")]
        public string Isbn { set; get; }

        [Column("price")]
        public float Price { set; get; }

        [Column("category")]
        public string Category { set; get; }

        [NotMapped]
        public List<string> Authors { set; get; }

        [NotMapped]
        public string Title { set; get; }

        [NotMapped]
        public string Description { set; get; }


    }
}