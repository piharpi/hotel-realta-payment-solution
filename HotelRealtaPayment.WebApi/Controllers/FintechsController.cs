using HotelRealtaPayment.Contract.Models;
using HotelRealtaPayment.Domain.Base;
using HotelRealtaPayment.Domain.Entities;
using HotelRealtaPayment.Services.Abstraction;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HotelRealtaPayment.WebApi.Controllers
{
    [Route("api/fintechs")]
    [ApiController]
    public class FintechsController : ControllerBase
    {
        private IRepositoryManager _repoManager;
        private ILoggerManager _logger;

        public FintechsController(IRepositoryManager repoManager, ILoggerManager logger)
        {
            _repoManager = repoManager;
            _logger = logger;
        }

        // GET: api/<FintechsController>
        [HttpGet]
        public IActionResult Get()
        {
            var f = _repoManager.FintechRepository
                .FindAllFintech()
                .Select(f => new FintechDto
                {
                    id = f.paga_entity_id,
                    code = f.paga_code,
                    name = f.paga_name,
                });

            return Ok(new
            {
                status = "success",
                data = new
                {
                    fintechs = f
                }
            });
        }

        // GET api/<FintechsController>/5
        [HttpGet("{id}", Name = "GetFintech")]
        public IActionResult Get(int id)
        {
            var b = _repoManager.FintechRepository.FindFintechById(id);

            if (b == null)
                return NotFound();

            var fintechDto = new FintechDto
            {
                id = b.paga_entity_id,
                code = b.paga_code,
                name = b.paga_name,
                modifiedDate = b.paga_modified_date
            };

            return Ok(new
            {
                status = "success",
                data = new
                {
                    fintech = fintechDto
                }
            });
        }

        // POST api/<FintechsController>
        [HttpPost]
        public IActionResult Post([FromBody] FintechDto fintechDto)
        {
            var fintech = new Fintech()
            {
                paga_code = fintechDto.code,
                paga_name = fintechDto.name,
            };

            var id = _repoManager.FintechRepository.Insert<int>(fintech);

            return CreatedAtRoute("GetFintech", new { id = id },
            new
            {
                status = "success",
                message = "Create fintech successfully.",
                data = new
                {
                    idFintech = id
                }
            }
            );
        }

        // PUT api/<FintechsController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] FintechDto fintechDto)
        {
            var fintech = new Fintech()
            {
                paga_entity_id = id,
                paga_code = fintechDto.code,
                paga_name = fintechDto.name,
            };

            var rows = _repoManager.FintechRepository.Edit(fintech);

            if (rows == 0)
            {
                return NotFound();
            }


            return CreatedAtRoute("GetFintech", new { id = id },
            new
            {
                status = "success",
                message = "Edit fintech successfully.",
                data = new
                {
                    idFintech = id
                }
            }
            );
        }

        // DELETE api/<FintechsController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var rows = _repoManager.FintechRepository.Remove(id);

            if (rows == 0)
            {
                return NotFound();
            }

            return Ok(new
            {
                status = "success",
                message = "Delete fintech successfully.",
            });
        }
    }
}
