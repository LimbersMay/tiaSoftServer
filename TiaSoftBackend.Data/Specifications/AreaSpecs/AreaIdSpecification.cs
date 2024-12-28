using System.Linq.Expressions;
using TiaSoftBackend.Data.Entities;

namespace TiaSoftBackend.Data.Specifications.AreaSpecs;

public class AreaIdSpecification (string areaId) : Specification<Area>
{
    public override Expression<Func<Area, bool>> ToExpression()
    {
        return a => a.AreaId == areaId;
    }
}