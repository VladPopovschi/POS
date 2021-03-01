using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PointOfSale.Application.Models;
using PointOfSale.Application.SaleTransactions.Commands.CreateSaleTransaction;
using PointOfSale.Application.SaleTransactions.Queries.GetAllStoreSalesTransactions;
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

        /// <summary>
        /// Получение всех транзакций продажи магазина
        /// </summary>
        [HttpGet("AllStoreSalesTransactions/{storeId}")]
        [ProducesResponseType(Status200OK)]
        [ProducesResponseType(Status400BadRequest)]
        [ProducesResponseType(Status404NotFound)]
        public async Task<ActionResult<List<SaleTransactionModel>>> GetAllStoreSalesTransactions(int storeId)
        {
            var storeSaleTransactions = await _sender.Send(new GetAllStoreSalesTransactionsQuery
            {
                StoreId = storeId
            });

            return storeSaleTransactions;
        }
    }
}