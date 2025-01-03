namespace RiverBooks.OrderProcessing;

internal class Order
{
  public Guid Id { get; private set; } = Guid.NewGuid();
  public Guid UserId { get; private set; }
  public Address ShippingAddress { get; private set; } = default!;
  public Address BillingAddress { get; private set; } = default!;
  private readonly List<OrderItem> _orderItems = new();
  public IReadOnlyCollection<OrderItem> OrderItems => _orderItems.AsReadOnly();

  public DateTimeOffset DateCreated { get; private set; } = DateTimeOffset.Now;
  private void AddOrderItem(OrderItem item) => _orderItems.Add(item);

  internal static class Factory
  {
    public static Order Create(Guid userId,
        Address ShippingAddress,
        Address BillingAddress,
        IEnumerable<OrderItem> orderItems)
    {
      var order = new Order();
      order.UserId = userId;
      order.ShippingAddress = ShippingAddress;
      order.BillingAddress = BillingAddress;
      foreach (var item in orderItems)
      {
        order.AddOrderItem(item);
      }

      return order;
    }
  }
}


