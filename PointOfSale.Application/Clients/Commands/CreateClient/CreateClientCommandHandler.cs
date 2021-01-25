using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PointOfSale.Application.Exceptions;
using PointOfSale.Application.Interfaces.DbContexts;
using PointOfSale.Domain.Entities;

namespace PointOfSale.Application.Clients.Commands.CreateClient
{
    public class CreateClientCommandHandler : IRequestHandler<CreateClientCommand, int>
    {
        private readonly IPointOfSaleContext _pointOfSaleContext;

        public CreateClientCommandHandler(IPointOfSaleContext pointOfSaleContext)
        {
            _pointOfSaleContext = pointOfSaleContext;
        }

        public async Task<int> Handle(CreateClientCommand command, CancellationToken cancellationToken)
        {
            await ValidateTheClient(command, cancellationToken);

            var client = new Client
            {
                Name = command.Name,
                TimestampCreated = DateTimeOffset.UtcNow
            };

            await _pointOfSaleContext.Clients.AddAsync(client, cancellationToken);
            await _pointOfSaleContext.SaveChangesAsync(cancellationToken);

            return client.Id;
        }

        private async Task ValidateTheClient(CreateClientCommand command, CancellationToken cancellationToken)
        {
            if (await _pointOfSaleContext
                .Clients
                .AnyAsync(client => client.Name.ToUpper() == command.Name.ToUpper(), cancellationToken))
            {
                throw new ValidationException($"The client with the name {command.Name} already exists in the database.");
            }
        }
    }
}