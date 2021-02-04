using FluentValidation;

namespace PointOfSale.Application.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(command => command.Id).GreaterThan(0);
            RuleFor(command => command.Name).NotEmpty();
            RuleFor(command => command.GTIN).NotEmpty();
            RuleFor(command => command.Price).GreaterThan(0);
        }
    }
}