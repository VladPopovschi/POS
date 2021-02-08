using FluentValidation;

namespace PointOfSale.Application.SaleTransactions.Commands.CreateSaleTransaction
{
    public class CreateSaleTransactionCommandValidator : AbstractValidator<CreateSaleTransactionCommand>
    {
        public CreateSaleTransactionCommandValidator()
        {
            RuleFor(command => command.StoreId).GreaterThan(0);

            RuleFor(command => command.SaleTransactionProducts)
                .NotEmpty()
                .ForEach(collection => collection.Must(product => product.Quantity > 0))
                .ForEach(collection => collection.Must(product => product.ProductId > 0));
        }
    }
}