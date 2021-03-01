using System.Collections.Generic;
using MediatR;
using PointOfSale.Application.Models;

namespace PointOfSale.Application.Products.Queries.GetQuantityOfClientProductsSold
{
    public class GetQuantityOfClientProductsSoldQuery : IRequest<List<QuantityOfProductSoldModel>>
    {
        public int ClientId { get; set; }
    }
}