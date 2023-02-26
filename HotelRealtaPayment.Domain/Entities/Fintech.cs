using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelRealtaPayment.Domain.Entities
{
    [Table("payment_gateway")]
    public class Fintech
    {
        [Key]
        [ForeignKey("entity")]
        public int PagaEntityId { get; set; }
        public string PagaCode { get; set;}
        public string PagaName { get; set;}
        public DateTime PagaModifiedDate { get; set;} = DateTime.Now;
    }
}
