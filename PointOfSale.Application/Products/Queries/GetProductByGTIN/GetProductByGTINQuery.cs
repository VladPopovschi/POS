using MediatR;
using PointOfSale.Application.Models;

namespace PointOfSale.Application.Products.Queries.GetProductByGTIN
{
    public class GetProductByGTINQuery : IRequest<ProductModel>
    {
        public string GTIN { get; set; }
    }
}