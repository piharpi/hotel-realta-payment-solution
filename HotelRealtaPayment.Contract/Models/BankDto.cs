using System.ComponentModel.DataAnnotations;

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
        public DateTime modifiedDate { get; set; } 
    }
}