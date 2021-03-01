using System.Collections.Generic;
using MediatR;
using PointOfSale.Application.Models;

namespace PointOfSale.Application.SaleTransactions.Queries.GetAllStoreSalesTransactions
{
    public class GetAllStoreSalesTransactionsQuery : IRequest<List<SaleTransactionModel>>
    {
        public int StoreId { get; set; }
    }
}