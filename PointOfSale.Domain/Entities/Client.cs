using System;
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
    }
}