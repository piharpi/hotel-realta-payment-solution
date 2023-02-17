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
        public IActionResult Get()
        {
            var b = _repoManager.BankRepository
                .FindAllBank()
                .Select(b => new BankDto
                {
                    id = b.bank_entity_id,
                    code = b.bank_code,
                    name = b.bank_name,
                });

            return Ok(new {
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

            if (b == null)
                return NotFound();

            var bankDto = new BankDto
            {
                id = b.bank_entity_id,
                code = b.bank_code,
                name = b.bank_name,
                modifiedDate = b.bank_modified_date
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

        // POST api/<BanksController>
        [HttpPost]
        public IActionResult Post([FromBody] BankDto bankDto)
        {
            var bank = new Bank()
            {
                bank_code = bankDto.code,
                bank_name = bankDto.name,
            };

            var id = _repoManager.BankRepository.Insert<int>(bank);

            return CreatedAtRoute("GetBank", new { id = id },
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
                bank_entity_id = id,
                bank_code = bankDto.code,
                bank_name = bankDto.name,
            };

            var rows = _repoManager.BankRepository.Edit(bank);

            if (rows == 0)
            {
                return NotFound();
            }


            return CreatedAtRoute("GetBank", new { id = id },
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

            return Ok(new {
                status = "success",
                message = "Delete bank successfully.",
            });
        }
    }
}
