using System.Linq.Expressions;
using TiaSoftBackend.Constants;
using TiaSoftBackend.Entities;

namespace TiaSoftBackend.Specifications.OrderSpecs;

public sealed class ActiveOrderSpecification : Specification<Order>
{
    public override Expression<Func<Order, bool>> ToExpression()
    {
        return order => order.OrderStatus.Name == OrderStatusConstants.Activo.ToString();
    }
}