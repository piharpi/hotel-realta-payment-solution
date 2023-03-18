using HotelRealtaPayment.Contract.Models.FrontEnd;

namespace HotelRealtaPayment.Contract.Models;

public class TransactionRepaymentDto : TransactionRefundDto
{
    public string? CardNumber { get; set; }
}