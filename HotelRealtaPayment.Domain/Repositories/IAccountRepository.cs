using HotelRealtaPayment.Domain.Entities;

namespace HotelRealtaPayment.Domain.Repositories
{
    public interface IAccountRepository
    {
        IEnumerable<Account> FindAllAccount();
        Task<IEnumerable<Account>> FindAllAccountAsync();
        Account FindAccountById(int accountId);
        T Insert<T>(Account account);
        int Edit(Account account);
        int Remove(int accountId);
    }
}
