using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PointOfSale.Application.Stores.Commands.CreateStore;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace PointOfSale.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StoresController : ControllerBase
    {
        private readonly ISender _sender;

        public StoresController(ISender sender)
        {
            _sender = sender;
        }

        /// <summary>
        /// Создание магазина
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(int), Status200OK)]
        [ProducesResponseType(Status400BadRequest)]
        [ProducesResponseType(Status404NotFound)]
        public async Task<ActionResult<int>> Create(CreateStoreCommand command)
        {
            var createdStoreId = await _sender.Send(command);

            return createdStoreId;
        }
    }
}