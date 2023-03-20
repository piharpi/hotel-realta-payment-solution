using HotelRealtaPayment.Contract.Models;
using HotelRealtaPayment.Domain.Base;
using HotelRealtaPayment.Domain.Entities;
using HotelRealtaPayment.Domain.RequestFeatures;
using HotelRealtaPayment.Services.Abstraction;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HotelRealtaPayment.WebApi.Controllers
{
    [Route("api/accounts")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private IRepositoryManager _repoManager;
        private ILoggerManager _logger;

        public AccountsController(IRepositoryManager repoManager, ILoggerManager logger)
        {
            _repoManager = repoManager;
            _logger = logger;
        }

        // GET: api/<AccountsController>
        [HttpGet]
        public IActionResult Get()
        {
            var a = _repoManager.AccountRepository
                .FindAllAccount()
                .Select(a => new AccountDto
                {
                    Number = a.AccountNumber,
                    CodeName = a.CodeName,
                    Saldo = a.Saldo,
                    Type = a.Type
                });

            return Ok(new
            {
                status = "success",
                data = new
                {
                    accounts = a
                }
            });
        }
        
        // GET: api/<AccountsController>
        [HttpGet("payments")]
        public IActionResult GetAllPayment()
        {
            var p = _repoManager.AccountRepository
                .GetAllPayment()
                .Select(a => new PaymentDto
                {
                    Id = a.Id,
                    Code = a.Code,
                    Name = a.Name
                    
                });

            return Ok(new
            {
                status = "success",
                data = new
                {
                    payments = p
                }
            });
        }
        
        // GET: api/<AccountsController>/pageslist
        [HttpGet("pagelist")]
        public async Task<IActionResult> GetAccountPageList([FromQuery] AccountParameters accountParameters)
        {
            var accounts = await _repoManager.AccountRepository.GetAccountPageList(accountParameters);

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(accounts.MetaData));
            var ac = accounts
                .Select(a => new AccountDto
                {
                    Number = a.AccountNumber,
                    CodeName = a.CodeName,
                    Saldo = a.Saldo,
                    Type = a.Type
                });

            return Ok(new
            {
                status = "success",
                data = new
                {
                    accounts = ac
                }
            });
        }
        
        [HttpGet("users/{id}/pagelist", Name = "GetAccountDetailList")]
        public async Task<IActionResult> GetAccountDetailList([FromQuery] AccountParameters accountParameters, int id)
        {
            var accounts = await _repoManager.AccountRepository.GetAccountDetailPageList(accountParameters, id);

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(accounts.MetaData));
            var ac = accounts
                .Select(a => new AccountDto
                {
                    Id = a.Id,
                    Number = a.AccountNumber,
                    UserId = a.UserId,
                    EntityId = a.EntityId,
                    CodeName = a.CodeName,
                    Saldo = a.Saldo,
                    Type = a.Type,
                    ExpMonth = a.Expmonth,
                    ExpYear = a.Expyear
                });

            return Ok(new
            {
                status = "success",
                data = new
                {
                    accounts = ac
                }
            });
        }

        // GET api/<AccountsController>/5
        [HttpGet("{id}", Name = "GetAccount")]
        public IActionResult Get(int id)
        {
            var b = _repoManager.AccountRepository.FindAccountById(id);

            try
            {
                var accountDto = new AccountDto
                {
                    Number = b.AccountNumber,
                    CodeName = b.CodeName,
                    Saldo = b.Saldo,
                    ModifiedDate = b.ModifiedDate,
                    Type = b.Type,
                    ExpMonth = b.Expmonth,
                    ExpYear = b.Expyear,
                };

                return Ok(new
                {
                    status = "success",
                    data = new
                    {
                        account = accountDto
                    }
                });
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
        
        // GET api/<AccountsController>/users/5
        [HttpGet("users/{id}", Name = "GetUserAccountInfo")]
        public IActionResult GetUserAccountInfo(int id)
        {
            var a = _repoManager.AccountRepository.FindAccountByUserId(id);
        
            return Ok(new
            {
                status = "success",
                data = new
                {
                    accounts = a.ToList()
                }
            });
        }

        // POST api/<AccountsController>
        [HttpPost]
        public IActionResult Post([FromBody] AccountDto accountDto)
        {
            var account = new Account()
            {
                EntityId = accountDto.EntityId,
                UserId = accountDto.UserId,
                AccountNumber = accountDto.Number,
                Saldo = accountDto.Saldo,
                Type = accountDto.Type,
                Expmonth = accountDto.ExpMonth,
                Expyear = accountDto.ExpYear,
            };

            var id = _repoManager.AccountRepository.Insert<int>(account);

            return CreatedAtRoute("GetAccount", new { id },
                new
                {
                    status = "success",
                    message = "Create account successfully.",
                    data = new
                    {
                        idAccount = id
                    }
                }
            );
        }

        // PUT api/<AccountsController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] AccountDto accountDto)
        {
            var account = new Account()
            {
                Id = id,
                AccountNumber = accountDto.Number,
                Saldo = accountDto.Saldo,
                Type = accountDto.Type,
                Expmonth = accountDto.ExpMonth,
                Expyear = accountDto.ExpYear,
                EntityId = accountDto.EntityId
            };

            var rows = _repoManager.AccountRepository.Edit(account);

            if (rows == 0)
                return NotFound();

            return CreatedAtRoute("GetAccount", new { id },
                new
                {
                    status = "success",
                    message = "Edit account successfully.",
                    data = new
                    {
                        idAccount = id
                    }
                });
        }

        // DELETE api/<AccountsController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var rows = _repoManager.AccountRepository.Remove(id);

            if (rows == 0)
                return NotFound();

            return Ok(new
            {
                status = "success",
                message = "Delete account successfully.",
            });
        }
    }
}