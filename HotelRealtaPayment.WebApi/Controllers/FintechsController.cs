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
        public IActionResult Get(string? name)
        {
            var f = _repoManager.FintechRepository
                .FindAllFintech()
                .Select(f => new FintechDto
                {
                    Id = f.Id,
                    Code = f.Code,
                    Name = f.Name,
                });

            if (!string.IsNullOrEmpty(name))
                f = f.Where(fintech => fintech.Name.ToLower().Contains(name.ToLower()));

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

            try
            {
                var fintechDto = new FintechDto
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
                        fintech = fintechDto
                    }
                });
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        // POST api/<FintechsController>
        [HttpPost]
        public IActionResult Post([FromBody] FintechDto fintechDto)
        {
            var fintech = new Fintech()
            {
                Code = fintechDto.Code,
                Name = fintechDto.Name,
            };

            var id = _repoManager.FintechRepository.Insert<int>(fintech);

            return CreatedAtRoute("GetFintech", new { id },
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
                Id = id,
                Code = fintechDto.Code,
                Name = fintechDto.Name,
            };

            var rows = _repoManager.FintechRepository.Edit(fintech);

            if (rows == 0)
            {
                return NotFound();
            }


            return Ok(
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
