using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PointOfSale.Application.Exceptions;
using PointOfSale.Application.Interfaces.DbContexts;
using PointOfSale.Application.Models;
using PointOfSale.Domain.Entities;

namespace PointOfSale.Application.Stores.Queries.GetAllClientStores
{
    public class GetAllClientStoresQueryHandler : IRequestHandler<GetAllClientStoresQuery, List<StoreModel>>
    {
        private readonly IPointOfSaleContext _pointOfSaleContext;

        public GetAllClientStoresQueryHandler(IPointOfSaleContext pointOfSaleContext)
        {
            _pointOfSaleContext = pointOfSaleContext;
        }

        public async Task<List<StoreModel>> Handle(GetAllClientStoresQuery query, CancellationToken cancellationToken)
        {
            var client = await _pointOfSaleContext
                .Clients
                .AsNoTracking()
                .Include(client => client.Stores)
                .SingleOrDefaultAsync(client => client.Id == query.ClientId, cancellationToken);

            ValidateTheExistenceOfTheClient(query, client);

            var clientStores = new List<StoreModel>();

            client.Stores.ForEach(store => clientStores.Add(new StoreModel
            {
                Id = store.Id,
                GLN = store.GLN,
                ClientId = store.ClientId,
                TimestampCreated = store.TimestampCreated
            }));

            return clientStores;
        }

        private static void ValidateTheExistenceOfTheClient(GetAllClientStoresQuery query, Client clientFromDatabase)
        {
            if (clientFromDatabase == null)
            {
                throw new NotFoundException($"The Client with Id {query.ClientId} not found in the database");
            }
        }
    }
}