using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PointOfSale.Application.Models;

namespace PointOfSale.Application.Clients.Queries.GetClientById
{
    public class GetClientByIdQueryHandler : IRequestHandler<GetClientByIdQuery, Client>
    {
        public async Task<Client> Handle(GetClientByIdQuery request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}