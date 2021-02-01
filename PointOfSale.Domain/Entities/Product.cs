using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PointOfSale.Domain.Entities
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public string GTIN { get; set; }

        public decimal Price { get; set; }

        public string ImageURL { get; set; }

        public DateTimeOffset TimestampCreated { get; set; }

        public int ClientId { get; set; }

        public Client Client { get; set; }

        public List<SaleTransactionProduct> SaleTransactionProducts { get; set; }
    }
}