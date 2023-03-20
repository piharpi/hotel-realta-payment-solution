using HotelRealtaPayment.Domain.Entities;
using HotelRealtaPayment.Domain.Repositories;
using HotelRealtaPayment.Persistence.Base;
using HotelRealtaPayment.Persistence.RepositoryContext;
using System.Data;
using HotelRealtaPayment.Domain.Dto;
using HotelRealtaPayment.Domain.RequestFeatures;
using HotelRealtaPayment.Persistence.Repositories.RepositoryExtensions;

namespace HotelRealtaPayment.Persistence.Repositories
{
    internal class AccountRepository : RepositoryBase<Account>, IAccountRepository
    {
        public AccountRepository(AdoDbContext adoContext) : base(adoContext)
        {
        }

        public IEnumerable<Account> FindAllAccount()
        {
            const string query = @"SELECT usac_account_number AccountNumber, 
			                     CONCAT(ba.bank_name, pg.paga_code) as CodeName,
			                     usac_saldo Saldo, usac_type Type
                            FROM Payment.User_Accounts ua
                       LEFT JOIN Payment.entity en ON usac_entity_id=entity_id
                       LEFT JOIN Payment.bank ba ON bank_entity_id=entity_id
                       LEFT JOIN Payment.payment_gateway pg ON paga_entity_id=entity_id";

            var listOfAccount = FindAll<Account>(query);

            while (listOfAccount.MoveNext())
                yield return listOfAccount.Current;
        }

        public int Edit(Account account)
        {
            const string query = @"UPDATE Payment.User_Accounts
                                      SET usac_account_number = @accountNumber,
                                          usac_modified_date = @usacModified,
                                          usac_entity_id = @entityId,
                                          usac_expmonth = @expMonth,
                                          usac_expyear = @expYear,
                                          usac_saldo = @saldo,
                                          usac_type = @type
                                    WHERE usac_id = @id;";

            var parameters = new SqlCommandParameterModel[]
            {
                new()
                {
                    ParameterName = "@id",
                    DataType = DbType.Int32,
                    Value = account.Id
                },
                new()
                {
                    ParameterName = "@accountNumber",
                    DataType = DbType.String,
                    Value = account.AccountNumber
                },
                new()
                {
                    ParameterName = "@saldo",
                    DataType = DbType.Decimal,
                    Value = account.Saldo
                },
                new()
                {
                    ParameterName = "@usacModified",
                    DataType = DbType.DateTime,
                    Value = DateTime.Now
                },
                new()
                {
                    ParameterName = "@entityId",
                    DataType = DbType.Int32,
                    Value = account.EntityId
                },
                new()
                {
                    ParameterName = "@expMonth",
                    DataType = DbType.Byte,
                    Value = account.Expmonth
                },
                new()
                {
                    ParameterName = "@expYear",
                    DataType = DbType.Int16,
                    Value = account.Expyear
                },
                new()
                {
                    ParameterName = "@type",
                    DataType = DbType.String,
                    Value = account.Type
                }
            };

            var model = new SqlCommandModel()
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
            var model = new SqlCommandModel()
            {
                CommandText = @"SELECT usac_account_number AccountNumber, usac_modified_date ModifiedDate,
			                         CONCAT(ba.bank_name, pg.paga_code) as CodeName,
			                         usac_saldo Saldo, usac_type Type, usac_expmonth Expmonth, usac_expyear Expyear
                                FROM Payment.User_Accounts ua
                           LEFT JOIN Payment.entity en ON usac_entity_id=entity_id
                           LEFT JOIN Payment.bank ba ON bank_entity_id=entity_id
                           LEFT JOIN Payment.payment_gateway pg ON paga_entity_id=entity_id
                                WHERE usac_entity_id=@id",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[]
                {
                    new()
                    {
                        ParameterName = "@id",
                        DataType = DbType.Int32,
                        Value = accountId
                    }
                }
            };

            var listOfAccount = FindByCondition<Account>(model);

            var data = listOfAccount.Current;

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
            var model = new SqlCommandModel()
            {
                CommandText = @"INSERT INTO Payment.User_Accounts 
                                (usac_entity_id, usac_user_id, usac_account_number, usac_saldo,
                                 usac_type, usac_expmonth, usac_expyear)
                                OUTPUT INSERTED.usac_id 
                                VALUES (@entityId, @userId, @accountNumber, @saldo,
                                        @type, @expmonth, @expyear);",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[]
                {
                    new()
                    {
                        ParameterName = "@entityId",
                        DataType = DbType.Int32,
                        Value = account.EntityId
                    },
                    new()
                    {
                        ParameterName = "@userId",
                        DataType = DbType.Int32,
                        Value = account.UserId
                    },
                    new()
                    {
                        ParameterName = "@accountNumber",
                        DataType = DbType.String,
                        Value = account.AccountNumber
                    },
                    new()
                    {
                        ParameterName = "@saldo",
                        DataType = DbType.Decimal,
                        Value = account.Saldo
                    },
                    new()
                    {
                        ParameterName = "@type",
                        DataType = DbType.String,
                        Value = account.Type
                    },
                    new()
                    {
                        ParameterName = "@expmonth",
                        IsNullable = true,
                        DataType = DbType.Byte,
                        Value = account.Expmonth.HasValue ? account.Expmonth : DBNull.Value
                    },
                    new()
                    {
                        ParameterName = "@expyear",
                        IsNullable = true,
                        DataType = DbType.Int16,
                        Value = account.Expyear.HasValue ? account.Expyear : DBNull.Value
                    }
                }
            };

            return Create<T>(model);
        }

