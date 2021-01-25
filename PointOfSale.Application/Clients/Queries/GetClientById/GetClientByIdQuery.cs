using MediatR;
using PointOfSale.Application.Models;

namespace PointOfSale.Application.Clients.Queries.GetClientById
{
    public class GetClientByIdQuery : IRequest<Client>
    {
        public int Id { get; set; }
    }
}