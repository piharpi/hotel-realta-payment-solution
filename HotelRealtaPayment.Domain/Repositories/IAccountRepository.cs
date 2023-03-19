using HotelRealtaPayment.Domain.Dto;
using HotelRealtaPayment.Domain.Entities;
using HotelRealtaPayment.Domain.RequestFeatures;

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
        IEnumerable<AccountUser> FindAccountByUserId(int id);
        Task<PagedList<Account>> GetAccountPageList(AccountParameters accountParameters);
        Task<PagedList<Account>> GetAccountDetailPageList(AccountParameters accountParameters, int id);
        IEnumerable<Payment> GetAllPayment();
    }
}