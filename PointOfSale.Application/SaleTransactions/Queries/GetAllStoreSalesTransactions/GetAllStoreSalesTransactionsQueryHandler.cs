using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PointOfSale.Application.Exceptions;
using PointOfSale.Application.Interfaces.DbContexts;
using PointOfSale.Application.Models;
using PointOfSale.Domain.Entities;

namespace PointOfSale.Application.SaleTransactions.Queries.GetAllStoreSalesTransactions
{
    public class GetAllStoreSalesTransactionsQueryHandler
        : IRequestHandler<GetAllStoreSalesTransactionsQuery, List<SaleTransactionModel>>
    {
        private readonly IPointOfSaleContext _pointOfSaleContext;

        public GetAllStoreSalesTransactionsQueryHandler(IPointOfSaleContext pointOfSaleContext)
        {
            _pointOfSaleContext = pointOfSaleContext;
        }

        public async Task<List<SaleTransactionModel>> Handle(
            GetAllStoreSalesTransactionsQuery query,
            CancellationToken cancellationToken)
        {
            var store = await _pointOfSaleContext
                .Stores
                .AsNoTracking()
                .Include(store => store.SaleTransactions)
                .ThenInclude(saleTransaction => saleTransaction.SaleTransactionProducts)
                .SingleOrDefaultAsync(store => store.Id == query.StoreId, cancellationToken);

            ValidateTheExistenceOfTheStore(query, store);

            var storeSaleTransactions = new List<SaleTransactionModel>();

            store.SaleTransactions
                .OrderByDescending(saleTransaction => saleTransaction.TimestampCreated)
                .ToList()
                .ForEach(saleTransaction => storeSaleTransactions.Add(new SaleTransactionModel
                {
                    Id = saleTransaction.Id,
                    TimestampCreated = saleTransaction.TimestampCreated,
                    Price = saleTransaction.Price,
                    StoreId = saleTransaction.StoreId,
                    SaleTransactionProducts = saleTransaction.SaleTransactionProducts
                    .Select(saleTransactionProduct => new SaleTransactionProductModel
                    {
                        Id = saleTransactionProduct.Id,
                        Quantity = saleTransactionProduct.Quantity,
                        ProductPrice = saleTransactionProduct.ProductPrice,
                        SaleTransactionId = saleTransactionProduct.SaleTransactionId,
                        ProductId = saleTransactionProduct.ProductId
                    })
                    .ToList()
                }));

            return storeSaleTransactions;
        }

        private static void ValidateTheExistenceOfTheStore(GetAllStoreSalesTransactionsQuery query, Store storeFromDatabase)
        {
            if (storeFromDatabase == null)
            {
                throw new NotFoundException($"The Store with Id {query.StoreId} not found in the database");
            }
        }
    }
}