using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PointOfSale.Application.Models;
using PointOfSale.Application.Products.Commands.CreateProduct;
using PointOfSale.Application.Products.Commands.UpdateProduct;
using PointOfSale.Application.Products.Queries.GetAllClientProducts;
using PointOfSale.Application.Products.Queries.GetProductByGTIN;
using PointOfSale.Application.Products.Queries.GetProductById;
using PointOfSale.Application.Products.Queries.GetQuantityOfClientProductsSold;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace PointOfSale.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ISender _sender;

        public ProductsController(ISender sender)
        {
            _sender = sender;
        }

        /// <summary>
        /// Создание продукта
        /// </summary>
        [HttpPost]
        [ProducesResponseType(Status200OK)]
        [ProducesResponseType(Status400BadRequest)]
        [ProducesResponseType(Status404NotFound)]
        public async Task<ActionResult<int>> Create(CreateProductCommand command)
        {
            var createdProductId = await _sender.Send(command);

            return createdProductId;
        }

        /// <summary>
        /// Получение продукта по идентификатору
        /// </summary>
        [HttpGet("{productId}")]
        [ProducesResponseType(Status200OK)]
        [ProducesResponseType(Status400BadRequest)]
        [ProducesResponseType(Status404NotFound)]
        public async Task<ActionResult<ProductModel>> GetById(int productId)
        {
            var product = await _sender.Send(new GetProductByIdQuery
            {
                Id = productId
            });

            return product;
        }

        /// <summary>
        /// Получение продукта по GTIN
        /// </summary>
        [HttpGet("ByGTIN/{productGTIN}")]
        [ProducesResponseType(Status200OK)]
        [ProducesResponseType(Status400BadRequest)]
        [ProducesResponseType(Status404NotFound)]
        public async Task<ActionResult<ProductModel>> GetByGTIN(string productGTIN)
        {
            var product = await _sender.Send(new GetProductByGTINQuery
            {
                GTIN = productGTIN
            });

            return product;
        }

        /// <summary>
        /// Получение всех продуктов клиента
        /// </summary>
        [HttpGet("AllClientProducts/{clientId}")]
        [ProducesResponseType(Status200OK)]
        [ProducesResponseType(Status400BadRequest)]
        [ProducesResponseType(Status404NotFound)]
        public async Task<ActionResult<List<ProductModel>>> GetAllClientProducts(int clientId)
        {
            var clientProducts = await _sender.Send(new GetAllClientProductsQuery
            {
                ClientId = clientId
            });

            return clientProducts;
        }

        /// <summary>
        /// Получение количества проданных продуктов клиента
        /// </summary>
        [HttpGet("Clients/{clientId}/QuantityOfClientProductsSold")]
        [ProducesResponseType(Status200OK)]
        [ProducesResponseType(Status400BadRequest)]
        [ProducesResponseType(Status404NotFound)]
        public async Task<ActionResult<List<QuantityOfProductSoldModel>>> GetQuantityOfClientProductsSold(int clientId)
        {
            var quantityOfClientProductSoldModels = await _sender.Send(
                new GetQuantityOfClientProductsSoldQuery
                {
                    ClientId = clientId
                });

            return quantityOfClientProductSoldModels;
        }

        /// <summary>
        /// Обновление продукта
        /// </summary>
        [HttpPatch]
        [ProducesResponseType(Status204NoContent)]
        [ProducesResponseType(Status400BadRequest)]
        [ProducesResponseType(Status404NotFound)]
        public async Task<IActionResult> Update(UpdateProductCommand command)
        {
            await _sender.Send(command);

            return new NoContentResult();
        }
    }
}