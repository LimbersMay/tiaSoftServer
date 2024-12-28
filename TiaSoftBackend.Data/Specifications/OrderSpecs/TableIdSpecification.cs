using System.Linq.Expressions;
using TiaSoftBackend.Data.Entities;

namespace TiaSoftBackend.Data.Specifications.OrderSpecs;

public class TableIdSpecification (string tableId) : Specification<Order>
{
    public override Expression<Func<Order, bool>> ToExpression()
    {
        return order => order.TableId == tableId;
    }
}