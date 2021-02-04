using MediatR;

namespace PointOfSale.Application.Products.Commands.UpdateProduct
{
    public class UpdateProductCommand : IRequest
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string GTIN { get; set; }

        public decimal Price { get; set; }

        public string ImageURL { get; set; }
    }
}