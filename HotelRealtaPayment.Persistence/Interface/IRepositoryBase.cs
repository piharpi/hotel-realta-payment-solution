using HotelRealtaPayment.Persistence.RepositoryContext;

namespace HotelRealtaPayment.Persistence.Interface
{
    internal interface IRepositoryBase<T>
    {
        IEnumerator<T> FindAll<T>(string sql);
        Task<IEnumerable<T>> GetAllAsync<T>(SqlCommandModel model);
        IEnumerator<T> FindByCondition<T>(SqlCommandModel model);
        Task<IEnumerable<TValue>> FindByConditionAsync<TValue>(SqlCommandModel model);
        IAsyncEnumerator<T> FindAllAsync<T>(SqlCommandModel model);
        T Create<T>(SqlCommandModel model);
        void Update(SqlCommandModel model);
        void Delete(SqlCommandModel model);
    }
}