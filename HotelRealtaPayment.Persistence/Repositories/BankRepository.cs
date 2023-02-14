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

        public int Edit(Bank bank)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = @"UPDATE Payment.Bank
                                   SET bank_code=@code, 
                                       bank_name=@name, 
                                       bank_modified_date=@date
                                 WHERE bank_entity_id= @id;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@id",
                        DataType = DbType.Int32,
                        Value = bank.bank_entity_id
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@code",
                        DataType = DbType.String,
                        Value = bank.bank_code
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@name",
                        DataType = DbType.String,
                        Value = bank.bank_name
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@date",
                        DataType = DbType.DateTime,
                        Value = bank.bank_modified_date
                    }
                }
            };

            var rowsAffected = _adoContext.ExecuteNonQueryReturn(model);
            _adoContext.Dispose();

            return rowsAffected;
        }

        public Task<IEnumerable<Bank>> FindAllBankAsync()
        {
            throw new NotImplementedException();
        }

        public Bank FindBankById(int bankId)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = @"SELECT bank_entity_id, bank_code, bank_name, bank_modified_date 
                                  FROM Payment.Bank 
                                 WHERE bank_entity_id = @id;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@id",
                        DataType = DbType.Int32,
                        Value = bankId
                    }
                }
            };

            var listOfBank = FindByCondition<Bank>(model);

            Bank? data = listOfBank.Current;

            while (listOfBank.MoveNext())
                data = listOfBank.Current;

            return data;
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

        public int Remove(int bankId)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "DELETE FROM Payment.Bank WHERE bank_entity_id=@id;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@id",
                        DataType = DbType.Int32,
                        Value = bankId
                    }
                }
            };

            var rowAffected = _adoContext.ExecuteNonQueryReturn(model);
            _adoContext.Dispose();

            return rowAffected;
        }
    }
}
