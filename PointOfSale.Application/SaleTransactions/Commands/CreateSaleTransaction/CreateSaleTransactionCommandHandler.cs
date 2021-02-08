using System.Threading;
using System.Threading.Tasks;
using MediatR;
using PointOfSale.Application.Interfaces.DbContexts;

namespace PointOfSale.Application.SaleTransactions.Commands.CreateSaleTransaction
{
    public class CreateSaleTransactionCommandHandler : AsyncRequestHandler<CreateSaleTransactionCommand>
    {
        private readonly IPointOfSaleContext _pointOfSaleContext;

        public CreateSaleTransactionCommandHandler(IPointOfSaleContext pointOfSaleContext)
        {
            _pointOfSaleContext = pointOfSaleContext;
        }

        protected override async Task Handle(CreateSaleTransactionCommand request, CancellationToken cancellationToken)
        {
            // Валидация существования магазина
            // Валидация существования каждого продукта
            // Валидация принадлежности каждого продукта клиента магазина
            throw new System.NotImplementedException();
        }
    }
}