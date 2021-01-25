using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PointOfSale.Application.Exceptions;
using PointOfSale.Application.Interfaces.DbContexts;
using PointOfSale.Domain.Entities;

namespace PointOfSale.Application.Clients.Commands.UpdateClient
{
    public class UpdateClientCommandHandler : AsyncRequestHandler<UpdateClientCommand>
    {
        private readonly IPointOfSaleContext _pointOfSaleContext;

        public UpdateClientCommandHandler(IPointOfSaleContext pointOfSaleContext)
        {
            _pointOfSaleContext = pointOfSaleContext;
        }

        protected override async Task Handle(UpdateClientCommand command, CancellationToken cancellationToken)
        {
            var client = await _pointOfSaleContext.Clients
                .SingleOrDefaultAsync(client => client.Id == command.Id, cancellationToken);

            ValidateTheExistenceOfTheClient(command, client);

            await ValidateTheUniquenessOfTheClientName(command, cancellationToken);

            await UpdateTheClient(command, client, cancellationToken);
        }

        private static void ValidateTheExistenceOfTheClient(UpdateClientCommand command, Client clientFromDatabase)
        {
            if (clientFromDatabase == null)
            {
                throw new NotFoundException($"The Client with Id {command.Id} not found in the database");
            }
        }

        private async Task ValidateTheUniquenessOfTheClientName(UpdateClientCommand command, CancellationToken cancellationToken)
        {
            if (await _pointOfSaleContext
                .Clients
                .AnyAsync(client => client.Name.ToUpper() == command.Name.ToUpper(), cancellationToken))
            {
                throw new ValidationException($"The client with the name {command.Name} already exists in the database.");
            }
        }

        private async Task UpdateTheClient(UpdateClientCommand command, Client client, CancellationToken cancellationToken)
        {
            client.Name = command.Name;
            await _pointOfSaleContext.SaveChangesAsync(cancellationToken);
        }
    }
}