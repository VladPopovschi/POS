using Microsoft.EntityFrameworkCore;

namespace PointOfSale.Infrastructure.Persistence.DbContexts
{
    public class PointOfSaleDbContext : DbContext
    {
        public PointOfSaleDbContext(DbContextOptions<PointOfSaleDbContext> options) : base(options)
        {
        }
    }
}