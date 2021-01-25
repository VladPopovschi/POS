using System.Collections.Generic;
using MediatR;
using PointOfSale.Application.Models;

namespace PointOfSale.Application.Clients.Queries.GetAllClients
{
    public class GetAllClientsQuery : IRequest<List<ClientModel>>
    {
    }
}