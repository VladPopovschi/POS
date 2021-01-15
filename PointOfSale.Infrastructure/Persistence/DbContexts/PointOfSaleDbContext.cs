﻿using Microsoft.EntityFrameworkCore;
using PointOfSale.Application.Interfaces.DbContexts;
using PointOfSale.Domain.Entities;

namespace PointOfSale.Infrastructure.Persistence.DbContexts
{
    public class PointOfSaleContext : DbContext, IPointOfSaleContext
    {
        public PointOfSaleContext(DbContextOptions<PointOfSaleContext> options) : base(options)
        {
        }

        public DbSet<Client> Clients { get; set; }

        public DbSet<Store> Stores { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<SaleTransaction> SaleTransactions { get; set; }

        public DbSet<SaleTransactionProduct> SaleTransactionProducts { get; set; }
    }
}