using HotelRealtaPayment.Domain.Entities;
using HotelRealtaPayment.Domain.Repositories;
using HotelRealtaPayment.Persistence.Base;
using HotelRealtaPayment.Persistence.RepositoryContext;

namespace HotelRealtaPayment.Persistence.Repositories
{
    public class BankRepository : RepositoryBase<Bank>, IBankRepository
    {
        public BankRepository(AdoDbContext adoDbContext) : base(adoDbContext)
        { 
        }

        public void Edit(Bank bank)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Bank> FindAllBank()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Bank>> FindAllBankAsync()
        {
            throw new NotImplementedException();
        }

        public Bank FindBankById(int bankId)
        {
            throw new NotImplementedException();
        }

        public T Insert<T>(Bank bank)
        {
            throw new NotImplementedException();
        }

        public void Remove(Bank bank)
        {
            throw new NotImplementedException();
        }
    }
}
