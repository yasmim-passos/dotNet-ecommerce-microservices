using Microsoft.EntityFrameworkCore;
using Catalog.Domain.Entities;
using Catalog.Domain.ValueObjects;

namespace Catalog.Infrastructure.Data;

public class CatalogDbContext : DbContext
{
    public CatalogDbContext(DbContextOptions<CatalogDbContext> options) : base(options) { }

    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Product Configuration
        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.CreatedAt).IsRequired();
            
            // Money Value Object
            entity.OwnsOne(e => e.Price, price =>
            {
                price.Property(p => p.Amount).HasColumnName("Price").HasColumnType("decimal(18,2)");
                price.Property(p => p.Currency).HasColumnName("Currency").HasMaxLength(3);
            });
            
            // Stock Value Object
            entity.OwnsOne(e => e.Stock, stock =>
            {
                stock.Property(s => s.Quantity).HasColumnName("StockQuantity");
            });
            
            entity.HasOne(e => e.Category)
                  .WithMany()
                  .HasForeignKey(e => e.CategoryId);
            
            entity.Ignore(e => e.DomainEvents);
        });

        // Category Configuration
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
        });

        // Seed Data
        var electronicsId = Guid.NewGuid();
        var clothingId = Guid.NewGuid();

        modelBuilder.Entity<Category>().HasData(
            new { Id = electronicsId, Name = "Electronics" },
            new { Id = clothingId, Name = "Clothing" }
        );
    }
}
