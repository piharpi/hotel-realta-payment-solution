using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace HotelRealtaPayment.Contract.Models
{
    public class AccountDto
    {
        [Required]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        [JsonPropertyName("userId")]
        public int UserId { get; set; }

        [Required]
        [JsonPropertyName("number")]
        public string Number { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        [JsonPropertyName("entityId")]
        public int Id { get; set; }

        [JsonPropertyName("codeName")]
        public string? CodeName { get; set; }

        [Required]
        [JsonPropertyName("saldo")]
        public decimal Saldo { get; set; }

        [Required]
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [Range(0, 12)]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("expMonth")]
        public byte? ExpMonth { get; set; }

        [Range(0, 99)]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("expYear")]
        public Int16? ExpYear { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("modifiedDate")]
        public DateTime? ModifiedDate { get; set; }
    }
}
