using System.Collections.Generic;
using MediatR;
using PointOfSale.Application.Models;

namespace PointOfSale.Application.Stores.Queries.GetAllClientStores
{
    public class GetAllClientStoresQuery : IRequest<List<StoreModel>>
    {
        public int ClientId { get; set; }
    }
}