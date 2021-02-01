using MediatR;

namespace PointOfSale.Application.Products.Commands.CreateProduct
{
    public class CreateProductCommand : IRequest<int>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string GTIN { get; set; }

        public decimal Price { get; set; }

        public string ImageURL { get; set; }

        public int ClientId { get; set; }
    }
}