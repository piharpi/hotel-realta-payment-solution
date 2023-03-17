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
        
        [HttpGet("page")]
        public async Task<IActionResult> GetTransactionPaging([FromQuery] TransactionParameters transactionParameters)
        {
            var transactions = await _repoManager.TransactionRepository.GetTransactionPaging(transactionParameters);
            var t = transactions
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
        
        // GET api/<ProductController>/5
        [HttpGet("pageList")]
        public async Task<IActionResult> GetProductPageList([FromQuery] TransactionParameters transactionParameters)
        {
            var transactions = await _repoManager.TransactionRepository.GetTransactionPageList(transactionParameters);

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(transactions.MetaData));
            var t = transactions
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

        // POST api/<TransactionsController>/topup
        [HttpPost("topup")]
        public IActionResult PostTopUp([FromBody] TransactionTopUpDto topUpDto)
        {
            var topUp = new Transaction()
            {
                PatrSourceId = topUpDto.SourceAccount,
                PatrTargetId = topUpDto.TargetAccount,
                PatrCredit = topUpDto.Amount,
                PatrUserId = topUpDto.UserId
            };

            var id = _repoManager.TransactionRepository.Transfer<int>(topUp);

            return CreatedAtRoute("GetTransaction", new { id },
                new
                {
                    status = "success",
                    message = "Create TopUp transaction successfully.",
                    data = new
                    {
                        id
                    }
                }
            );
        }
        
        // POST api/<TransactionsController>/topup
        [HttpPost("book")]
        public IActionResult PostTopUp([FromBody] TransactionBookDto bookDto)
        {
            var book = new Transaction()
            {
                PatrOrderNumber = bookDto.OrderNumber,
                PatrSourceId = bookDto.CardNumber,
                PatrUserId = bookDto.UserId
            };

            var id = _repoManager.TransactionRepository.PayBook<int>(book);

            return CreatedAtRoute("GetTransaction", new { id },
                new
                {
                    status = "success",
                    message = "Create Book transaction successfully.",
                    data = new
                    {
                        id
                    }
                }
            );
        }
        
        // [HttpPost("repayment")]
        // [HttpPost("refund")]
        // [HttpPost("order-menu")]

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