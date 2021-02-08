using System.Collections.Generic;
using MediatR;

namespace PointOfSale.Application.SaleTransactions.Commands.CreateSaleTransaction
{
    public class CreateSaleTransactionCommand : IRequest
    {
        public int StoreId { get; set; }

        public List<SaleTransactionProductModel> SaleTransactionProducts { get; set; }
    }

    public class SaleTransactionProductModel
    {
        public decimal Quantity { get; set; }

        public int ProductId { get; set; }
    }
}