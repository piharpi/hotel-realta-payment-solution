using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelRealtaPayment.Domain.Entities
{
    [Table("user_accounts")]
    public class Transaction
    {
        [Key] public int PatrId { get; set; }
        public string? PatrTrxNumber { get; set; }
        public decimal PatrDebet { get; set; }
        public decimal PatrCredit { get; set; }
        public string PatrType { get; set; }
        public string PatrNote { get; set; }
        public DateTime PatrModifiedDate { get; set; }
        public string PatrOrderNumber { get; set; }
        public string PatrSourceId { get; set; }
        public string PatrTargetId { get; set; }
        public string? PatrTrxNumberRef { get; set; }

        [ForeignKey("users")] public int PatrUserId { get; set; }

        public string UserFullName { get; set; }
    }
}