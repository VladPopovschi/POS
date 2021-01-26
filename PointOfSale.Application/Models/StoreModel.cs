using System;

namespace PointOfSale.Application.Models
{
    public class StoreModel
    {
        public int Id { get; set; }

        public string GLN { get; set; }

        public int ClientId { get; set; }

        public DateTimeOffset TimestampCreated { get; set; }
    }
}