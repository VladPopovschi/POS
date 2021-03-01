using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PointOfSale.Application.Exceptions;
using PointOfSale.Application.Interfaces.DbContexts;
using PointOfSale.Application.Models;

namespace PointOfSale.Application.Products.Queries.GetQuantityOfClientProductsSold
{
    public class GetQuantityOfClientProductsSoldQueryHandler
        : IRequestHandler<GetQuantityOfClientProductsSoldQuery, List<QuantityOfProductSoldModel>>
    {
        private readonly IPointOfSaleContext _pointOfSaleContext;

        public GetQuantityOfClientProductsSoldQueryHandler(IPointOfSaleContext pointOfSaleContext)
        {
            _pointOfSaleContext = pointOfSaleContext;
        }

        public async Task<List<QuantityOfProductSoldModel>> Handle(
            GetQuantityOfClientProductsSoldQuery query,
            CancellationToken cancellationToken)
        {
            await ValidateTheExistenceOfTheClient(query, cancellationToken);

            var quantityOfClientProductSoldModels = _pointOfSaleContext.Products
                .AsNoTracking()
                .Include(product => product.SaleTransactionProducts)
                .Where(product => product.ClientId == query.ClientId)
                .Select(product => new QuantityOfProductSoldModel
                {
                    ProductGTIN = product.GTIN,
                    Quantity = product.SaleTransactionProducts
                        .Sum(saleTransactionProduct => saleTransactionProduct.Quantity)
                })
                .OrderByDescending(quantityOfProductSoldModel => quantityOfProductSoldModel.Quantity)
                .ToList();

            return quantityOfClientProductSoldModels;
        }

        private async Task ValidateTheExistenceOfTheClient(
            GetQuantityOfClientProductsSoldQuery query,
            CancellationToken cancellationToken)
        {
            if (!await _pointOfSaleContext
                .Clients
                .AnyAsync(client => client.Id == query.ClientId, cancellationToken))
            {
                throw new NotFoundException($"The Client with Id {query.ClientId} not found in the database");
            }
        }
    }
}