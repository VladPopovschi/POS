using System.Collections.Generic;
using MediatR;
using PointOfSale.Application.Interfaces.DbContexts;
using PointOfSale.Application.Models;

namespace PointOfSale.Application.Stores.Queries.GetAllClientStores
{
    public class GetAllClientStoresQueryHandler : RequestHandler<GetAllClientStoresQuery, List<StoreModel>>
    {
        private readonly IPointOfSaleContext _pointOfSaleContext;

        public GetAllClientStoresQueryHandler(IPointOfSaleContext pointOfSaleContext)
        {
            _pointOfSaleContext = pointOfSaleContext;
        }

        protected override List<StoreModel> Handle(GetAllClientStoresQuery request)
        {
        }
    }
}