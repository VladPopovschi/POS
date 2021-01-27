using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PointOfSale.Application.Exceptions;
using PointOfSale.Application.Interfaces.DbContexts;
using PointOfSale.Domain.Entities;

namespace PointOfSale.Application.Stores.Commands.UpdateStore
{
    public class UpdateStoreCommandHandler : AsyncRequestHandler<UpdateStoreCommand>
    {
        private readonly IPointOfSaleContext _pointOfSaleContext;

        public UpdateStoreCommandHandler(IPointOfSaleContext pointOfSaleContext)
        {
            _pointOfSaleContext = pointOfSaleContext;
        }

        protected override async Task Handle(UpdateStoreCommand command, CancellationToken cancellationToken)
        {
            var store = await _pointOfSaleContext.Stores
                .SingleOrDefaultAsync(store => store.Id == command.Id, cancellationToken);

            ValidateTheExistenceOfTheStore(command, store);

            await ValidateTheUniquenessOfTheStoreGLN(command, cancellationToken);

            await UpdateTheStore(command, store, cancellationToken);
        }

        private static void ValidateTheExistenceOfTheStore(UpdateStoreCommand command, Store storeFromDatabase)
        {
            if (storeFromDatabase == null)
            {
                throw new NotFoundException($"The Store with Id {command.Id} not found in the database");
            }
        }

        private async Task ValidateTheUniquenessOfTheStoreGLN(UpdateStoreCommand command, CancellationToken cancellationToken)
        {
            if (await _pointOfSaleContext
                .Stores
                .AnyAsync(store => store.GLN.ToUpper() == command.GLN.ToUpper(), cancellationToken))
            {
                throw new ValidationException($"A store with GLN {command.GLN} already exists in the database.");
            }
        }

        private async Task UpdateTheStore(UpdateStoreCommand command, Store store, CancellationToken cancellationToken)
        {
            store.GLN = command.GLN;

            await _pointOfSaleContext.SaveChangesAsync(cancellationToken);
        }
    }
}