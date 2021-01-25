using System.Collections.Generic;
using System.Linq;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PointOfSale.Application.Interfaces.DbContexts;
using PointOfSale.Application.Models;

namespace PointOfSale.Application.Clients.Queries.GetAllClients
{
    public class GetAllClientsQueryHandler : RequestHandler<GetAllClientsQuery, List<ClientModel>>
    {
        private readonly IPointOfSaleContext _pointOfSaleContext;

        public GetAllClientsQueryHandler(IPointOfSaleContext pointOfSaleContext)
        {
            _pointOfSaleContext = pointOfSaleContext;
        }

        protected override List<ClientModel> Handle(GetAllClientsQuery request)
        {
            return _pointOfSaleContext.Clients
                .AsNoTracking()
                .Select(client => new ClientModel
                {
                    Id = client.Id,
                    Name = client.Name,
                    TimestampCreated = client.TimestampCreated
                })
                .ToList();
        }
    }
}