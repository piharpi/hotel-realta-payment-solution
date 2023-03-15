using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelRealtaPayment.Domain.Entities
{
    [Table("user_accounts")]
    public class Account
    {
        [Key] [ForeignKey("entity")] public int Id { get; set; }

        [ForeignKey("users")] public int UserId { get; set; }
        public string? CodeName { get; set; }
        public string AccountNumber { get; set; }
        public decimal Saldo { get; set; }
        public string Type { get; set; }
        public byte? Expmonth { get; set; }
        public Int16? Expyear { get; set; }
        public DateTime? ModifiedDate { get; set; } = DateTime.Now;
    }
}