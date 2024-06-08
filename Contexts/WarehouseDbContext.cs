using Microsoft.EntityFrameworkCore;
using warehouse.Models;

namespace warehouse.Contexts;

public class WarehouseDbContext(DbContextOptions<WarehouseDbContext> options) : DbContext(options)
{
    public DbSet<Shipping> Shippings { get; set; }
    public DbSet<Invoice> Invoices { get; set; }
    public DbSet<SalesDocument> SalesDocuments { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<PreparationScan> PreparationScans { get; set; }
    public DbSet<LoadingScan> LoadingScans { get; set; }
    public DbSet<ErrorViewModel> ErrorViewModel { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Shipping>()
            .HasKey(s => s.Id);

        modelBuilder.Entity<Invoice>()
            .HasKey(i => i.Id);

        modelBuilder.Entity<SalesDocument>()
            .HasKey(sd => sd.Id);

        modelBuilder.Entity<Product>()
            .HasKey(p => p.Id);

        modelBuilder.Entity<PreparationScan>()
            .HasKey(ps => ps.Id);

        modelBuilder.Entity<LoadingScan>()
            .HasKey(ls => ls.Id);

        modelBuilder.Entity<ErrorViewModel>()
            .HasNoKey();

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

        modelBuilder.Entity<PreparationScan>()
            .HasOne(ps => ps.LoadingScans)
            .WithOne(ls => ls.PreparationScan)
            .HasForeignKey<LoadingScan>(ls => ls.PreparationId);
    }

    public override int SaveChanges()
    {
        var entries = ChangeTracker
            .Entries()
            .Where(e => e.Entity is Shipping or Invoice or SalesDocument or Product or PreparationScan or LoadingScan);

        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added)
            {
                ((dynamic)entry.Entity).CreatedAt = DateTime.Now;
            }

            ((dynamic)entry.Entity).UpdatedAt = DateTime.Now;
        }

        return base.SaveChanges();
    }
}