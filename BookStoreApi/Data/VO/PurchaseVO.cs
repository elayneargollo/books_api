
namespace Solutis.Data.VO
{
    public class PurchaseVO
    {
        public string User { get; set; }
        public BookVO Book { get; set; }
        public string Address { set; get; }
        public string Email { set; get; }
        public decimal Smartphone { set; get; }
        public long Id { set; get; }
        public long idUser { get; set; }
        public long idBook { get; set; }
    }

}
