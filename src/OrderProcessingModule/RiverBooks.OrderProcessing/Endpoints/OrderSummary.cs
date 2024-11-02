namespace RiverBooks.OrderProcessing.Endpoints;

public class OrderSummary
{
    public Guid UserId { get; set; }
    public Guid OrderId { get; set; }
    public DateTimeOffset DateCreated { get; set; }
    public DateTimeOffset DateShipped { get; set; }
    public decimal Total { get; set; }
}
