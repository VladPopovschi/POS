using FluentValidation;

namespace PointOfSale.Application.Stores.Commands.UpdateStore
{
    public class UpdateStoreCommandValidator : AbstractValidator<UpdateStoreCommand>
    {
        public UpdateStoreCommandValidator()
        {
            RuleFor(command => command.Id).GreaterThan(0);
            RuleFor(command => command.GLN).NotEmpty();
        }
    }
}