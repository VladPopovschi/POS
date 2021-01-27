using MediatR;

namespace PointOfSale.Application.Stores.Commands.UpdateStore
{
    public class UpdateStoreCommand : IRequest
    {
        public int Id { get; set; }

        public string GLN { get; set; }
    }
}