using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PointOfSale.Application.Exceptions;
using PointOfSale.Application.Interfaces.DbContexts;
using PointOfSale.Domain.Entities;

namespace PointOfSale.Application.Products.Commands.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
    {
        private readonly IPointOfSaleContext _pointOfSaleContext;

        public CreateProductCommandHandler(IPointOfSaleContext pointOfSaleContext)
        {
            _pointOfSaleContext = pointOfSaleContext;
        }

        public async Task<int> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            await ValidateTheUniquenessOfTheProductGTIN(command, cancellationToken);

            await ValidateTheExistenceOfTheClient(command, cancellationToken);

            var product = new Product
            {
                Name = command.Name,
                Description = command.Description,
                GTIN = command.GTIN,
                Price = command.Price,
                ImageURL = command.ImageURL,
                TimestampCreated = DateTimeOffset.UtcNow,
                ClientId = command.ClientId,
            };

            _pointOfSaleContext.Products.Add(product);
            await _pointOfSaleContext.SaveChangesAsync(cancellationToken);

            return product.Id;
        }

        private async Task ValidateTheUniquenessOfTheProductGTIN(CreateProductCommand command, CancellationToken cancellationToken)
        {
            if (await _pointOfSaleContext
                .Products
                .AnyAsync(product => product.GTIN.ToUpper() == command.GTIN.ToUpper(), cancellationToken))
            {
                throw new ValidationException($"A product with GTIN {command.GTIN} already exists in the database.");
            }
        }

        private async Task ValidateTheExistenceOfTheClient(CreateProductCommand command, CancellationToken cancellationToken)
        {
            if (!await _pointOfSaleContext
                .Clients
                .AnyAsync(client => client.Id == command.ClientId, cancellationToken))
            {
                throw new NotFoundException($"The Client with Id {command.ClientId} not found in the database");
            }
        }
    }
}