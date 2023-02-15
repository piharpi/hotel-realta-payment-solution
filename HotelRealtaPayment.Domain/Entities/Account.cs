using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelRealtaPayment.Domain.Entities
{
    [Table("user_accounts")]
    public class Account
    {
        [Key]
        [ForeignKey("entity")]
        public int usac_entity_id { get; set; }

        [ForeignKey("users")]
        public int usac_user_id { get; set; }
        public string? code_name { get; set; }
        public string usac_account_number { get; set; }
        public decimal usac_saldo { get; set; }
        public string usac_type { get; set; }
        public byte? usac_expmonth { get; set; }
        public Int16? usac_expyear { get; set; }
        public DateTime? usac_modified_date { get; set; } = DateTime.Now;
    }
}
