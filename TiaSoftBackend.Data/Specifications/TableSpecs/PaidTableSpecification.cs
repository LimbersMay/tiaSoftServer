using System.Linq.Expressions;
using TiaSoftBackend.Data.Constants;
using TiaSoftBackend.Data.Entities;

namespace TiaSoftBackend.Data.Specifications.TableSpecs;

public class PaidTableSpecification: Specification<TableEntity>
{
    public override Expression<Func<TableEntity, bool>> ToExpression()
    {
        return t => t.TableStatus.Name == TableStatusConstants.Pagado.ToString();
    }
}