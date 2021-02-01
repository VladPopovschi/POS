using System.ComponentModel.DataAnnotations;

namespace PointOfSale.Domain.Entities
{
    public class SaleTransactionProduct
    {
        [Key]
        public int Id { get; set; }

        public decimal Quantity { get; set; }

        public decimal ProductPrice { get; set; }

        public int SaleTransactionId { get; set; }

        public int ProductId { get; set; }

        public SaleTransaction SaleTransaction { get; set; }

        public Product Product { get; set; }
    }
}