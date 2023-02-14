using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelRealtaPayment.Domain.Entities
{
    [Table("bank")]
    public class Bank
    {
        [Key]
        [ForeignKey("entity")]
        public int bank_entity_id { get; set; }
        public string bank_code { get; set;}
        public string bank_name { get; set;}
        public DateTime bank_modified_date { get; set;} = DateTime.Now;
    }
}
