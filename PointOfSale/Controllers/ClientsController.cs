using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PointOfSale.Application.Clients.Commands.CreateClient;
using PointOfSale.Application.Clients.Queries.GetClientById;
using PointOfSale.Application.Models;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace PointOfSale.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly ISender _sender;

        public ClientsController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost]
        [ProducesResponseType(typeof(int), Status200OK)]
        [ProducesResponseType(Status400BadRequest)]
        public async Task<ActionResult<int>> Create(CreateClientCommand command)
        {
            var createdClientId = await _sender.Send(command);

            return createdClientId;
        }

        [HttpGet("{clientId}")]
        [ProducesResponseType(typeof(ClientModel), Status200OK)]
        [ProducesResponseType(Status400BadRequest)]
        [ProducesResponseType(Status404NotFound)]
        public async Task<ActionResult<ClientModel>> GetById(int clientId)
        {
            var client = await _sender.Send(new GetClientByIdQuery
            {
                Id = clientId
            });

            return client;
        }
    }
}