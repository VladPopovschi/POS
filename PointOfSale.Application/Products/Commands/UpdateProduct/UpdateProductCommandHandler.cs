using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PointOfSale.Application.Exceptions;
using PointOfSale.Application.Interfaces.DbContexts;
using PointOfSale.Domain.Entities;

namespace PointOfSale.Application.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandHandler : AsyncRequestHandler<UpdateProductCommand>
    {
        private readonly IPointOfSaleContext _pointOfSaleContext;

        public UpdateProductCommandHandler(IPointOfSaleContext pointOfSaleContext)
        {
            _pointOfSaleContext = pointOfSaleContext;
        }

        protected override async Task Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            var product = await _pointOfSaleContext.Products
                .SingleOrDefaultAsync(product => product.Id == command.Id, cancellationToken);

            ValidateTheExistenceOfTheProduct(command, product);

            await ValidateTheUniquenessOfTheProductGTIN(command, cancellationToken);

            await UpdateTheProduct(command, product, cancellationToken);
        }

        private static void ValidateTheExistenceOfTheProduct(UpdateProductCommand command, Product productFromDatabase)
        {
            if (productFromDatabase == null)
            {
                throw new NotFoundException($"The Product with Id {command.Id} not found in the database");
            }
        }

        private async Task ValidateTheUniquenessOfTheProductGTIN(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            if (await _pointOfSaleContext
                .Products
                .AnyAsync(product => product.GTIN.ToUpper() == command.GTIN.ToUpper(), cancellationToken))
            {
                throw new ValidationException($"A product with GTIN {command.GTIN} already exists in the database.");
            }
        }

        private async Task UpdateTheProduct(UpdateProductCommand command, Product product, CancellationToken cancellationToken)
        {
            product.Name = command.Name;
            product.Description = command.Description;
            product.GTIN = command.GTIN;
            product.Price = command.Price;
            product.ImageURL = command.ImageURL;

            await _pointOfSaleContext.SaveChangesAsync(cancellationToken);
        }
    }
}