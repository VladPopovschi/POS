using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PointOfSale.Domain.Entities;

namespace PointOfSale.Application.Interfaces.DbContexts
{
    public interface IPointOfSaleContext
    {
        DbSet<Client> Clients { get; set; }

        DbSet<Store> Stores { get; set; }

        DbSet<Product> Products { get; set; }

        DbSet<SaleTransaction> SaleTransactions { get; set; }

        DbSet<SaleTransactionProduct> SaleTransactionProducts { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}