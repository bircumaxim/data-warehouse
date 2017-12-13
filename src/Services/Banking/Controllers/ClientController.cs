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
    public class ClientController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ClientController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet(Name = "GetClient")]
        [ProducesResponseType(typeof(IEnumerable<Client>), (int) HttpStatusCode.OK)]
        [ProducesResponseType(typeof(PaginatedItemsViewModel<Client>), (int) HttpStatusCode.OK)]
        public async Task<IActionResult> Get([FromQuery] int offset = 0, [FromQuery] int limit = 0)
        {
            IEnumerable<Client> clients;
            
            if (offset != 0 || limit != 0)
            {
                clients = _unitOfWork.ClientRepository.GetRange(offset, limit);
                return Ok(new PaginatedItemsViewModel<Client>(offset, limit, clients.Count(), clients));
            }

            clients = _unitOfWork.ClientRepository.GetAll();
            return Ok(clients);
        }

        [HttpGet("{id}", Name = "GetClientById")]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Client), (int) HttpStatusCode.OK)]
        public async Task<IActionResult> Get(Guid id)
        {
            var client = _unitOfWork.ClientRepository.Get(id);
            if (client != null)
            {
                return Ok(client);
            }
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

            _unitOfWork.ClientRepository.Add(newClient);
            _unitOfWork.Complete();

            return CreatedAtAction(nameof(Get), new {id = newClient.Id}, null);
        }

        [HttpPut("{id}", Name = "PutClient")]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [ProducesResponseType((int) HttpStatusCode.Created)]
        public async Task<IActionResult> Put(Guid id, [FromBody] Client newClient)
        {
            var client = _unitOfWork.ClientRepository.Get(id);
            if (client == null)
            {
                return NotFound();
            }

            client.FirstName = newClient.FirstName;
            client.LastName = newClient.LastName;
            client.Cnp = newClient.Cnp;
            _unitOfWork.ClientRepository.Update(client);
            _unitOfWork.Complete();
            return CreatedAtAction(nameof(Get), new {id}, null);
        }

        [HttpDelete("{id}", Name = "RemoveClient")]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        public async Task<IActionResult> Remove(Guid id)
        {
            var client = _unitOfWork.ClientRepository.Get(id);

            if (client == null)
            {
                return NotFound();
            }
            _unitOfWork.ClientRepository.Remove(client);
            _unitOfWork.Complete();
            return NoContent();
        }
    }
}