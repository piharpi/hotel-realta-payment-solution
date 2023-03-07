using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelRealtaPayment.Domain.Entities
{
    [Table("bank")]
    public class Bank
    {
        [Key]
        [ForeignKey("entity")]
        public int Id { get; set; }
        public string? Code { get; set;}
        public string? Name { get; set;}
        public DateTime ModifiedDate { get; set; } = DateTime.Now;
    }
}
