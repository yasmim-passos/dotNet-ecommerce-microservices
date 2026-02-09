using Catalog.Domain.Common;
using Catalog.Domain.Events;
using Catalog.Domain.ValueObjects;

namespace Catalog.Domain.Entities;

public class Product : AggregateRoot
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public Money Price { get; private set; }
    public Stock Stock { get; private set; }
    public Guid CategoryId { get; private set; }
    public Category Category { get; private set; }
    public bool IsActive { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    private Product() { } // EF Core

    public Product(string name, string description, Money price, Stock stock, Guid categoryId)
    {
        Id = Guid.NewGuid();
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Description = description;
        Price = price ?? throw new ArgumentNullException(nameof(price));
        Stock = stock ?? throw new ArgumentNullException(nameof(stock));
        CategoryId = categoryId;
        IsActive = true;
        CreatedAt = DateTime.UtcNow;

        AddDomainEvent(new ProductCreatedEvent(Id, Name, Price.Amount));
    }

    public void UpdatePrice(Money newPrice)
    {
        if (newPrice.Amount <= 0)
            throw new InvalidOperationException("Price must be greater than zero");

        var oldPrice = Price;
        Price = newPrice;
        UpdatedAt = DateTime.UtcNow;

        AddDomainEvent(new ProductPriceChangedEvent(Id, oldPrice.Amount, newPrice.Amount));
    }

    public void UpdateStock(int quantity)
    {
        if (quantity < 0)
            throw new InvalidOperationException("Cannot update to negative stock");

        Stock = new Stock(quantity);
        UpdatedAt = DateTime.UtcNow;

        AddDomainEvent(new StockUpdatedEvent(Id, Stock.Quantity));
    }

    public void DecreaseStock(int quantity)
    {
        if (quantity <= 0)
            throw new InvalidOperationException("Quantity must be positive");

        if (Stock.Quantity < quantity)
            throw new InvalidOperationException("Insufficient stock");

        Stock = Stock.Decrease(quantity);
        UpdatedAt = DateTime.UtcNow;

        AddDomainEvent(new StockDecreasedEvent(Id, quantity, Stock.Quantity));
    }

    public void IncreaseStock(int quantity)
    {
        if (quantity <= 0)
            throw new InvalidOperationException("Quantity must be positive");

        Stock = Stock.Increase(quantity);
        UpdatedAt = DateTime.UtcNow;

        AddDomainEvent(new StockIncreasedEvent(Id, quantity, Stock.Quantity));
    }

    public void Deactivate()
    {
        IsActive = false;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Activate()
    {
        IsActive = true;
        UpdatedAt = DateTime.UtcNow;
    }
}
