namespace HotelRealtaPayment.Contract.Models.FrontEnd;

public class JsonCollection<T>
{
    public string? status { get; set; }
    public Dictionary<string, List<T>> data { get; set; }
}