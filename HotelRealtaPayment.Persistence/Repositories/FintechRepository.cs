using HotelRealtaPayment.Domain.Entities;
using HotelRealtaPayment.Domain.Repositories;
using HotelRealtaPayment.Persistence.Base;
using HotelRealtaPayment.Persistence.Repositories.RepositoryExtensions;
using HotelRealtaPayment.Persistence.RepositoryContext;
using System.Data;
using HotelRealtaPayment.Domain.RequestFeatures;

namespace HotelRealtaPayment.Persistence.Repositories
{
    public class FintechRepository : RepositoryBase<Fintech>, IFintechRepository
    {
        public FintechRepository(AdoDbContext adoDbContext) : base(adoDbContext)
        {
        }

        public IEnumerable<Fintech> FindAllFintech()
        {
            const string query = @"SELECT paga_entity_id Id, 
                                          paga_code Code, 
                                          paga_name Name 
                                     FROM Payment.Payment_Gateway";

            var listOfFintech = FindAll<Fintech>(query);

            while (listOfFintech.MoveNext())
                yield return listOfFintech.Current;
        }

        public int Edit(Fintech fintech)
        {
            var model = new SqlCommandModel()
            {
                CommandText = @"UPDATE Payment.Payment_Gateway
                                   SET paga_code = @code, 
                                       paga_name = @name, 
                                       paga_modified_date = @date
                                 WHERE paga_entity_id = @id;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[]
                {
                    new()
                    {
                        ParameterName = "@id",
                        DataType = DbType.Int32,
                        Value = fintech.Id
                    },
                    new()
                    {
                        ParameterName = "@code",
                        DataType = DbType.String,
                        Value = fintech.Code
                    },
                    new()
                    {
                        ParameterName = "@name",
                        DataType = DbType.String,
                        Value = fintech.Name
                    },
                    new()
                    {
                        ParameterName = "@date",
                        DataType = DbType.DateTime,
                        Value = fintech.ModifiedDate
                    }
                }
            };

            var idFintech = _adoContext.ExecuteNonQueryReturn(model);
            _adoContext.Dispose();

            return idFintech;
        }

        public Task<IEnumerable<Fintech>> FindAllFintechAsync()
        {
            throw new NotImplementedException();
        }

        public Fintech FindFintechById(int fintechId)
        {
            var model = new SqlCommandModel()
            {
                CommandText = @"SELECT paga_entity_id Id, 
                                       paga_code Code,
                                       paga_name Name, 
                                       paga_modified_date ModifiedDate 
                                  FROM Payment.Payment_Gateway 
                                 WHERE paga_entity_id = @id;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[]
                {
                    new()
                    {
                        ParameterName = "@id",
                        DataType = DbType.Int32,
                        Value = fintechId
                    }
                }
            };

            var listOfFintech = FindByCondition<Fintech>(model);

            var data = listOfFintech.Current;

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
                CommandParameters = new SqlCommandParameterModel[]
                {
                    new()
                    {
                        ParameterName = "@code",
                        DataType = DbType.String,
                        Value = fintech.Code
                    },
                    new()
                    {
                        ParameterName = "@name",
                        DataType = DbType.String,
                        Value = fintech.Name
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
                CommandParameters = new SqlCommandParameterModel[]
                {
                    new()
                    {
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

        public async Task<PagedList<Fintech>> GetTransactionPageList(FintechParameters fintechParameters)
        {
            var model = new SqlCommandModel()
            {
                CommandText = @"SELECT paga_entity_id Id, 
                                       paga_code Code, 
                                       paga_name Name, 
                                       paga_modified_date ModifiedDate 
                                  FROM Payment.Payment_Gateway",
                CommandType = CommandType.Text,
                CommandParameters = Array.Empty<SqlCommandParameterModel>()
            };
            
            var fintechs = await GetAllAsync<Fintech>(model);
            
            var fintechSearch = fintechs.AsQueryable()
                .Search(fintechParameters.SearchTerm)
                .Sort(fintechParameters.OrderBy);

            // var totalRow = (await FindAllTransactionAsync()).Count();
            
            return PagedList<Fintech>.ToPagedList(fintechSearch.ToList(), fintechParameters.PageNumber, fintechParameters.PageSize);
        }
    }
}