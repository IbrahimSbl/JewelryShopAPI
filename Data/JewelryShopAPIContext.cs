using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using JewelryShopManagementSystem.Models;
using JewelryShopAPI.Models;

namespace JewelryShopAPI.Data
{
    public class JewelryShopAPIContext : DbContext
    {
        public JewelryShopAPIContext (DbContextOptions<JewelryShopAPIContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<JewelryItemPurchase>()
                .HasKey(jt => new { jt.PurchasesId, jt.JewelryItemsId });

            modelBuilder.Entity<JewelryItemPurchase>()
                .HasOne(jt => jt.Purchase)
                .WithMany(ea => ea.JoinTables)
                .HasForeignKey(jt => jt.PurchasesId);

            modelBuilder.Entity<JewelryItemPurchase>()
                .HasOne(jt => jt.JewelryItem)
                .WithMany(eb => eb.JoinTables)
                .HasForeignKey(jt => jt.JewelryItemsId);
        }

        public DbSet<JewelryShopManagementSystem.Models.Employee> Employee { get; set; } = default!;

        public DbSet<JewelryShopManagementSystem.Models.Customer> Customer { get; set; } = default!;

        public DbSet<JewelryShopManagementSystem.Models.Category> Category { get; set; } = default!;

        public DbSet<JewelryShopManagementSystem.Models.JewelryItem> JewelryItem { get; set; } = default!;

        public DbSet<JewelryShopManagementSystem.Models.Purchase> Purchase { get; set; } = default!;
    }
}
