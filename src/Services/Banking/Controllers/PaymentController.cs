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
    public class PaymentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public PaymentController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet(Name = "GetPayment")]
        [ProducesResponseType(typeof(IEnumerable<Payment>), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(PaginatedItemsViewModel<Payment>), (int) HttpStatusCode.OK)]
        public async Task<IActionResult> Get([FromQuery] int offset, [FromQuery] int limit)
        {
            if (offset != 0 || limit != 0)
            {
                var payments = _unitOfWork.PaymentRepository.GetRange(offset, limit);
                return Ok(new PaginatedItemsViewModel<Payment>(offset, limit, payments.Count(), payments));
            }
            return Ok(_unitOfWork.PaymentRepository.GetAll());
        }

        [HttpGet("{id}", Name = "GetPaymentById")]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Payment), (int) HttpStatusCode.OK)]
        public async Task<IActionResult> Get(Guid id)
        {
            var payment = _unitOfWork.PaymentRepository.Get(id);
            if (payment != null)
            {
                return Ok(payment);
            }
            return NotFound();
        }

        [HttpPost("{id}", Name = "PostPayment")]
        [ProducesResponseType((int) HttpStatusCode.Created)]
        public async Task<IActionResult> Post(Guid id, [FromBody] Payment payment)
        {
            var client = _unitOfWork.ClientRepository.Get(id);
            if (client == null)
            {
                return NotFound();
            }
            client.Payments.Add(payment);
            _unitOfWork.ClientRepository.Update(client);
            _unitOfWork.Complete();
            return CreatedAtAction(nameof(Get), new {id}, null);
        }


        [HttpPut("{id}", Name = "PutPayment")]
        [ProducesResponseType((int) HttpStatusCode.Created)]
        public async Task<IActionResult> Put(Guid id, [FromBody] Payment newPayment)
        {
            var payment = _unitOfWork.PaymentRepository.Get(id);
            if (payment == null)
            {
                return NotFound();
            }

            payment.Due = newPayment.Due;
            payment.From = newPayment.From;
            payment.Transaction = newPayment.Transaction;

            _unitOfWork.PaymentRepository.Update(payment);
            _unitOfWork.Complete();
            return CreatedAtAction(nameof(Get), new {id}, null);
        }

        [HttpDelete("{id}", Name = "RemovePayment")]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        public async Task<IActionResult> Remove(Guid id)
        {
            var payment = _unitOfWork.PaymentRepository.Get(id);

            if (payment == null)
            {
                return NotFound();
            }
            _unitOfWork.PaymentRepository.Remove(payment);
            _unitOfWork.Complete();
            return NoContent();
        }
    }
}