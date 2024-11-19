using System.Linq.Expressions;
using TiaSoftBackend.Data.Entities;

namespace TiaSoftBackend.Data.Specifications.BillSpecs;

public class BillIdSpecification(string billId): Specification<Bill>
{
    public override Expression<Func<Bill, bool>> ToExpression()
        => bill => bill.BillId == billId;
}