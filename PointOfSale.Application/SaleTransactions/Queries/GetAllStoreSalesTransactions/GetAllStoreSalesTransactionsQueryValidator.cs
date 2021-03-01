using FluentValidation;

namespace PointOfSale.Application.SaleTransactions.Queries.GetAllStoreSalesTransactions
{
    public class GetAllStoreSalesTransactionsQueryValidator : AbstractValidator<GetAllStoreSalesTransactionsQuery>
    {
        public GetAllStoreSalesTransactionsQueryValidator()
        {
            RuleFor(query => query.StoreId).GreaterThan(0);
        }
    }
}