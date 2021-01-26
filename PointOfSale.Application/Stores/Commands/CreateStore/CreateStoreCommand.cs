using MediatR;

namespace PointOfSale.Application.Stores.Commands.CreateStore
{
    public class CreateStoreCommand : IRequest<int>
    {
        public string GLN { get; set; }

        public int ClientId { get; set; }
    }
}