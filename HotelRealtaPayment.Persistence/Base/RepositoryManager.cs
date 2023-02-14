using HotelRealtaPayment.Domain.Base;
using HotelRealtaPayment.Domain.Repositories;
using HotelRealtaPayment.Persistence.Repositories;
using HotelRealtaPayment.Persistence.RepositoryContext;

namespace HotelRealtaPayment.Persistence.Base
{
    public class RepositoryManager : IRepositoryManager
    {
        private AdoDbContext _adoContext;
        private IBankRepository _bankRepository;

        public RepositoryManager(AdoDbContext adoContext)
        {
            _adoContext = adoContext;
        }

        public IBankRepository BankRepository => _bankRepository ??= new BankRepository(_adoContext);
    }
}
