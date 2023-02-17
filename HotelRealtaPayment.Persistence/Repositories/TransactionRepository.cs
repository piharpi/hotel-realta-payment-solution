using HotelRealtaPayment.Domain.Entities;
using HotelRealtaPayment.Domain.Repositories;
using HotelRealtaPayment.Persistence.Base;
using HotelRealtaPayment.Persistence.RepositoryContext;
using System.Data;

namespace HotelRealtaPayment.Persistence.Repositories
{
    public class TransactionRepository : RepositoryBase<Transaction>, ITransactionRepository
    {
        public TransactionRepository(AdoDbContext adoContext) : base(adoContext)
        {
        }

        public int Edit(Transaction transaction)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Transaction> FindAllTransaction()
        {
            var query = @"SELECT patr_trx_number, 
				                 patr_modified_date,
				                 patr_debet,
				                 patr_credit,
				                 patr_note,
				                 patr_order_number,
				                 patr_source_id,
				                 patr_target_id,
				                 patr_trx_number_ref,
				                 patr_type,
				                 us.user_full_name
		                    FROM Payment.payment_transaction patr
                       LEFT JOIN Users.users us
			                  ON us.user_id = patr.patr_user_id";

            IEnumerator<Transaction> listOfTransaction = FindAll<Transaction>(query);

            while (listOfTransaction.MoveNext())
            {
                var data = listOfTransaction.Current;
                yield return data;
            }
        }

        public Task<IEnumerable<Transaction>> FindAllTransactionAsync()
        {
            throw new NotImplementedException();
        }

        public Transaction FindTransactionById(int transactionId)
        {
            var query = @"SELECT patr_trx_number, 
				                 patr_modified_date,
				                 patr_debet,
				                 patr_credit,
				                 patr_note,
				                 patr_order_number,
				                 patr_source_id,
				                 patr_target_id,
				                 patr_trx_number_ref,
				                 patr_type,
				                 us.user_full_name
		                    FROM Payment.payment_transaction patr
                       LEFT JOIN Users.users us
			                  ON us.user_id = patr.patr_user_id
                           WHERE patr.patr_id=@id;";

            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = query,
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@id",
                        DataType = DbType.Int32,
                        Value = transactionId
                    }
                }
            };

            var listOfTransaction = FindByCondition<Transaction>(model);

            Transaction? data = listOfTransaction.Current;

            while (listOfTransaction.MoveNext())
                data = listOfTransaction.Current;

            return data;
        }

        public T Insert<T>(Transaction transaction)
        {
            throw new NotImplementedException();
        }

        public int Remove(int transactionId)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = @"DELETE FROM Payment.Payment_Transaction
                                 WHERE patr_id=@id;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@id",
                        DataType = DbType.Int32,
                        Value = transactionId
                    }
                }
            };

            var rowAffected = _adoContext.ExecuteNonQueryReturn(model);
            _adoContext.Dispose();

            return rowAffected;
        }
    }
}
