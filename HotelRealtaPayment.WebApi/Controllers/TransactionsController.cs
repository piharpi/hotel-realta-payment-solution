using HotelRealtaPayment.Contract.Models;
using HotelRealtaPayment.Domain.Base;
using HotelRealtaPayment.Domain.Entities;
using HotelRealtaPayment.Persistence.Base;
using HotelRealtaPayment.Services.Abstraction;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HotelRealtaPayment.WebApi.Controllers
{
    [Route("api/transactions")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly IRepositoryManager _repoManager;
        private readonly ILoggerManager _logger;

        public TransactionsController(IRepositoryManager repoManager, ILoggerManager logger)
        {
            _repoManager = repoManager;
            _logger = logger;
        }


        // GET: api/<TransactionsController>
        [HttpGet]
        public IActionResult Get()
        {
            var t = _repoManager.TransactionRepository
                    .FindAllTransaction()
                    .Select(t => new TransactionDto()
                    {
                        transactionNumber = t.patr_trx_number,
                        modifiedDate = t.patr_modified_date,
                        debet = t.patr_debet,
                        credit = t.patr_credit,
                        note = t.patr_note,
                        orderNumber = t.patr_order_number,
                        sourceId = t.patr_source_id,
                        targetId = t.patr_target_id,
                        transactionRef = t.patr_trx_number_ref,
                        type = t.patr_type,
                        userName = t.user_full_name
                    });

            return Ok(new
            {
                status = "success",
                data = new
                {
                    transactions = t
                }
            });
        }

        // GET api/<TransactionsController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var t = _repoManager.TransactionRepository.FindTransactionById(id);

            if (t == null)
                return NotFound();

            var transactionDto = new TransactionDto
            {
                transactionNumber = t.patr_trx_number,
                modifiedDate = t.patr_modified_date,
                debet = t.patr_debet,
                credit = t.patr_credit,
                note = t.patr_note,
                orderNumber = t.patr_order_number,
                sourceId = t.patr_source_id,
                targetId = t.patr_target_id,
                transactionRef = t.patr_trx_number_ref,
                type = t.patr_type,
                userName = t.user_full_name
            };

            return Ok(new
            {
                status = "success",
                data = new
                {
                    transaction = transactionDto
                }
            });
        }

        // POST api/<TransactionsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<TransactionsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TransactionsController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var rows = _repoManager.TransactionRepository.Remove(id);

            if (rows == 0)
                return NotFound();

            return Ok(new
            {
                status = "success",
                message = "Delete bank successfully.",
            });
        }
    }
}
