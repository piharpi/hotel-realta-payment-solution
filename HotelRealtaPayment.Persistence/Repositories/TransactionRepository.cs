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
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = @"INSERT INTO [Payment].[payment_transaction]
                                       ([patr_trx_number]
                                       ,[patr_debet]
                                       ,[patr_credit]
                                       ,[patr_type]
                                       ,[patr_note]
                                       ,[patr_modified_date]
                                       ,[patr_order_number]
                                       ,[patr_source_id]
                                       ,[patr_target_id]
                                       ,[patr_trx_number_ref]
                                       ,[patr_user_id])
                                 VALUES
                                       (CONCAT(@transactionNumber, IDENT_CURRENT('Payment.[payment_transaction]'))
                                       ,@debet
                                       ,@credit
                                       ,@type
                                       ,@note
                                       ,GETDATE()
                                       ,@order_number
                                       ,@src_id
                                       ,@trg_id
                                       ,@order_number_ref
                                       ,@user_id);",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@transactionNumber",
                        DataType = DbType.String,
                        Value = $"{transaction.patr_type}#{DateTime.Now.ToString("yyyyMMdd")}-"
                    },
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@credit",
                        DataType = DbType.Decimal,
                        Value = transaction.patr_credit
                    },
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@debet",
                        DataType = DbType.Decimal,
                        Value = transaction.patr_debet
                    },
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@type",
                        DataType = DbType.String,
                        Value = transaction.patr_type
                    },
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@note",
                        DataType = DbType.String,
                        Value = transaction.patr_note
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@src_id",
                        DataType = DbType.String,
                        Value = transaction.patr_source_id
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@trg_id",
                        DataType = DbType.String,
                        Value = transaction.patr_target_id
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@order_number",
                        DataType = DbType.String,
                        Value = transaction.patr_order_number
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@order_number_ref",
                        DataType = DbType.String,
                        IsNullable = true,
                        Value = transaction.patr_trx_number_ref
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@user_id",
                        DataType = DbType.Int64,
                        Value = transaction.patr_user_id
                    }
                }
            };

            return Create<T>(model);
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
