using System.ComponentModel.DataAnnotations.Schema;
using Solutis.Model.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace Solutis.Model
{
    [Table("books")]
    public class Book : BaseEntity
    {

        [Column("isbn")]
        [MaxLength(13)]
        [Required(ErrorMessage = "ISBN is required")]
        public string Isbn { set; get; }

        [Column("price")]
        [Required(ErrorMessage = "Price is required")]
        public decimal Price { set; get; }

        [Column("category")]
        [MaxLength(80)]
        [Required(ErrorMessage = "Category is required")]
        public string Category { set; get; }

        [Column("title")]
        [MaxLength(80)]
        public string Title { set; get; }

        [NotMapped]
        public List<string> Authors { set; get; }

        [NotMapped]
        public string Description { set; get; }

        [Column("amount")]
        public long Amount { set; get; }

    }
}