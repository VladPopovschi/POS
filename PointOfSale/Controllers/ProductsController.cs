using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PointOfSale.Application.Models;
using PointOfSale.Application.Products.Commands.CreateProduct;
using PointOfSale.Application.Products.Queries.GetProductByGTIN;
using PointOfSale.Application.Products.Queries.GetProductById;
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
        [HttpGet("{productGTIN}")]
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
    }
}