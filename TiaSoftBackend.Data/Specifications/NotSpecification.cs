using System.Linq.Expressions;
namespace TiaSoftBackend.Data.Specifications;

public class NotSpecification<T> : Specification<T>
{
    public Specification<T> InnerSpecification { get; }

    public NotSpecification(Specification<T> specification)
    {
        InnerSpecification = specification;
    }

    public override Expression<Func<T, bool>> ToExpression()
    {
        Expression<Func<T, bool>> innerExpression = InnerSpecification.ToExpression();
        UnaryExpression notExpression = Expression.Not(innerExpression.Body);

        return Expression.Lambda<Func<T, bool>>(notExpression, innerExpression.Parameters.Single());
    }
}