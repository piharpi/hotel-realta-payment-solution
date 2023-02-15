using HotelRealtaPayment.Contract.Models;
using HotelRealtaPayment.Domain.Base;
using HotelRealtaPayment.Domain.Entities;
using HotelRealtaPayment.Persistence.RepositoryContext;
using HotelRealtaPayment.Services.Abstraction;
using Microsoft.AspNetCore.Mvc;
using System.Data;

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
                   number = a.usac_account_number,
                   codeName = a.code_name,
                   saldo = a.usac_saldo,
                   type = a.usac_type
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

        // GET api/<AccountsController>/5
        [HttpGet("{id}", Name = "GetAccount")]
        public IActionResult Get(int id)
        {
            var b = _repoManager.AccountRepository.FindAccountById(id);

            if (b == null)
                return NotFound();

            var accountDto = new AccountDto
            {
                number = b.usac_account_number,
                codeName = b.code_name,
                saldo = b.usac_saldo,
                modifiedDate = b.usac_modified_date,
                type = b.usac_type,
                expMonth = b.usac_expmonth,
                expYear = b.usac_expyear,
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

        // POST api/<AccountsController>
        [HttpPost]
        public IActionResult Post([FromBody] AccountDto accountDto)
        {
            var account = new Account()
            {
                usac_user_id = accountDto.userId,
                usac_account_number = accountDto.number,
                usac_entity_id = accountDto.entityId,
                usac_saldo = accountDto.saldo,
                usac_type = accountDto.type,
                usac_expmonth = accountDto.expMonth,
                usac_expyear = accountDto.expYear,
            };

            var id = _repoManager.AccountRepository.Insert<int>(account);

            return CreatedAtRoute("GetAccount", new { id = id },
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
                usac_entity_id = id,
                usac_account_number = accountDto.number,
                usac_saldo = accountDto.saldo,
                usac_type = accountDto.type,
                usac_expmonth = accountDto.expMonth,
                usac_expyear = accountDto.expYear,
            };

            var rows = _repoManager.AccountRepository.Edit(account);

            if (rows == 0)
                return NotFound();

            return CreatedAtRoute("GetAccount", new { id = id },
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
