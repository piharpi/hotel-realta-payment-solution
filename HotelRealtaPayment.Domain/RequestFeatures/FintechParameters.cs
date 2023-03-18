namespace HotelRealtaPayment.Domain.RequestFeatures;

public class FintechParameters : RequestParameters
{
    public string? SearchTerm { get; set; } = string.Empty;
    public string? OrderBy { get; set; } = "Name";
}