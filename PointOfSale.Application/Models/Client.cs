using System;

namespace PointOfSale.Application.Models
{
    public class Client
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTimeOffset TimestampCreated { get; set; }
    }
}