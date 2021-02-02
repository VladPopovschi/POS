using System;

namespace PointOfSale.Application.Models
{
    public class ProductModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string GTIN { get; set; }

        public decimal Price { get; set; }

        public string ImageURL { get; set; }

        public int ClientId { get; set; }

        public DateTimeOffset TimestampCreated { get; set; }
    }
}