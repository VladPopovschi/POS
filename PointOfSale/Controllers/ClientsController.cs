using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PointOfSale.Application.Clients.Commands.CreateClient;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace PointOfSale.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly ISender _sender;

        public ClientsController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost]
        [ProducesResponseType(typeof(CreateClientCommand), Status201Created)]
        [ProducesResponseType(Status400BadRequest)]
        public async Task<IActionResult> Create(CreateClientCommand command)
        {
            // TODO Добавить фильтр исключений для приложения с логикой обработки исключения
            return new CreatedResult(string.Empty, await _sender.Send(command));
        }
    }
}
