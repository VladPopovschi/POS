using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PointOfSale.Application.SaleTransactions.Commands.CreateSaleTransaction;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace PointOfSale.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SaleTransactionsController : ControllerBase
    {
        private readonly ISender _sender;

        public SaleTransactionsController(ISender sender)
        {
            _sender = sender;
        }

        /// <summary>
        /// Создание транзакции продажи
        /// </summary>
        [HttpPost]
        [ProducesResponseType(Status204NoContent)]
        [ProducesResponseType(Status400BadRequest)]
        [ProducesResponseType(Status404NotFound)]
        public async Task<IActionResult> Create(CreateSaleTransactionCommand command)
        {
            await _sender.Send(command);

            return new NoContentResult();
        }
    }
}