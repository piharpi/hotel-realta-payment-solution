using HotelRealtaPayment.Domain.Entities;
using HotelRealtaPayment.Domain.RequestFeatures;

namespace HotelRealtaPayment.Domain.Repositories
{
    public interface IFintechRepository
    {
        IEnumerable<Fintech> FindAllFintech();
        Task<IEnumerable<Fintech>> FindAllFintechAsync();
        Fintech FindFintechById(int fintechId);
        T Insert<T>(Fintech fintech);
        int Edit(Fintech fintech);
        int Remove(int fintechId);
        Task<PagedList<Fintech>> GetTransactionPageList(FintechParameters fintechParameters);
    }
}