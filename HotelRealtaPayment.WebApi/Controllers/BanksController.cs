using HotelRealtaPayment.Contract.Models;
using HotelRealtaPayment.Domain.Base;
using HotelRealtaPayment.Domain.Entities;
using HotelRealtaPayment.Services.Abstraction;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HotelRealtaPayment.WebApi.Controllers
{
    [Route("api/banks")]
    [ApiController]
    public class BanksController : ControllerBase
    {
        private readonly IRepositoryManager _repoManager;
        private readonly ILoggerManager _logger;

        public BanksController(IRepositoryManager repoManager, ILoggerManager logger)
        {
            _repoManager = repoManager;
            _logger = logger;
        }

        // GET: api/<BanksController>
        [HttpGet]
        public IActionResult Get(string? name)
        {
            var b = _repoManager.BankRepository
                .FindAllBank()
                .Select(b => new BankDto
                {
                    Id = b.Id,
                    Code = b.Code,
                    Name = b.Name
                });

            if (!string.IsNullOrEmpty(name))
                b = b.Where(bank => bank.Name.ToLower().Contains(name.ToLower()));

            return Ok(new
            {
                status = "success",
                data = new
                {
                    banks = b
                }
            });
        }

        // GET api/<BanksController>/5
        [HttpGet("{id}", Name = "GetBank")]
        public IActionResult Get(int id)
        {
            var b = _repoManager.BankRepository.FindBankById(id);

            try
            {
                var bankDto = new BankDto
                {
                    Id = b.Id,
                    Code = b.Code,
                    Name = b.Name,
                    ModifiedDate = b.ModifiedDate
                };

                return Ok(new
                {
                    status = "success",
                    data = new
                    {
                        bank = bankDto
                    }
                });
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        // POST api/<BanksController>
        [HttpPost]
        public IActionResult Post([FromBody] BankDto bankDto)
        {
            var bank = new Bank()
            {
                Code = bankDto.Code,
                Name = bankDto.Name,
            };

            var id = _repoManager.BankRepository.Insert<int>(bank);

            return CreatedAtRoute("GetBank", new { id },
                new
                {
                    status = "success",
                    message = "Create bank successfully.",
                    data = new
                    {
                        idBank = id
                    }
                }
            );
        }

        // PUT api/<BanksController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] BankDto bankDto)
        {
            var bank = new Bank()
            {
                Id = id,
                Code = bankDto.Code,
                Name = bankDto.Name,
            };

            var rows = _repoManager.BankRepository.Edit(bank);

            if (rows == 0)
                return NotFound();

            return Ok(
                new
                {
                    status = "success",
                    message = "Edit bank successfully.",
                    data = new
                    {
                        idBank = id
                    }
                }
            );
        }

        // DELETE api/<BanksController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var rows = _repoManager.BankRepository.Remove(id);

            if (rows == 0)
            {
                return NotFound();
            }

            return Ok(new
            {
                status = "success",
                message = "Delete bank successfully.",
            });
        }
    }
}