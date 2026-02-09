using Catalog.Domain.Common;

namespace Catalog.Domain.Events;

public class ProductCreatedEvent : DomainEvent
{
    public Guid ProductId { get; }
    public string Name { get; }
    public decimal Price { get; }

    public ProductCreatedEvent(Guid productId, string name, decimal price)
    {
        ProductId = productId;
        Name = name;
        Price = price;
    }
}

public class ProductPriceChangedEvent : DomainEvent
{
    public Guid ProductId { get; }
    public decimal OldPrice { get; }
    public decimal NewPrice { get; }

    public ProductPriceChangedEvent(Guid productId, decimal oldPrice, decimal newPrice)
    {
        ProductId = productId;
        OldPrice = oldPrice;
        NewPrice = newPrice;
    }
}

public class StockUpdatedEvent : DomainEvent
{
    public Guid ProductId { get; }
    public int NewQuantity { get; }

    public StockUpdatedEvent(Guid productId, int newQuantity)
    {
        ProductId = productId;
        NewQuantity = newQuantity;
    }
}

public class StockDecreasedEvent : DomainEvent
{
    public Guid ProductId { get; }
    public int Quantity { get; }
    public int RemainingStock { get; }

    public StockDecreasedEvent(Guid productId, int quantity, int remainingStock)
    {
        ProductId = productId;
        Quantity = quantity;
        RemainingStock = remainingStock;
    }
}

public class StockIncreasedEvent : DomainEvent
{
    public Guid ProductId { get; }
    public int Quantity { get; }
    public int NewStock { get; }

    public StockIncreasedEvent(Guid productId, int quantity, int newStock)
    {
        ProductId = productId;
        Quantity = quantity;
        NewStock = newStock;
    }
}
