using System.Linq.Expressions;
using TiaSoftBackend.Data.Entities;

namespace TiaSoftBackend.Data.Specifications.TableSpecs;

public class TableIdSpecification : Specification<TableEntity>
{
    private readonly string _tableId;
    
    public TableIdSpecification(string tableId)
    {
        _tableId = tableId;
    }
    
    public override Expression<Func<TableEntity, bool>> ToExpression()
    {
        return t => t.TableId == _tableId;
    }
}