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
            const string query = @"SELECT bank_entity_id Id, 
                                          bank_code Code, 
                                          bank_name Name
                                     FROM Payment.Bank";

            var listOfBank = FindAll<Bank>(query);

            //var listOfBank = GetAll<Bank>(query);

            //return listOfBank;
            while (listOfBank.MoveNext())
                yield return listOfBank.Current;
        }

        public int Edit(Bank bank)
        {
            var model = new SqlCommandModel()
            {
                CommandText = @"UPDATE Payment.Bank
                                   SET bank_code = @code, 
                                       bank_name = @name, 
                                       bank_modified_date = @date
                                 WHERE bank_entity_id = @id;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[]
                {
                    new()
                    {
                        ParameterName = "@id",
                        DataType = DbType.Int32,
                        Value = bank.Id
                    },
                    new()
                    {
                        ParameterName = "@code",
                        DataType = DbType.String,
                        Value = bank.Code
                    },
                    new()
                    {
                        ParameterName = "@name",
                        DataType = DbType.String,
                        Value = bank.Name
                    },
                    new()
                    {
                        ParameterName = "@date",
                        DataType = DbType.DateTime,
                        Value = bank.ModifiedDate
                    }
                }
            };

            var bankId = _adoContext.ExecuteNonQueryReturn(model);
            _adoContext.Dispose();

            return bankId;
        }

        public Task<IEnumerable<Bank>> FindAllBankAsync()
        {
            throw new NotImplementedException();
        }

        public Bank FindBankById(int bankId)
        {
            var model = new SqlCommandModel()
            {
                CommandText = @"SELECT bank_entity_id Id, 
                                       bank_code Code, 
                                       bank_name Name,
                                       bank_modified_date ModifiedDate
                                  FROM Payment.Bank 
                                 WHERE bank_entity_id = @id;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[]
                {
                    new()
                    {
                        ParameterName = "@id",
                        DataType = DbType.Int32,
                        Value = bankId
                    }
                }
            };

            var listOfBank = FindByCondition<Bank>(model);

            var data = listOfBank.Current;

            while (listOfBank.MoveNext())
                data = listOfBank.Current;

            return data;
        }

        public T Insert<T>(Bank bank)
        {
            var model = new SqlCommandModel()
            {
                CommandText = @"INSERT INTO Payment.Bank (bank_code, bank_name)
                                VALUES (@code, @name);",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[]
                {
                    new()
                    {
                        ParameterName = "@code",
                        DataType = DbType.String,
                        Value = bank.Code
                    },
                    new()
                    {
                        ParameterName = "@name",
                        DataType = DbType.String,
                        Value = bank.Name
                    }
                }
            };

            return Create<T>(model);
        }

        public int Remove(int bankId)
        {
            var model = new SqlCommandModel()
            {
                CommandText = "DELETE FROM Payment.Bank WHERE bank_entity_id = @id;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[]
                {
                    new()
                    {
                        ParameterName = "@id",
                        DataType = DbType.Int32,
                        Value = bankId
                    }
                }
            };

            var rowsAffected = _adoContext.ExecuteNonQueryReturn(model);
            _adoContext.Dispose();

            return rowsAffected;
        }
    }
}