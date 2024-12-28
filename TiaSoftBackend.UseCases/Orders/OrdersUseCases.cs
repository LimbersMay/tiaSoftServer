namespace TiaSoftBackend.UseCases.Orders;

public record class OrdersUseCases (
    GetOrders GetOrders, CreateOrder CreateOrder, CancelOrderItem CancelOrderItem);