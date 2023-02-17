using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelRealtaPayment.Domain.Entities
{
    [Table("user_accounts")]
    public class Transaction
    {
        [Key]
        public int patr_id { get; set; }
        public string patr_trx_number { get; set; }
        public decimal patr_debet { get; set; }
        public decimal patr_credit { get; set; }
        public string patr_type { get; set; }
        public string patr_note { get; set; }
        public DateTime patr_modified_date { get; set; }
        public string patr_order_number { get; set; }
        public string patr_source_id { get; set; }
        public string patr_target_id { get; set; }
        public string? patr_trx_number_ref { get; set; }

        [ForeignKey("users")]
        public int patr_user_id { get; set; }

        public string user_full_name { get; set; }
    }
}
