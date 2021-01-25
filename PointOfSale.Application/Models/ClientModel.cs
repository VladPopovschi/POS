using System;

namespace PointOfSale.Application.Models
{
    public class ClientModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTimeOffset TimestampCreated { get; set; }
    }
}