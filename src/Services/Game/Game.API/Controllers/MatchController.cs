using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Game.API.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Game.API.Controllers
{
    public class MatchController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMatchQueries _matchQueries;

        public MatchController(IMediator mediator, IMatchQueries matchQueries)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _matchQueries = matchQueries ?? throw new ArgumentNullException(nameof(mediator));
        }


        [Route("{orderId:int}")]
        [HttpGet]
        [ProducesResponseType(typeof(Match), (int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetMatch(int orderId)
        {
            try
            {
                var order = await _matchQueries.GetMatchAsync(orderId);
                return Ok(order);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }


        [Route("")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<OrderSummary>), (int) HttpStatusCode.OK)]
        public async Task<IActionResult> GetOrders()
        {
            var orders = await _matchQueries.GetMatchesAsync();

            return Ok(orders);
        }
    }
}