using System.Linq.Expressions;
using TiaSoftBackend.Data.Entities;

namespace TiaSoftBackend.UseCases.Specifications.TableSpecs;

public class UserIdSpecification: Specification<TableEntity>
{
    private readonly string _userId;
    
    public UserIdSpecification(string userId)
    {
        _userId = userId;
    }
    
    public override Expression<Func<TableEntity, bool>> ToExpression()
    {
        return t => t.UserId == _userId;
    }
}