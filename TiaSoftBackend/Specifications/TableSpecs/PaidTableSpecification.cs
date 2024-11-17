using System.Linq.Expressions;
using TiaSoftBackend.Constants;
using TiaSoftBackend.Entities;

namespace TiaSoftBackend.Specifications.TableSpecs;

public class PaidTableSpecification: Specification<TableEntity>
{
    public override Expression<Func<TableEntity, bool>> ToExpression()
    {
        return t => t.TableStatus.Name == TableStatusConstants.Pagado.ToString();
    }
}