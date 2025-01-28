using AutoMapper;
using ROP;
using TiaSoftBackend.Data.Constants;
using TiaSoftBackend.Data.Entities;
using TiaSoftBackend.Data.Repositories;
using TiaSoftBackend.DTOs.Orders;

namespace TiaSoftBackend.UseCases.Orders;

public class CreateOrder (IOrdersRepository ordersRepository, IMenuRepository menuRepository, IMapper mapper)
{
    public async Task<Result<List<OrderDto>>> Execute(string userId, CreateOrderRequest request)
    {
        var defaultOrderStatus = await ordersRepository.GetOrderStatusByName(OrderStatusConstants.Activo.ToString());
        
        if (defaultOrderStatus == null)
        {
            return Result.NotFound<List<OrderDto>>(ErrorCodes.ErrorCodes.OrderStatusNotFound);
        }
        
        var ordersToPrint = new List<Order>();
        
        foreach (var bill in request.Bills)
        {
            var newOrderId = Guid.NewGuid().ToString();
            
            /*
             * Create a list of OrderProduct objects to store the menu products in the order
             */
            var products = new List<OrderProduct>();
            var billTotal = 0.0m;
            
            foreach (var item in bill.Items)
            {
                var product = await menuRepository.GetProductById(item.ProductId);
                
                // Calculate the total price of the bill by adding the price of each product multiplied by the quantity
                billTotal += product.Price * item.Quantity;
    
                // TODO: Add orderStatusId 
                products.Add(new OrderProduct
                {
                    OrderId = newOrderId,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    PriceAtOrder = product.Price,
                });
            }
            
            // Calculate the orderId, current date without time, only dd/MM/yyyy
            var orderCounter = await ordersRepository.GetOrdersCount(DateTime.Today);
            
            var newOrder = new Order
            {
                OrderId = newOrderId,
                UserId = userId,
                TableId = bill.TableId,
                AreaId = request.AreaId,
                AdditionalInfo = request.AdditionalInfo ?? String.Empty,
                OrderStatusId = defaultOrderStatus.OrderStatusId,
                BillId = bill.BillId,
                TotalPrice = billTotal,
                OrderNumber = orderCounter
            };
            
            newOrder.Products = products;
            ordersToPrint.Add(newOrder);
            
            await ordersRepository.CreateOrder(newOrder);
        }
        
        return mapper.Map<List<OrderDto>>(ordersToPrint);
    }
}