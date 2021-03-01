using FluentValidation;

namespace PointOfSale.Application.Products.Queries.GetQuantityOfClientProductsSold
{
    public class GetQuantityOfClientProductsSoldQueryValidator : AbstractValidator<GetQuantityOfClientProductsSoldQuery>
    {
        public GetQuantityOfClientProductsSoldQueryValidator()
        {
            RuleFor(query => query.ClientId).GreaterThan(0);
        }
    }
}