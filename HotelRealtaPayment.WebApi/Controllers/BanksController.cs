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
        private IRepositoryManager _repoManager;
        private ILoggerManager _logger;

        public BanksController(IRepositoryManager repoManager, ILoggerManager logger)
        {
            _repoManager = repoManager;
            _logger = logger;
        }

        // GET: api/<BanksController>
        [HttpGet]
        public IActionResult Get()
        {
            var banks = _repoManager.BankRepository
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
                    banks = banks
                }
            });
        }

        // GET api/<BanksController>/5
        [HttpGet("{id}", Name = "GetBank")]
        public string Get(int id)
        {
            return "value";
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
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<BanksController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
