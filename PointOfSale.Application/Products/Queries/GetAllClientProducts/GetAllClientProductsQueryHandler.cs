using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PointOfSale.Application.Exceptions;
using PointOfSale.Application.Interfaces.DbContexts;
using PointOfSale.Application.Models;
using PointOfSale.Domain.Entities;

namespace PointOfSale.Application.Products.Queries.GetAllClientProducts
{
    public class GetAllClientProductsQueryHandler : IRequestHandler<GetAllClientProductsQuery, List<ProductModel>>
    {
        private readonly IPointOfSaleContext _pointOfSaleContext;

        public GetAllClientProductsQueryHandler(IPointOfSaleContext pointOfSaleContext)
        {
            _pointOfSaleContext = pointOfSaleContext;
        }

        public async Task<List<ProductModel>> Handle(GetAllClientProductsQuery query, CancellationToken cancellationToken)
        {
            var client = await _pointOfSaleContext
                .Clients
                .AsNoTracking()
                .Include(client => client.Products)
                .SingleOrDefaultAsync(client => client.Id == query.ClientId, cancellationToken);

            ValidateTheExistenceOfTheClient(query, client);

            var clientProducts = new List<ProductModel>();

            client.Products.ForEach(product => clientProducts.Add(new ProductModel
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                GTIN = product.GTIN,
                Price = product.Price,
                ImageURL = product.ImageURL,
                ClientId = product.ClientId,
                TimestampCreated = product.TimestampCreated
            }));

            return clientProducts;
        }

        private static void ValidateTheExistenceOfTheClient(GetAllClientProductsQuery query, Client clientFromDatabase)
        {
            if (clientFromDatabase == null)
            {
                throw new NotFoundException($"The Client with Id {query.ClientId} not found in the database");
            }
        }
    }
}