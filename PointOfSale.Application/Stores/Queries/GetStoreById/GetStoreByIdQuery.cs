using MediatR;
using PointOfSale.Application.Models;

namespace PointOfSale.Application.Stores.Queries.GetStoreById
{
    public class GetStoreByIdQuery : IRequest<StoreModel>
    {
        public int Id { get; set; }
    }
}