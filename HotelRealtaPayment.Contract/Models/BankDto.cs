using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace HotelRealtaPayment.Contract.Models
{
    public class BankDto
    {
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(4)]
        [JsonPropertyName("code")]
        public string Code { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(55)]
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("modifiedDate")]
        public DateTime? ModifiedDate { get; set; } 
    }
}