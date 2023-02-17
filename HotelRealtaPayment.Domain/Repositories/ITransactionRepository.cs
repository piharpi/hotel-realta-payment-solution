using HotelRealtaPayment.Domain.Entities;

namespace HotelRealtaPayment.Domain.Repositories
{
    public interface ITransactionRepository
    {
        IEnumerable<Transaction> FindAllTransaction();
        Task<IEnumerable<Transaction>> FindAllTransactionAsync();
        Transaction FindTransactionById(int transactionId);
        T Insert<T>(Transaction transaction);
        int Edit(Transaction transaction);
        int Remove(int transactionId);
    }
}
