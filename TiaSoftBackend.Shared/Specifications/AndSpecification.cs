using System.Linq.Expressions;
namespace TiaSoftBackend.Shared.Specifications;

public sealed class AndSpecification<T> : Specification<T>
{
    private Specification<T> Left { get; }
    private Specification<T> Right { get; }
    
    public AndSpecification(Specification<T> left, Specification<T> right)
    {
        Left = left;
        Right = right;
    }

    public override Expression<Func<T, bool>> ToExpression()
    {
        Expression<Func<T, bool>> leftExpression = Left.ToExpression();
        Expression<Func<T, bool>> rightExpression = Right.ToExpression();
        
        var parameter = Expression.Parameter(typeof(T));
        
        var combined = Expression.AndAlso(
            leftExpression.Body,
            rightExpression.Body
        );
        
        combined = (BinaryExpression) new ParameterReplacer(parameter).Visit(combined);
        
        return Expression.Lambda<Func<T, bool>>(combined, parameter);
    }
    
    private class ParameterReplacer : ExpressionVisitor
    {
        private readonly ParameterExpression _parameter;
        
        public ParameterReplacer(ParameterExpression parameter)
        {
            _parameter = parameter;
        }
        
        protected override Expression VisitParameter(ParameterExpression node)
        {
            return _parameter;
        }
    }
}