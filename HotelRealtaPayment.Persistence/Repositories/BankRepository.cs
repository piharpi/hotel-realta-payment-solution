using HotelRealtaPayment.Domain.Entities;
using HotelRealtaPayment.Domain.Repositories;
using HotelRealtaPayment.Persistence.Base;
using HotelRealtaPayment.Persistence.RepositoryContext;
using System.Data;

namespace HotelRealtaPayment.Persistence.Repositories
{
    public class BankRepository : RepositoryBase<Bank>, IBankRepository
    {
        public BankRepository(AdoDbContext adoDbContext) : base(adoDbContext)
        { 
        }

        public IEnumerable<Bank> FindAllBank()
        {
            var query = "SELECT bank_entity_id, bank_code, bank_name FROM Payment.Bank";

            IEnumerator<Bank> listOfBank = FindAll<Bank>(query);

            while (listOfBank.MoveNext())
            {
                var data = listOfBank.Current;
                yield return data;
            }
        }

        public void Edit(Bank bank)
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
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = @"INSERT INTO Payment.Bank (bank_code, bank_name)
                                VALUES (@code, @name);",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@code",
                        DataType = DbType.String,
                        Value = bank.bank_code
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@name",
                        DataType = DbType.String,
                        Value = bank.bank_name
                    }
                }
            };

            return Create<T>(model);
        }

        public void Remove(Bank bank)
        {
            throw new NotImplementedException();
        }
    }
}
