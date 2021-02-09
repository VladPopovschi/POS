using FluentValidation;

namespace PointOfSale.Application.SaleTransactions.Commands.CreateSaleTransaction
{
    public class CreateSaleTransactionCommandValidator : AbstractValidator<CreateSaleTransactionCommand>
    {
        public CreateSaleTransactionCommandValidator()
        {
            RuleFor(command => command.StoreId).GreaterThan(0);

            RuleFor(command => command.SaleTransactionProducts)
                .NotEmpty();

            RuleFor(command => command.SaleTransactionProducts)
                .Cascade(CascadeMode.Stop)
                .ForEach(collection => collection
                    .Must(product => product != null)
                    .WithMessage($"Every {nameof(SaleTransactionProductModel)} object passed must not be null"))
                .ForEach(collection => collection
                    .Must(product => product.Quantity > 0)
                    .WithMessage("The quantity of each product sold must be greater than 0."))
                .ForEach(collection => collection
                    .Must(product => product.ProductId > 0)
                    .WithMessage("The identifier of each product sold must be greater than 0."));
        }
    }
}