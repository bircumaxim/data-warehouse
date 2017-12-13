using System;
using System.Collections.Generic;
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
                return Ok(new PaginatedItemsViewModel<Payment>(offset, limit, limit - offset,
                    _unitOfWork.PaymentRepository.GetRange(offset, limit)));
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

        [HttpPost(Name = "PostPayment")]
        [ProducesResponseType((int) HttpStatusCode.Created)]
        public async Task<IActionResult> Post([FromBody] Payment payment)
        {
            var newPayment = new Payment
            {
                Id = Guid.NewGuid(),
                Client = payment.Client
            };

            _unitOfWork.PaymentRepository.Add(newPayment);
            _unitOfWork.Complete();

            return CreatedAtAction(nameof(Get), new {id = newPayment.Id}, null);
        }



    }
}