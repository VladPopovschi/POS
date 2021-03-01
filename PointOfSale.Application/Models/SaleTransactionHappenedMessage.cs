using System;
using System.Collections.Generic;

namespace PointOfSale.Application.Models
{
    public class SaleTransactionHappenedMessage
    {
        public int Id { get; set; }

        public DateTimeOffset TimestampCreated { get; set; }

        public decimal Price { get; set; }

        public int StoreId { get; set; }

        public List<SaleTransactionHappenedMessageProduct> SaleTransactionProducts { get; set; }
    }

    public class SaleTransactionHappenedMessageProduct
    {
        public int Id { get; set; }

        public decimal Quantity { get; set; }

        public decimal ProductPrice { get; set; }

        public int ProductId { get; set; }
    }
}