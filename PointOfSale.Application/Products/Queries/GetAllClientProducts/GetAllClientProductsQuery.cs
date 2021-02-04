using System.Collections.Generic;
using MediatR;
using PointOfSale.Application.Models;

namespace PointOfSale.Application.Products.Queries.GetAllClientProducts
{
    public class GetAllClientProductsQuery : IRequest<List<ProductModel>>
    {
        public int ClientId { get; set; }
    }
}