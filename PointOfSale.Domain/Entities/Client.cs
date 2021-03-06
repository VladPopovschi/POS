using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PointOfSale.Domain.Entities
{
    public class Client
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public DateTimeOffset TimestampCreated { get; set; }

        public List<Store> Stores { get; set; }

        public List<Product> Products { get; set; }
    }
}