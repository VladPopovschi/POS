using MediatR;

namespace PointOfSale.Application.Clients.Commands.CreateClient
{
    public class CreateClientCommand : IRequest<int>
    {
        public string Name { get; set; }
    }
}