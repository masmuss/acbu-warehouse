using Microsoft.EntityFrameworkCore;
using warehouse.Models;

namespace warehouse.Contexts;

public class WarehouseDbContext(DbContextOptions<WarehouseDbContext> options) : DbContext(options)
{
    public DbSet<Shipping> Shippings { get; set; }
    public DbSet<Invoice> Invoices { get; set; }
    public DbSet<SalesDocument> SalesDocuments { get; set; }
    public DbSet<Product> Products { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Shipping>()
            .HasMany(s => s.Invoices)
            .WithOne(i => i.Shipping)
            .HasForeignKey(i => i.ShippingId);

        modelBuilder.Entity<Shipping>()
            .HasMany(s => s.SalesDocuments)
            .WithOne(sd => sd.Shipping)
            .HasForeignKey(sd => sd.ShippingId);

        modelBuilder.Entity<Shipping>()
            .HasMany(s => s.Products)
            .WithOne(p => p.Shipping)
            .HasForeignKey(p => p.ShippingId);
    }
}