using System.Text.Json.Serialization;
namespace Solutis.Data.VO
{
    public class PurchaseVO
    {
        public string User { get; set; }
        public BookVO Book { get; set; }
        public string Address { set; get; }
        public string Email { set; get; }
        public decimal Smartphone { set; get; }

        [JsonIgnore]
        public long Id { set; get; }

        [JsonIgnore]
        public long idUser { get; set; }

        [JsonIgnore]
        public long idBook { get; set; }
    }

}
