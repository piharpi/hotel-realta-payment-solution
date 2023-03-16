namespace HotelRealtaPayment.Domain.RequestFeatures;

public abstract class RequestParameters
{
    private const int MaxPageSize = 20;
    private int _pageSize = 5;
    private int _pageNumber;

    public int PageNumber { get; set; } = 1;

    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
    }
}