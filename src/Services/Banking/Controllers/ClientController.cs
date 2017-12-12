using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Banking.Data.Entitites;
using Banking.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Banking.Controllers
{
    [Route("api/[controller]")]
    public class ClientController : Controller
    {
        [HttpGet(Name = "GetClient")]
        [ProducesResponseType(typeof(IEnumerable<Client>), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(PaginatedItemsViewModel<Client>), (int) HttpStatusCode.OK)]
        public async Task<IActionResult> Get([FromQuery] int offset, [FromQuery] int limit)
        {
            return Ok(await GetClientAsync());
        }

        [HttpGet("{id}", Name = "GetClientById")]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Client), (int) HttpStatusCode.OK)]
        public async Task<IActionResult> GetById(String id)
        {
            Guid guid;
            if (Guid.TryParse(id, out guid)) //TODo replace
            {
                return BadRequest();
            }

            //TODO get client by guid and if not null then return it otherwise return NotFound.

            return Ok(await GetClientAsync());
            return NotFound();
        }

        [HttpPost(Name = "PostClient")]
        [ProducesResponseType((int) HttpStatusCode.Created)]
        public async Task<IActionResult> Post([FromBody] Client client)
        {
            var newClient = new Client
            {
                Id = Guid.NewGuid(),
                FirstName = client.FirstName,
                LastName = client.LastName,
                Cnp = client.Cnp
            };

            return CreatedAtAction(nameof(GetById), new {id = newClient.Id}, null);
        }

        [HttpPut("{id}", Name = "PutClient")]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [ProducesResponseType((int) HttpStatusCode.Created)]
        public async Task<IActionResult> Put(Guid id, [FromBody] Client client)
        {
            return CreatedAtAction(nameof(GetById), new {id}, null);
        }

        [HttpDelete("{id}", Name = "DeleteMovie")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Delete(Guid id)
        {
            Client client = null; //TODO retrive client from DB

            if (client == null)
            {
                return NotFound();
            }

            //TODO remove client from DB \
            return NoContent();
        }

        private static Task<IEnumerable<Client>> GetClientAsync()
        {
            return Task<IEnumerable<Client>>.Factory.StartNew(() => new List<Client>
            {
                new Client(Guid.NewGuid(), "Muhamed", "Ali", "testtesttest"),
                new Client(Guid.NewGuid(), "Muhamed", "Ali", "testtesttest")
            });
        }
    }
}