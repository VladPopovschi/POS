using System;
using System.Collections.Generic;

namespace PointOfSale.Application.Models
{
    public class SaleTransactionModel
    {
        public int Id { get; set; }

        public DateTimeOffset TimestampCreated { get; set; }

        public decimal Price { get; set; }

        public int StoreId { get; set; }

        public List<SaleTransactionModelProductModel> SaleTransactionProducts { get; set; }
    }
}