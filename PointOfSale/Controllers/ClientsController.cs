using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PointOfSale.Application.Clients.Commands.CreateClient;
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
    }
}
