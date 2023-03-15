﻿using HotelRealtaPayment.Contract.Models;
using HotelRealtaPayment.Domain.Base;
using HotelRealtaPayment.Domain.Entities;
using HotelRealtaPayment.Services.Abstraction;
using Microsoft.AspNetCore.Mvc;

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

        // POST api/<AccountsController>
        [HttpPost]
        public IActionResult Post([FromBody] AccountDto accountDto)
        {
            var account = new Account()
            {
                UserId = accountDto.UserId,
                AccountNumber = accountDto.Number,
                Id = accountDto.Id,
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
                Type = accountDto.Type
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