using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PointOfSale.Application.Exceptions;
using PointOfSale.Application.Interfaces.DbContexts;
using PointOfSale.Application.Models;
using PointOfSale.Domain.Entities;

namespace PointOfSale.Application.Stores.Queries.GetStoreById
{
    public class GetStoreByIdQueryHandler : IRequestHandler<GetStoreByIdQuery, StoreModel>
    {
        private readonly IPointOfSaleContext _pointOfSaleContext;

        public GetStoreByIdQueryHandler(IPointOfSaleContext pointOfSaleContext)
        {
            _pointOfSaleContext = pointOfSaleContext;
        }

        public async Task<StoreModel> Handle(GetStoreByIdQuery query, CancellationToken cancellationToken)
        {
            var store = await _pointOfSaleContext.Stores
                .AsNoTracking()
                .SingleOrDefaultAsync(store => store.Id == query.Id, cancellationToken);

            ValidateTheStore(query, store);

            return new StoreModel
            {
                Id = store.Id,
                GLN = store.GLN,
                ClientId = store.ClientId,
                TimestampCreated = store.TimestampCreated
            };
        }

        private static void ValidateTheStore(GetStoreByIdQuery query, Store storeFromDatabase)
        {
            if (storeFromDatabase == null)
            {
                throw new NotFoundException($"The Store with Id {query.Id} not found in the database");
            }
        }
    }
}