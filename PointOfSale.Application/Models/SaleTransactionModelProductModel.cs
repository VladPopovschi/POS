namespace PointOfSale.Application.Models
{
    public class SaleTransactionModelProductModel
    {
        public int Id { get; set; }

        public decimal Quantity { get; set; }

        public decimal ProductPrice { get; set; }

        public int SaleTransactionId { get; set; }

        public int ProductId { get; set; }
    }
}