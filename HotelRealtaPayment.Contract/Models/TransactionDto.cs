using System.Text.Json.Serialization;

namespace HotelRealtaPayment.Contract.Models
{
    public class TransactionDto
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("transactionNumber")]
        public string? TransactionNumber { get; set; }

        [JsonPropertyName("modifiedDate")] public DateTime ModifiedDate { get; set; }

        [JsonPropertyName("debet")] public decimal Debet { get; set; }

        [JsonPropertyName("credit")] public decimal Credit { get; set; }

        [JsonPropertyName("note")] public string Note { get; set; }

        [JsonPropertyName("orderNumber")] public string? OrderNumber { get; set; }

        [JsonPropertyName("sourceId")] public string SourceId { get; set; }

        [JsonPropertyName("targetId")] public string TargetId { get; set; }

        [JsonPropertyName("type")] public string Type { get; set; }

        [JsonPropertyName("transactionRef")] public string? TransactionRef { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        [JsonPropertyName("userName")]
        public string? UserName { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        [JsonPropertyName("userId")]
        public int UserId { get; set; }
    }
}