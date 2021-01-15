using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PointOfSale.Domain.Entities
{
    public class SaleTransaction
    {
        [Key]
        public int Id { get; set; }

        public DateTimeOffset TimestampCreated { get; set; }

        public decimal Price { get; set; }

        [Required]
        public string PriceCurrency { get; set; }

        public int StoreId { get; set; }

        public Store Store { get; set; }

        public List<SaleTransactionProduct> SaleTransactionProducts { get; set; }
    }
}