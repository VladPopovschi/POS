using FluentValidation;

namespace PointOfSale.Application.Clients.Queries.GetClientById
{
    public class GetClientByIdQueryValidator : AbstractValidator<GetClientByIdQuery>
    {
        public GetClientByIdQueryValidator()
        {
            RuleFor(query => query.Id).GreaterThan(0);
        }
    }
}