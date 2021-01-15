using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PointOfSale.Domain.Entities
{
    public class Store
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string GLN { get; set; }

        public DateTimeOffset TimestampCreated { get; set; }

        public int ClientId { get; set; }

        public Client Client { get; set; }

        public List<SaleTransaction> SaleTransactions { get; set; }
    }
}