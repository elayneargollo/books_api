using System.ComponentModel.DataAnnotations.Schema;
using Solutis.Model.Base;
using System.ComponentModel.DataAnnotations;
using Solutis.Data.VO;


namespace Solutis.Model
{
    [Table("purchase")]
    public partial class Purchase : BaseEntity
    {

        [Column("id_client")]
        public long idUser { get; set; }

        [Column("id_book")]
        public long idBook { get; set; }

        [Column("address")]
        [MaxLength(80)]
        public string Address { set; get; }

        [Column("email")]
        [Required(ErrorMessage = "Email is required")]
        public string Email { set; get; }

        [Column("smartphone")]
        public decimal Smartphone { set; get; }

        [NotMapped]
        public string UserName { get; set; }

        [NotMapped]
        public BookVO Book { get; set; }

    }
}