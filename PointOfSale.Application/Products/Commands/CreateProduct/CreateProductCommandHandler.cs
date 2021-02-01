using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PointOfSale.Application.Interfaces.DbContexts;

namespace PointOfSale.Application.Products.Commands.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
    {
        private readonly IPointOfSaleContext _pointOfSaleContext;

        public CreateProductCommandHandler(IPointOfSaleContext pointOfSaleContext)
        {
            _pointOfSaleContext = pointOfSaleContext;
        }

        public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}