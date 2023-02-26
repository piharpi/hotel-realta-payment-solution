using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelRealtaPayment.Domain.Entities
{
    [Table("user_accounts")]
    public class Account
    {
        [Key]
        [ForeignKey("entity")]
        public int UsacEntityId { get; set; }

        [ForeignKey("users")]
        public int UsacUserId { get; set; }
        public string? CodeName { get; set; }
        public string UsacAccountNumber { get; set; }
        public decimal UsacSaldo { get; set; }
        public string UsacType { get; set; }
        public byte? UsacExpmonth { get; set; }
        public Int16? UsacExpyear { get; set; }
        public DateTime? UsacModifiedDate { get; set; } = DateTime.Now;
    }
}
