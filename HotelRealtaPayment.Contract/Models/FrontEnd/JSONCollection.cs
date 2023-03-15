namespace HotelRealtaPayment.Contract.Models.FrontEnd;

public class JSONCollection<T>
{
    public string? status { get; set; }
    public Dictionary<string, List<T>> data { get; set; }
}