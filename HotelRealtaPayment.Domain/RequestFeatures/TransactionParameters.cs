namespace HotelRealtaPayment.Domain.RequestFeatures;

public class TransactionParameters : RequestParameters
{
    public string? Type { get; set; } = string.Empty;
    public string? SearchTerm { get; set; } = string.Empty;

    public string? OrderBy { get; set; } = "PatrType";
}