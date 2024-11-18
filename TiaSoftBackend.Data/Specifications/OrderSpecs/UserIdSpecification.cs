using System.Linq.Expressions;
using TiaSoftBackend.Data.Entities;

namespace TiaSoftBackend.Data.Specifications.OrderSpecs;

public class UserIdSpecification: Specification<Order>
{
    private readonly string _userId;
    
    public UserIdSpecification(string userId)
    {
        _userId = userId;
    }
    
    public override Expression<Func<Order, bool>> ToExpression()
    {
        return order => order.UserId == _userId;
    }
}