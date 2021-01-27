using FluentValidation;

namespace PointOfSale.Application.Stores.Queries.GetAllClientStores
{
    public class GetAllClientStoresQueryValidator : AbstractValidator<GetAllClientStoresQuery>
    {
        public GetAllClientStoresQueryValidator()
        {
            RuleFor(query => query.ClientId).GreaterThan(0);
        }
    }
}