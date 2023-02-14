using HotelRealtaPayment.Domain.Entities;
using HotelRealtaPayment.Domain.Repositories;
using HotelRealtaPayment.Persistence.Base;
using HotelRealtaPayment.Persistence.RepositoryContext;
using System.Data;

namespace HotelRealtaPayment.Persistence.Repositories
{
    public class FintechRepository : RepositoryBase<Fintech>, IFintechRepository
    {
        public FintechRepository(AdoDbContext adoDbContext) : base(adoDbContext)
        { 
        }

        public IEnumerable<Fintech> FindAllFintech()
        {
            var query = @"SELECT paga_entity_id, paga_code, paga_name 
                            FROM Payment.Payment_Gateway";

            IEnumerator<Fintech> listOfFintech = FindAll<Fintech>(query);

            while (listOfFintech.MoveNext())
            {
                var data = listOfFintech.Current;
                yield return data;
            }
        }

        public int Edit(Fintech fintech)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = @"UPDATE Payment.Payment_Gateway
                                   SET paga_code=@code, 
                                       paga_name=@name, 
                                       paga_modified_date=@date
                                 WHERE paga_entity_id= @id;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@id",
                        DataType = DbType.Int32,
                        Value = fintech.paga_entity_id
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@code",
                        DataType = DbType.String,
                        Value = fintech.paga_code
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@name",
                        DataType = DbType.String,
                        Value = fintech.paga_name
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@date",
                        DataType = DbType.DateTime,
                        Value = fintech.paga_modified_date
                    }
                }
            };

            var rowsAffected = _adoContext.ExecuteNonQueryReturn(model);
            _adoContext.Dispose();

            return rowsAffected;
        }

        public Task<IEnumerable<Fintech>> FindAllFintechAsync()
        {
            throw new NotImplementedException();
        }

        public Fintech FindFintechById(int fintechId)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = @"SELECT paga_entity_id, paga_code, paga_name, paga_modified_date 
                                  FROM Payment.Payment_Gateway 
                                 WHERE paga_entity_id = @id;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@id",
                        DataType = DbType.Int32,
                        Value = fintechId
                    }
                }
            };

            var listOfFintech = FindByCondition<Fintech>(model);

            Fintech? data = listOfFintech.Current;

            while (listOfFintech.MoveNext())
                data = listOfFintech.Current;

            return data;
        }

        public T Insert<T>(Fintech fintech)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = @"INSERT INTO Payment.Payment_Gateway (paga_code, paga_name)
                                VALUES (@code, @name);",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@code",
                        DataType = DbType.String,
                        Value = fintech.paga_code
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@name",
                        DataType = DbType.String,
                        Value = fintech.paga_name
                    }
                }
            };

            return Create<T>(model);
        }

        public int Remove(int fintechId)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = @"DELETE FROM Payment.Payment_Gateway 
                                 WHERE paga_entity_id=@id;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@id",
                        DataType = DbType.Int32,
                        Value = fintechId
                    }
                }
            };

            var rowAffected = _adoContext.ExecuteNonQueryReturn(model);
            _adoContext.Dispose();

            return rowAffected;
        }
    }
}
