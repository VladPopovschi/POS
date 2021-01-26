using FluentValidation;

namespace PointOfSale.Application.Stores.Commands.CreateStore
{
    public class CreateStoreCommandValidator : AbstractValidator<CreateStoreCommand>
    {
        public CreateStoreCommandValidator()
        {
            RuleFor(command => command.GLN).NotEmpty();
            RuleFor(command => command.ClientId).GreaterThan(0);
        }
    }
}