using System.Linq.Expressions;
using TiaSoftBackend.Data.Entities;

namespace TiaSoftBackend.Data.Specifications.TableSpecs;

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