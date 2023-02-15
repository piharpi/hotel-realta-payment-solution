using HotelRealtaPayment.Domain.Entities;
using HotelRealtaPayment.Domain.Repositories;
using HotelRealtaPayment.Persistence.Base;
using HotelRealtaPayment.Persistence.RepositoryContext;
using System.Data;

namespace HotelRealtaPayment.Persistence.Repositories
{
    internal class AccountRepository : RepositoryBase<Account>, IAccountRepository
    {
        public AccountRepository(AdoDbContext adoContext) : base(adoContext)
        {
        }
        public IEnumerable<Account> FindAllAccount()
        {
            var query = @"SELECT usac_account_number, 
			                     CONCAT(ba.bank_name, pg.paga_code) as code_name,
			                     usac_saldo, usac_type 
                            FROM Payment.User_Accounts ua
                       LEFT JOIN Payment.entity en ON usac_entity_id=entity_id
                       LEFT JOIN Payment.bank ba ON bank_entity_id=entity_id
                       LEFT JOIN Payment.payment_gateway pg ON paga_entity_id=entity_id";

            IEnumerator<Account> listOfAccount = FindAll<Account>(query);

            while (listOfAccount.MoveNext())
            {
                var data = listOfAccount.Current;
                yield return data;
            }
        }

        public int Edit(Account account)
        {
            string query = @"UPDATE Payment.User_Accounts
                                   SET usac_account_number=@accountNumber,
                                       usac_saldo=@saldo,
                                       usac_type=@type, 
                                       usac_expmonth=@expmonth, 
                                       usac_expyear=@expyear, 
                                       usac_modified_date=@usacModified
                                 WHERE usac_entity_id=@id;";

            var parameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@id",
                        DataType = DbType.Int32,
                        Value = account.usac_entity_id
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@accountNumber",
                        DataType = DbType.String,
                        Value = account.usac_account_number
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@saldo",
                        DataType = DbType.Decimal,
                        Value = account.usac_saldo
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@type",
                        DataType = DbType.String,
                        Value = account.usac_type
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@usacModified",
                        DataType = DbType.DateTime,
                        Value = DateTime.Now
                    },
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@expmonth",
                        DataType = DbType.Byte,
                        Value = account.usac_expmonth,
                    },
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@expyear",
                        DataType = DbType.Byte,
                        Value = account.usac_expyear,
                    }
                };

            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = query,
                CommandType = CommandType.Text,
                CommandParameters = parameters
            };

            var rowsAffected = _adoContext.ExecuteNonQueryReturn(model);
            _adoContext.Dispose();

            return rowsAffected;
        }

        public Account FindAccountById(int accountId)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = @"SELECT usac_account_number, usac_modified_date,
			                         CONCAT(ba.bank_name, pg.paga_code) as code_name,
			                         usac_saldo, usac_type, usac_expmonth, usac_expyear 
                                FROM Payment.User_Accounts ua
                           LEFT JOIN Payment.entity en ON usac_entity_id=entity_id
                           LEFT JOIN Payment.bank ba ON bank_entity_id=entity_id
                           LEFT JOIN Payment.payment_gateway pg ON paga_entity_id=entity_id
                                WHERE usac_entity_id=@id",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@id",
                        DataType = DbType.Int32,
                        Value = accountId
                    }
                }
            };

            var listOfAccount = FindByCondition<Account>(model);

            Account? data = listOfAccount.Current;

            while (listOfAccount.MoveNext())
                data = listOfAccount.Current;

            return data;
        }

        public Task<IEnumerable<Account>> FindAllAccountAsync()
        {
            throw new NotImplementedException();
        }

        public T Insert<T>(Account account)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = @"INSERT INTO Payment.User_Accounts 
                                (usac_entity_id, usac_user_id, usac_account_number, usac_saldo,
                                 usac_type, usac_expmonth, usac_expyear, usac_modified_date)
                                OUTPUT INSERTED.usac_entity_id 
                                VALUES (@entityId, @userId, @accountNumber, @saldo,
                                        @type, @expmonth, @expyear, @usacModified);",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@entityId",
                        DataType = DbType.Int32,
                        Value = account.usac_entity_id
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@userId",
                        DataType = DbType.Int32,
                        Value = account.usac_user_id
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@accountNumber",
                        DataType = DbType.String,
                        Value = account.usac_account_number
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@saldo",
                        DataType = DbType.Decimal,
                        Value = account.usac_saldo
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@type",
                        DataType = DbType.String,
                        Value = account.usac_type
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@expmonth",
                        DataType = DbType.Byte,
                        Value = account.usac_expmonth
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@expyear",
                        DataType = DbType.Byte,
                        Value = account.usac_expyear
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@usacModified",
                        DataType = DbType.DateTime,
                        Value = DateTime.Now
                    }
                }
            };

            return Create<T>(model);
        }

        public int Remove(int accountId)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "DELETE FROM Payment.User_Accounts WHERE usac_entity_id=@id;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@id",
                        DataType = DbType.Int32,
                        Value = accountId
                    }
                }
            };

            var rowAffected = _adoContext.ExecuteNonQueryReturn(model);
            _adoContext.Dispose();

            return rowAffected;
        }
    }
}
