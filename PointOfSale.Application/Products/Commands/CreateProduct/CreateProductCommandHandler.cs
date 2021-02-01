using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PointOfSale.Application.Exceptions;
using PointOfSale.Application.Interfaces.DbContexts;

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
    }
}