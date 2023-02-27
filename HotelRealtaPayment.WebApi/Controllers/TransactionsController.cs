using HotelRealtaPayment.Contract.Models;
using HotelRealtaPayment.Domain.Base;
using HotelRealtaPayment.Domain.Entities;
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
                        TransactionNumber = t.PatrTrxNumber,
                        ModifiedDate = t.PatrModifiedDate,
                        Debet = t.PatrDebet,
                        Credit = t.PatrCredit,
                        Note = t.PatrNote,
                        OrderNumber = t.PatrOrderNumber,
                        SourceId = t.PatrSourceId,
                        TargetId = t.PatrTargetId,
                        TransactionRef = t.PatrTrxNumberRef,
                        Type = t.PatrType,
                        UserName = t.UserFullName
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
        [HttpGet("{id}", Name = "GetTransaction")]
        public IActionResult Get(int id)
        {
            var t = _repoManager.TransactionRepository.FindTransactionById(id);

            if (t == null)
                return NotFound();

            var transactionDto = new TransactionDto
            {
                TransactionNumber = t.PatrTrxNumber,
                ModifiedDate = t.PatrModifiedDate,
                Debet = t.PatrDebet,
                Credit = t.PatrCredit,
                Note = t.PatrNote,
                OrderNumber = t.PatrOrderNumber,
                SourceId = t.PatrSourceId,
                TargetId = t.PatrTargetId,
                TransactionRef = t.PatrTrxNumberRef,
                Type = t.PatrType,
                UserName = t.UserFullName
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
        [HttpPost("topup")]
        [HttpPost("transfer-booking")]
        [HttpPost("repayment")]
        [HttpPost("refund")]
        [HttpPost("order-menu")]
        public IActionResult Post([FromBody] TransactionDto transactionDto)
        {
            var transaction = new Transaction()
            {
                PatrTrxNumber = transactionDto.TransactionNumber,
                PatrDebet = transactionDto.Debet,
                PatrCredit = transactionDto.Credit,
                PatrType = transactionDto.Type,
                PatrNote = transactionDto.Note,
                PatrSourceId = transactionDto.SourceId,
                PatrTargetId = transactionDto.TargetId,
                PatrOrderNumber = transactionDto.OrderNumber,
                PatrTrxNumberRef =transactionDto.TransactionRef, 
                PatrUserId = transactionDto.UserId
            };

            var id = _repoManager.TransactionRepository.Insert<int>(transaction);

            return CreatedAtRoute("GetTransaction", new { id = id },
            new
            {
                status = "success",
                message = "Create transaction successfully.",
                data = new
                {
                    idTransaction = id
                }
            }
            );
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
