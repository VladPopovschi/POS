using MediatR;
using PointOfSale.Application.Models;

namespace PointOfSale.Application.Products.Queries.GetProductById
{
    public class GetProductByIdQuery : IRequest<ProductModel>
    {
        public int Id { get; set; }
    }
}