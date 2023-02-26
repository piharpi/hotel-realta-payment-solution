using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelRealtaPayment.Domain.Entities
{
    [Table("bank")]
    public class Bank
    {
        [Key]
        [ForeignKey("entity")]
        public int BankEntityId { get; set; }
        public string BankCode { get; set;}
        public string BankName { get; set;}
        public DateTime BankModifiedDate { get; set;} = DateTime.Now;
    }
}
