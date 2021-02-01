using FluentValidation;

namespace PointOfSale.Application.Products.Commands.CreateProduct
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(command => command.Name).NotEmpty();
            RuleFor(command => command.GTIN).NotEmpty();
            RuleFor(command => command.Price).GreaterThan(0);
            RuleFor(command => command.ClientId).GreaterThan(0);
        }
    }
}