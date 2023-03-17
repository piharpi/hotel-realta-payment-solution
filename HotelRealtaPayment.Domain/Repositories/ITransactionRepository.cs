using HotelRealtaPayment.Contract.Models;
using HotelRealtaPayment.Domain.Entities;
using HotelRealtaPayment.Domain.RequestFeatures;

namespace HotelRealtaPayment.Domain.Repositories
{
    public interface ITransactionRepository
    {
        IEnumerable<Transaction> FindAllTransaction();
        Task<IEnumerable<Transaction>> FindAllTransactionAsync();
        Task<IEnumerable<Transaction>> GetTransactionPaging(TransactionParameters transactionParameter);
        Task<PagedList<Transaction>> GetTransactionPageList(TransactionParameters transactionParameter);
        Transaction FindTransactionById(int transactionId);
        T Insert<T>(Transaction transaction);
        T Transfer<T>(Transaction transaction);
        int Edit(Transaction transaction);
        int Remove(int transactionId);
        T PayBook<T>(Transaction book);
    }
}