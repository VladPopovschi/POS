using FluentValidation;

namespace PointOfSale.Application.Stores.Queries.GetStoreById
{
    public class GetStoreByIdQueryValidator : AbstractValidator<GetStoreByIdQuery>
    {
        public GetStoreByIdQueryValidator()
        {
            RuleFor(query => query.Id).GreaterThan(0);
        }
    }
}