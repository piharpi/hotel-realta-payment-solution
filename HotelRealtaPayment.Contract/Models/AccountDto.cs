using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace HotelRealtaPayment.Contract.Models
{
    public class AccountDto
    {
        [Required]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int userId { get; set; }

        [Required]
        public string number { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int entityId { get; set; }

        public string? codeName { get; set; }

        [Required]
        public decimal saldo { get; set; }

        [Required]
        public string type { get; set; }

        //[Range(0, 12)]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public byte? expMonth { get; set; }

        //[Range(0, 99)]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Int16? expYear { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public DateTime? modifiedDate { get; set; }
    }
}
