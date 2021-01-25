using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PointOfSale.Application.Exceptions;
using PointOfSale.Application.Interfaces.DbContexts;
using PointOfSale.Application.Models;
using PointOfSale.Domain.Entities;

namespace PointOfSale.Application.Clients.Queries.GetClientById
{
    public class GetClientByIdQueryHandler : IRequestHandler<GetClientByIdQuery, ClientModel>
    {
        private readonly IPointOfSaleContext _pointOfSaleContext;

        public GetClientByIdQueryHandler(IPointOfSaleContext pointOfSaleContext)
        {
            _pointOfSaleContext = pointOfSaleContext;
        }

        public async Task<ClientModel> Handle(GetClientByIdQuery request, CancellationToken cancellationToken)
        {
            var client = await _pointOfSaleContext.Clients
                .AsNoTracking()
                .SingleOrDefaultAsync(client => client.Id == request.Id, cancellationToken);

            ValidateTheClient(request, client);

            return new ClientModel
            {
                Id = client.Id,
                Name = client.Name,
                TimestampCreated = client.TimestampCreated
            };
        }

        private static void ValidateTheClient(GetClientByIdQuery request, Client clientFromDatabase)
        {
            if (clientFromDatabase == null)
            {
                throw new NotFoundException($"The Client with Id {request.Id} not found in the database");
            }
        }
    }
}