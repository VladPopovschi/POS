using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PointOfSale.Application.Exceptions;
using PointOfSale.Application.Interfaces.DbContexts;
using PointOfSale.Application.Models;
using PointOfSale.Domain.Entities;

namespace PointOfSale.Application.Products.Queries.GetProductByGTIN
{
    public class GetProductByGTINQueryHandler : IRequestHandler<GetProductByGTINQuery, ProductModel>
    {
        private readonly IPointOfSaleContext _pointOfSaleContext;

        public GetProductByGTINQueryHandler(IPointOfSaleContext pointOfSaleContext)
        {
            _pointOfSaleContext = pointOfSaleContext;
        }

        public async Task<ProductModel> Handle(GetProductByGTINQuery query, CancellationToken cancellationToken)
        {
            var product = await _pointOfSaleContext.Products
                .AsNoTracking()
                .SingleOrDefaultAsync(product => product.GTIN.ToUpper() == query.GTIN.ToUpper(), cancellationToken);

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

        private static void ValidateTheProduct(GetProductByGTINQuery query, Product productFromDatabase)
        {
            if (productFromDatabase == null)
            {
                throw new NotFoundException($"The Product with GTIN {query.GTIN} not found in the database");
            }
        }
    }
}