using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PointOfSale.Application.Exceptions;
using PointOfSale.Application.Interfaces.DbContexts;

namespace PointOfSale.Application.Stores.Commands.CreateStore
{
    public class CreateStoreCommandHandler : IRequestHandler<CreateStoreCommand, int>
    {
        private readonly IPointOfSaleContext _pointOfSaleContext;

        public CreateStoreCommandHandler(IPointOfSaleContext pointOfSaleContext)
        {
            _pointOfSaleContext = pointOfSaleContext;
        }

        public async Task<int> Handle(CreateStoreCommand command, CancellationToken cancellationToken)
        {
            await ValidateTheUniquenessOfTheStoreGLN(command, cancellationToken);
        }

        private async Task ValidateTheUniquenessOfTheStoreGLN(CreateStoreCommand command, CancellationToken cancellationToken)
        {
            if (await _pointOfSaleContext
                .Stores
                .AnyAsync(store => store.GLN.ToUpper() == command.GLN.ToUpper(), cancellationToken))
            {
                throw new ValidationException($"A store with GLN {command.GLN} already exists in the database.");
            }
        }
    }
}