using MediatR;

namespace PointOfSale.Application.Clients.Commands.UpdateClient
{
    public class UpdateClientCommand : IRequest
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}