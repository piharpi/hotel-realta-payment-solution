using System.Text.Json.Serialization;

namespace HotelRealtaPayment.Contract.Models
{
    public class TransactionDto
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int id { get; set; }
        public string transactionNumber  { get; set; }
        public DateTime modifiedDate { get; set; }
        public decimal debet { get; set; }
        public decimal credit { get; set; }
        public string note { get; set; }
        public string orderNumber { get; set; }
        public string sourceId { get; set; }
        public string targetId { get; set; }
        public string? transactionRef { get; set; }
        public string userName { get; set; }
        public string type { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int userId { get; set; }
    }
}
