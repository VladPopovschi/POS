using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PointOfSale.Application.Exceptions;
using PointOfSale.Application.Interfaces.DbContexts;
using PointOfSale.Application.Models;

namespace PointOfSale.Application.Clients.Queries.GetClientById
{
    public class GetClientByIdQueryHandler : IRequestHandler<GetClientByIdQuery, Client>
    {
        private readonly IPointOfSaleContext _pointOfSaleContext;

        public GetClientByIdQueryHandler(IPointOfSaleContext pointOfSaleContext)
        {
            _pointOfSaleContext = pointOfSaleContext;
        }

        public async Task<Client> Handle(GetClientByIdQuery request, CancellationToken cancellationToken)
        {
            var client = await _pointOfSaleContext.Clients
                .SingleOrDefaultAsync(client => client.Id == request.Id, cancellationToken);

            if (client == null)
            {
                throw new NotFoundException($"The Client with Id {request.Id} not found in the database");
            }

            return new Client
            {
                Id = client.Id,
                Name = client.Name,
                TimestampCreated = client.TimestampCreated
            };
        }
    }
}