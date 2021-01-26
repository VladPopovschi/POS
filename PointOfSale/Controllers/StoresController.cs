using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PointOfSale.Application.Models;
using PointOfSale.Application.Stores.Commands.CreateStore;
using PointOfSale.Application.Stores.Queries.GetStoreById;
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

        /// <summary>
        /// Получение магазина по идентификатору
        /// </summary>
        [HttpGet("{storeId}")]
        [ProducesResponseType(typeof(StoreModel), Status200OK)]
        [ProducesResponseType(Status400BadRequest)]
        [ProducesResponseType(Status404NotFound)]
        public async Task<ActionResult<StoreModel>> GetById(int storeId)
        {
            var store = await _sender.Send(new GetStoreByIdQuery
            {
                Id = storeId
            });

            return store;
        }
    }
}