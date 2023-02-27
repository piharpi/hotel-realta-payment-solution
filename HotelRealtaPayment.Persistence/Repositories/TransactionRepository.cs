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
            var query = @"SELECT patr_trx_number PatrTrxNumber, 
				                 patr_modified_date PatrModifiedDate,
				                 patr_debet PatrDebet,
				                 patr_credit PatrCredit,
				                 patr_note PatrNote,
				                 patr_order_number PatrOrderNumber,
				                 patr_source_id PatrSourceId,
				                 patr_target_id PatrTargetId,
				                 patr_trx_number_ref PatrTrxNumberRef,
				                 patr_type PatrType,
				                 us.user_full_name UserFullName
		                    FROM Payment.payment_transaction patr
                       LEFT JOIN Users.users us
			                  ON us.user_id = patr.patr_user_id";

            IEnumerator<Transaction> listOfTransaction = FindAll<Transaction>(query);

            while (listOfTransaction.MoveNext())
                yield return listOfTransaction.Current;
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
                        Value = $"{transaction.PatrType}#{DateTime.Now.ToString("yyyyMMdd")}-"
                    },
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@credit",
                        DataType = DbType.Decimal,
                        Value = transaction.PatrCredit
                    },
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@debet",
                        DataType = DbType.Decimal,
                        Value = transaction.PatrDebet
                    },
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@type",
                        DataType = DbType.String,
                        Value = string.IsNullOrEmpty(transaction.PatrType) ? DBNull.Value : transaction.PatrType
                    },
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@note",
                        DataType = DbType.String,
                        Value = string.IsNullOrEmpty(transaction.PatrNote) ? DBNull.Value : transaction.PatrNote
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@src_id",
                        DataType = DbType.String,
                        Value = string.IsNullOrEmpty(transaction.PatrSourceId) ? DBNull.Value : transaction.PatrSourceId
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@trg_id",
                        DataType = DbType.String,
                        Value = string.IsNullOrEmpty(transaction.PatrTargetId) ? DBNull.Value : transaction.PatrTargetId
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@order_number",
                        DataType = DbType.String,
                        IsNullable = true,
                        Value = string.IsNullOrEmpty(transaction.PatrOrderNumber) ? DBNull.Value : transaction.PatrOrderNumber
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@order_number_ref",
                        DataType = DbType.String,
                        IsNullable = true,
                        Value = string.IsNullOrEmpty(transaction.PatrTrxNumberRef) ? DBNull.Value : transaction.PatrTrxNumberRef
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@user_id",
                        DataType = DbType.Int64,
                        Value = transaction.PatrUserId
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
