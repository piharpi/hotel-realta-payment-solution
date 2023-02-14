﻿using HotelRealtaPayment.Persistence.RepositoryContext;

namespace HotelRealtaPayment.Persistence.Interface
{
    internal interface IRepositoryBase<T>
    {
        IEnumerator<T> FindAll<T>(string sql);
        IEnumerator<T> FindByCondition<T>(SqlCommandModel model);
        IAsyncEnumerator<T> FindAllAsync<T>(SqlCommandModel model);
        T Create<T>(SqlCommandModel model);
        void Update(SqlCommandModel model);
        void Delete(SqlCommandModel model);
    }
}