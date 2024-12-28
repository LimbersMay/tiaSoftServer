using System.Linq.Expressions;
using TiaSoftBackend.Data.Entities;
using TiaSoftBackend.Data.Constants;

namespace TiaSoftBackend.Data.Specifications.TableSpecs;

public class ActiveTableSpecification: Specification<TableEntity>
{
    public override Expression<Func<TableEntity, bool>> ToExpression()
    {
        return t => t.TableStatus.Name == TableStatusConstants.Activo.ToString();
    }
}