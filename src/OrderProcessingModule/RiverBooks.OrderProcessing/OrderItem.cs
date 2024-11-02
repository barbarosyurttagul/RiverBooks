using Ardalis.GuardClauses;

namespace RiverBooks.OrderProcessing;

internal class OrderItem
{
    public OrderItem(Guid bookId, decimal unitPrice, int quantity, string description)
    {
        BookId = Guard.Against.Default(bookId);
        UnitPrice = Guard.Against.Negative(unitPrice);
        Quantity = Guard.Against.Negative(quantity);
        Description = Guard.Against.NullOrEmpty(description);
    }
    public Guid Id { get; private set; } = Guid.NewGuid();
    public Guid BookId { get; private set; }
    public decimal UnitPrice { get; private set; }
    public int Quantity { get; private set; }
    public string Description { get; private set; }
}

