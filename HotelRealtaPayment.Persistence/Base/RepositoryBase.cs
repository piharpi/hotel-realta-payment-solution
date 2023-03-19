using HotelRealtaPayment.Persistence.Interface;
using HotelRealtaPayment.Persistence.RepositoryContext;

namespace HotelRealtaPayment.Persistence.Base
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected AdoDbContext _adoContext;

        protected RepositoryBase(AdoDbContext adoContext)
        {
            _adoContext = adoContext;
        }

        public T Create<T>(SqlCommandModel model)
        {
            var dataT = _adoContext.ExecuteScalar<T>(model);
            _adoContext.Dispose();
            return dataT;
        }

        public void Delete(SqlCommandModel model)
        {
            _adoContext.ExecuteNonQuery(model);
            _adoContext.Dispose();
        }

        public IEnumerable<T> GetAll<T>(string sql)
        {
            var listOfData = _adoContext.ExecuteReader<T>(sql);
            _adoContext.Dispose();

            var list = new List<T>();

            while (listOfData.MoveNext())
            {
                list.Add(listOfData.Current);
            }

            return list;
        }

        public IEnumerator<T> FindAll<T>(string sql)
        {
            var listOfData = _adoContext.ExecuteReader<T>(sql);
            _adoContext.Dispose();
            return listOfData;
        }

        public async Task<IEnumerable<TValue>> GetAllAsync<TValue>(SqlCommandModel model)
        {
            var dataT = _adoContext.ExecuteReaderAsync<TValue>(model);
            var listData = new List<TValue>();

            while(await dataT.MoveNextAsync())
            {
                listData.Add(dataT.Current);
            }

            _adoContext.DisposeAsync();

            return listData;
        }
        
        public async Task<IEnumerable<TValue>> FindByConditionAsync<TValue>(SqlCommandModel model)
        {
            var dataT = _adoContext.ExecuteReaderAsync<TValue>(model);
            var listData = new List<TValue>();
            
            while(await dataT.MoveNextAsync())
            {
                listData.Add(dataT.Current);
            }

            _adoContext.DisposeAsync();
            return listData;
        }

        public IAsyncEnumerator<T> FindAllAsync<T>(SqlCommandModel model)
        {
            var dataT = _adoContext.ExecuteReaderAsync<T>(model);
            _adoContext.DisposeAsync();
            return dataT;
        }

        public IEnumerator<T> FindByCondition<T>(SqlCommandModel model)
        {
            var listOfData = _adoContext.ExecuteReader<T>(model);
            _adoContext.Dispose();
            return listOfData;
        }

        public void Update(SqlCommandModel model)
        {
            _adoContext.ExecuteNonQuery(model);
            _adoContext.Dispose();
        }
    }
}