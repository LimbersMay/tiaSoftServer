using System.Linq.Expressions;
using TiaSoftBackend.Entities;

namespace TiaSoftBackend.Specifications.TableSpecs;

public class TableNameSpecification : Specification<TableEntity>
{
    private readonly string _tableName;

    public TableNameSpecification(string tableName)
    {
        _tableName = tableName;
    }

    public override Expression<Func<TableEntity, bool>> ToExpression()
    {
        return t => t.Name == _tableName;
    }
}