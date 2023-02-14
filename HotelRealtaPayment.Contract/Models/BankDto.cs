using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace HotelRealtaPayment.Contract.Models
{
    public class BankDto
    {
        public int id { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(55)]
        public string code { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(55)]
        public string name { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public DateTime modifiedDate { get; set; } 
    }
}