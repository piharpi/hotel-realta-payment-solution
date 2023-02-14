using HotelRealtaPayment.Domain.Repositories;

namespace HotelRealtaPayment.Domain.Base
{
    public interface IRepositoryManager
    {
        IBankRepository BankRepository { get; }
        IFintechRepository FintechRepository { get; }
    }
}
