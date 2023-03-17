using System.ComponentModel.DataAnnotations;

namespace HotelRealtaPayment.Contract.Models;

public class TransactionBookOrderDto
{
    [Required] public string OrderNumber { get; set; }
    [Required] public string CardNumber { get; set; }
    [Required] public int UserId { get; set; }
}