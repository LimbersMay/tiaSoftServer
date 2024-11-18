using System.Linq.Expressions;
using TiaSoftBackend.Data.Entities;
using TiaSoftBackend.Constants;

namespace TiaSoftBackend.Shared.Specifications.OrderSpecs;

public sealed class ActiveOrderSpecification : Specification<Order>
{
    public override Expression<Func<Order, bool>> ToExpression()
    {
        return order => order.OrderStatus.Name == OrderStatusConstants.Activo.ToString();
    }
}