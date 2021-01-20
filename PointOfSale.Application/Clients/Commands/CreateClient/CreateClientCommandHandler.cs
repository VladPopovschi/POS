using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace PointOfSale.Application.Clients.Commands.CreateClient
{
    public class CreateClientCommandHandler : IRequestHandler<CreateClientCommand, int>
    {
        public async Task<int> Handle(CreateClientCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}