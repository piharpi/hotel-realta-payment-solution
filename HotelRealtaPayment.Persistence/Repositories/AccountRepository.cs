using HotelRealtaPayment.Domain.Entities;
using HotelRealtaPayment.Domain.Repositories;
using HotelRealtaPayment.Persistence.Base;
using HotelRealtaPayment.Persistence.RepositoryContext;

namespace HotelRealtaPayment.Persistence.Repositories
{
    internal class AccountRepository : RepositoryBase<Account>, IAccountRepository
    {
        public AccountRepository(AdoDbContext adoContext) : base(adoContext)
        {
        }
        public IEnumerable<Account> FindAllAccount()
        {
            var query = @"SELECT usac_account_number, 
                                 usac_entity_id, 
                                 usac_saldo, 
                                 usac_type
                            FROM Payment.User_Accounts";

            IEnumerator<Account> listOfAccount = FindAll<Account>(query);

            while (listOfAccount.MoveNext())
            {
                var data = listOfAccount.Current;
                yield return data;
            }
        }

        public int Edit(Account account)
        {
            throw new NotImplementedException();
        }

        public Account FindAccountById(int accountId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Account>> FindAllAccountAsync()
        {
            throw new NotImplementedException();
        }

        public T Insert<T>(Account account)
        {
            throw new NotImplementedException();
        }

        public int Remove(int accountId)
        {
            throw new NotImplementedException();
        }
    }
}
