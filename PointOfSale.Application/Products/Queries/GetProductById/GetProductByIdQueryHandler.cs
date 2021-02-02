using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PointOfSale.Application.Exceptions;
using PointOfSale.Application.Interfaces.DbContexts;
using PointOfSale.Application.Models;
using PointOfSale.Domain.Entities;

namespace PointOfSale.Application.Products.Queries.GetProductById
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductModel>
    {
        private readonly IPointOfSaleContext _pointOfSaleContext;

        public GetProductByIdQueryHandler(IPointOfSaleContext pointOfSaleContext)
        {
            _pointOfSaleContext = pointOfSaleContext;
        }

        public async Task<ProductModel> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
        {
            var product = await _pointOfSaleContext.Products
                .AsNoTracking()
                .SingleOrDefaultAsync(product => product.Id == query.Id, cancellationToken);

            ValidateTheProduct(query, product);

            return new ProductModel
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                GTIN = product.GTIN,
                Price = product.Price,
                ImageURL = product.ImageURL,
                ClientId = product.ClientId,
                TimestampCreated = product.TimestampCreated
            };
        }

        private static void ValidateTheProduct(GetProductByIdQuery query, Product productFromDatabase)
        {
            if (productFromDatabase == null)
            {
                throw new NotFoundException($"The Product with Id {query.Id} not found in the database");
            }
        }
    }
}