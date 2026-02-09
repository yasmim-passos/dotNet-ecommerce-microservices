using Catalog.Domain.Common;

namespace Catalog.Domain.ValueObjects;

public class Money : ValueObject
{
    public decimal Amount { get; private set; }
    public string Currency { get; private set; }

    private Money() { }

    public Money(decimal amount, string currency = "BRL")
    {
        if (amount < 0) throw new ArgumentException("Amount cannot be negative");
        Amount = amount;
        Currency = currency;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Amount;
        yield return Currency;
    }
}

public class Stock : ValueObject
{
    public int Quantity { get; private set; }

    private Stock() { }

    public Stock(int quantity)
    {
        if (quantity < 0) throw new ArgumentException("Stock cannot be negative");
        Quantity = quantity;
    }

    public Stock Decrease(int amount) => new Stock(Quantity - amount);
    public Stock Increase(int amount) => new Stock(Quantity + amount);

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Quantity;
    }
}
