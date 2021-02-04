using FluentValidation;

namespace PointOfSale.Application.Products.Queries.GetAllClientProducts
{
    public class GetAllClientProductsQueryValidator : AbstractValidator<GetAllClientProductsQuery>
    {
        public GetAllClientProductsQueryValidator()
        {
            RuleFor(query => query.ClientId).GreaterThan(0);
        }
    }
}