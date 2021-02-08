using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PointOfSale.Application.Exceptions;
using PointOfSale.Application.Interfaces.DbContexts;

namespace PointOfSale.Application.SaleTransactions.Commands.CreateSaleTransaction
{
    public class CreateSaleTransactionCommandHandler : AsyncRequestHandler<CreateSaleTransactionCommand>
    {
        private readonly IPointOfSaleContext _pointOfSaleContext;

        public CreateSaleTransactionCommandHandler(IPointOfSaleContext pointOfSaleContext)
        {
            _pointOfSaleContext = pointOfSaleContext;
        }

        protected override async Task Handle(CreateSaleTransactionCommand command, CancellationToken cancellationToken)
        {
            // Валидация существования магазина
            // Валидация существования каждого продукта
            // Валидация принадлежности каждого продукта клиента магазина

            await ValidateTheExistenceOfTheStore(command, cancellationToken);

            await ValidateTheExistenceOfTransactionProducts(command, cancellationToken);
            await ValidateIfTheProductsBelongToTheStoreClient(command, cancellationToken);
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
    }
}