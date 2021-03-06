using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MassTransit.EventHubIntegration;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PointOfSale.Application.Exceptions;
using PointOfSale.Application.Interfaces.DbContexts;
using PointOfSale.Application.Models;
using PointOfSale.Domain.Entities;

namespace PointOfSale.Application.SaleTransactions.Commands.CreateSaleTransaction
{
    public class CreateSaleTransactionCommandHandler : AsyncRequestHandler<CreateSaleTransactionCommand>
    {
        private readonly IPointOfSaleContext _pointOfSaleContext;
        private readonly IEventHubProducerProvider _eventHubProducerProvider;

        public CreateSaleTransactionCommandHandler(
            IPointOfSaleContext pointOfSaleContext,
            IEventHubProducerProvider eventHubProducerProvider)
        {
            _pointOfSaleContext = pointOfSaleContext;
            _eventHubProducerProvider = eventHubProducerProvider;
        }

        protected override async Task Handle(CreateSaleTransactionCommand command, CancellationToken cancellationToken)
        {
            await ValidateTheExistenceOfTheStore(command, cancellationToken);

            await ValidateTheExistenceOfTransactionProducts(command, cancellationToken);
            await ValidateIfTheProductsBelongToTheStoreClient(command, cancellationToken);

            var transaction = new SaleTransaction
            {
                TimestampCreated = DateTimeOffset.UtcNow,
                Price = await GetThePriceOfTheTransaction(command, cancellationToken),
                StoreId = command.StoreId,
                SaleTransactionProducts = command.SaleTransactionProducts
                    .Select(saleTransactionProduct =>
                    {
                        var product = _pointOfSaleContext
                            .Products
                            .AsNoTracking()
                            .Single(product => product.Id == saleTransactionProduct.ProductId);

                        return new SaleTransactionProduct
                        {
                            Quantity = saleTransactionProduct.Quantity,
                            ProductPrice = product.Price,
                            ProductId = saleTransactionProduct.ProductId
                        };
                    })
                    .ToList()
            };

            await _pointOfSaleContext.SaleTransactions.AddAsync(transaction, cancellationToken);
            await _pointOfSaleContext.SaveChangesAsync(cancellationToken);

            await ProduceMessageToEventHub(transaction, cancellationToken);
        }

        private async Task ValidateTheExistenceOfTheStore(CreateSaleTransactionCommand command, CancellationToken cancellationToken)
        {
            if (!await _pointOfSaleContext
                .Stores
                .AnyAsync(store => store.Id == command.StoreId, cancellationToken))
            {
                throw new NotFoundException($"The Store with Id {command.StoreId} not found in the database");
            }
        }

        private async Task ValidateTheExistenceOfTransactionProducts(
            CreateSaleTransactionCommand command,
            CancellationToken cancellationToken)
        {
            var productIds = command.SaleTransactionProducts.Select(product => product.ProductId);

            foreach (var productId in productIds)
            {
                if (!await _pointOfSaleContext
                    .Products
                    .AnyAsync(product => product.Id == productId, cancellationToken))
                {
                    throw new ValidationException($"The Product with Id {productId} not found in the database");
                }
            }
        }

        private async Task ValidateIfTheProductsBelongToTheStoreClient(
            CreateSaleTransactionCommand command,
            CancellationToken cancellationToken)
        {
            var clientId = (await _pointOfSaleContext
                .Stores
                .AsNoTracking()
                .SingleAsync(store => store.Id == command.StoreId, cancellationToken))
                .ClientId;

            var productIds = command.SaleTransactionProducts.Select(product => product.ProductId);

            foreach (var productId in productIds)
            {
                var productClientId = (await _pointOfSaleContext
                    .Products
                    .AsNoTracking()
                    .SingleAsync(product => product.Id == productId, cancellationToken)).ClientId;

                if (productClientId != clientId)
                {
                    throw new ValidationException($"The client of the product that has identifier {productId} " +
                                                  "is different from the client of the store " +
                                                  "where the sale transaction took place.");
                }
            }
        }

        private async Task<decimal> GetThePriceOfTheTransaction(CreateSaleTransactionCommand command, CancellationToken cancellationToken)
        {
            decimal transactionPrice = 0;

            foreach (var saleTransactionProduct in command.SaleTransactionProducts)
            {
                var product = await _pointOfSaleContext
                    .Products
                    .AsNoTracking()
                    .SingleAsync(product => product.Id == saleTransactionProduct.ProductId, cancellationToken);

                transactionPrice += product.Price * saleTransactionProduct.Quantity;
            }

            return transactionPrice;
        }

        private async Task ProduceMessageToEventHub(SaleTransaction transaction, CancellationToken cancellationToken)
        {
            var producer = await _eventHubProducerProvider.GetProducer("pointofsale");

            await producer.Produce(
                new SaleTransactionHappenedMessage
                {
                    Id = transaction.Id,
                    TimestampCreated = transaction.TimestampCreated,
                    Price = transaction.Price,
                    StoreId = transaction.StoreId,
                    SaleTransactionProducts = transaction.SaleTransactionProducts.Select(
                            product => new SaleTransactionHappenedMessageProduct
                            {
                                Id = product.Id,
                                Quantity = product.Quantity,
                                ProductPrice = product.ProductPrice,
                                ProductId = product.ProductId
                            })
                        .ToList()
                },
                cancellationToken);
        }
    }
}