        public int Remove(int accountId)
        {
            var model = new SqlCommandModel()
            {
                CommandText = "DELETE FROM Payment.User_Accounts WHERE usac_id=@id;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[]
                {
                    new()
                    {
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

        public IEnumerable<AccountUser> FindAccountByUserId(int id)
        {
            var model = new SqlCommandModel()
            {
                CommandText = @"SELECT usac_account_number AccountNumber,
                                       user_id UserId,
                                       usac_saldo Saldo,
                                       usac_type Type,
                                       payment_name PaymentName
                                  FROM Payment.fnGetUserBalance(@userId)",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[]
                {
                    new()
                    {
                        ParameterName = "@userId",
                        DataType = DbType.Int32,
                        Value = id
                    }
                }
            };

            var listOfAccount = FindByCondition<AccountUser>(model);
            
            while (listOfAccount.MoveNext())
                yield return listOfAccount.Current;
        }

        public async Task<PagedList<Account>> GetAccountPageList(AccountParameters accountParameters)
        {
            var model = new SqlCommandModel()
            {
            CommandText = @"SELECT usac_account_number AccountNumber, 
			                     CONCAT(ba.bank_name, pg.paga_code) as CodeName,
			                     usac_saldo Saldo, usac_type Type
                            FROM Payment.User_Accounts ua
                       LEFT JOIN Payment.entity en ON usac_entity_id=entity_id
                       LEFT JOIN Payment.bank ba ON bank_entity_id=entity_id
                       LEFT JOIN Payment.payment_gateway pg ON paga_entity_id=entity_id",
                CommandType = CommandType.Text,
                CommandParameters = Array.Empty<SqlCommandParameterModel>()
            };
            
            var accounts = await GetAllAsync<Account>(model);
            
            var accountSearch = accounts.AsQueryable()
                .Search(accountParameters.SearchTerm)
                .Sort(accountParameters.OrderBy);

            return PagedList<Account>.ToPagedList(accountSearch.ToList(), accountParameters.PageNumber, accountParameters.PageSize);
        }

        public async Task<PagedList<Account>> GetAccountDetailPageList(AccountParameters accountParameters, int id)
        {
            var model = new SqlCommandModel()
            {
                CommandText = @"SELECT usac_id Id,
                                       usac_account_number AccountNumber,
                                       usac_user_id UserId,
                                       usac_entity_id EntityId,
                                       CONCAT(ba.bank_name, pg.paga_code) as CodeName,
                                       usac_saldo Saldo, usac_type Type,
                                       usac_expmonth Expmonth, usac_expyear Expyear
                                    FROM Payment.User_Accounts ua
                                LEFT JOIN Payment.entity en ON usac_entity_id=entity_id
                                LEFT JOIN Payment.bank ba ON bank_entity_id=entity_id
                                LEFT JOIN Payment.payment_gateway pg ON paga_entity_id=entity_id
                                    WHERE usac_user_id=@userId",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[]
                {
                    new()
                    {
                        ParameterName = "@userId",
                        DataType = DbType.Int32,
                        Value = id
                    }
                }
            };

            var accounts = await FindByConditionAsync<Account>(model);
            
            var accountSearch = accounts.AsQueryable()
                .Search(accountParameters.SearchTerm)
                .Sort(accountParameters.OrderBy);

            return PagedList<Account>.ToPagedList(accountSearch.ToList(), accountParameters.PageNumber, accountParameters.PageSize);
        }

        public IEnumerable<Payment> GetAllPayment()
        {
            const string query = @"SELECT * FROM Payment.fnGetAllPaymentType();";

            var listOfAccount = FindAll<Payment>(query);

            while (listOfAccount.MoveNext())
                yield return listOfAccount.Current;
        }
    }
}