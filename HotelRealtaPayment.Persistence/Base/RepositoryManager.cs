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
        private IFintechRepository _fintechRepository;
        private IAccountRepository _accountRepository;
        private ITransactionRepository _transactionRepository;

        public RepositoryManager(AdoDbContext adoContext)
        {
            _adoContext = adoContext;
        }

        public IBankRepository BankRepository => _bankRepository ??= new BankRepository(_adoContext);
        public IFintechRepository FintechRepository => _fintechRepository ??= new FintechRepository(_adoContext);
        public IAccountRepository AccountRepository => _accountRepository ??= new AccountRepository(_adoContext);
        public ITransactionRepository TransactionRepository => _transactionRepository ??= new TransactionRepository(_adoContext);
    }
}
