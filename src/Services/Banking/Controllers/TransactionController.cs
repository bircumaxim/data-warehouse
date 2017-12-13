using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Banking.Data.Entitites;
using Banking.Data.UnitOfWork;
using Banking.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Banking.Controllers
{
    [Route("api/[controller]")]
    public class TransactionController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public TransactionController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet(Name = "GetTransaction")]
        [ProducesResponseType(typeof(IEnumerable<Transaction>), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(PaginatedItemsViewModel<Transaction>), (int) HttpStatusCode.OK)]
        public async Task<IActionResult> Get([FromQuery] int offset, [FromQuery] int limit)
        {
            IEnumerable<Transaction> transactions;
            
            if (offset != 0 || limit != 0)
            {
                transactions = _unitOfWork.TransactionRepository.GetRange(offset, limit);
                return Ok(new PaginatedItemsViewModel<Transaction>(offset, limit, transactions.Count(), transactions));
            }

            transactions = _unitOfWork.TransactionRepository.GetAll();
            return Ok(transactions);
        }

        [HttpGet("{id}", Name = "GetTransactionById")]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Payment), (int) HttpStatusCode.OK)]
        public async Task<IActionResult> Get(Guid id)
        {
            var transaction = _unitOfWork.TransactionRepository.Get(id);
            if (transaction != null)
            {
                return Ok(transaction);
            }
            return NotFound();
        }

        [HttpPost("{id}", Name = "PostTransaction")]
        [ProducesResponseType((int) HttpStatusCode.Created)]
        public async Task<IActionResult> Post(Guid id, [FromBody] Transaction transaction)
        {
            var payment = _unitOfWork.PaymentRepository.Get(id);
            if (payment == null)
            {
                return NotFound();
            }
            payment.Transaction = transaction;
            _unitOfWork.PaymentRepository.Update(payment);
            _unitOfWork.Complete();

            return CreatedAtAction(nameof(Get), new {id}, null);
        }


        [HttpPut("{id}", Name = "PutTransaction")]
        [ProducesResponseType((int) HttpStatusCode.Created)]
        public async Task<IActionResult> Put(Guid id, [FromBody] Transaction newTransaction)
        {
            var transaction = _unitOfWork.TransactionRepository.Get(id);
            if (transaction == null)
            {
                return NotFound();
            }

            transaction.Amount = newTransaction.Amount;
            transaction.TaxRate = newTransaction.TaxRate;
            transaction.TransactionTime = newTransaction.TransactionTime;

            _unitOfWork.TransactionRepository.Update(transaction);
            _unitOfWork.Complete();
            return CreatedAtAction(nameof(Get), new {id}, null);
        }

        [HttpDelete("{id}", Name = "RemoveTransaction")]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        public async Task<IActionResult> Remove(Guid id)
        {
            var transaction = _unitOfWork.TransactionRepository.Get(id);

            if (transaction == null)
            {
                return NotFound();
            }
            _unitOfWork.TransactionRepository.Remove(transaction);
            _unitOfWork.Complete();
            return NoContent();
        }
    }
}