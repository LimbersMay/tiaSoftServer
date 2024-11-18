using System.Linq.Expressions;
namespace TiaSoftBackend.Data.Specifications;

public class OrSpecification<T> : Specification<T>
{
    public Specification<T> Left { get; }
    public Specification<T> Right { get; }

    public OrSpecification(Specification<T> left, Specification<T> right)
    {
        Left = left;
        Right = right;
    }

    public override Expression<Func<T, bool>> ToExpression()
    {
        Expression<Func<T, bool>> leftExpression = Left.ToExpression();
        Expression<Func<T, bool>> rightExpression = Right.ToExpression();

        BinaryExpression orExpression = Expression.OrElse(leftExpression.Body, rightExpression.Body);

        return Expression.Lambda<Func<T, bool>>(orExpression, leftExpression.Parameters.Single());
    }
}