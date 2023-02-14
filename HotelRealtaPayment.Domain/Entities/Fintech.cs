using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelRealtaPayment.Domain.Entities
{
    [Table("payment_gateway")]
    public class Fintech
    {
        [Key]
        [ForeignKey("entity")]
        public int paga_entity_id { get; set; }
        public string paga_code { get; set;}
        public string paga_name { get; set;}
        public DateTime paga_modified_date { get; set;} = DateTime.Now;
    }
}
