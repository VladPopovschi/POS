using FluentValidation;

namespace PointOfSale.Application.Products.Queries.GetProductByGTIN
{
    public class GetProductByGTINQueryValidator : AbstractValidator<GetProductByGTINQuery>
    {
        public GetProductByGTINQueryValidator()
        {
            RuleFor(query => query.GTIN).NotEmpty();
        }
    }
}