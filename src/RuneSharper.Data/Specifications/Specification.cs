using RuneSharper.Domain.Entities;
using System.Linq.Expressions;

namespace RuneSharper.Data.Specifications;

internal abstract class Specification<TEntity>
    where TEntity : BaseEntity
{
    protected Specification(Expression<Func<TEntity, bool>>? criteria)
    {
        Criteria = criteria;
    }

    public Expression<Func<TEntity, bool>>? Criteria { get; }
    public int? Take { get; protected set; }
    public int? Skip { get; protected set; }
    public List<Expression<Func<TEntity, object>>> IncludeExpressions { get; } = new();
    public Expression<Func<TEntity, object>>? OrderByExpression { get; private set; }
    public Expression<Func<TEntity, object>>? OrderByDescendingExpression { get; private set; }

    protected void AddInclude(Expression<Func<TEntity, object>> includePression) =>
        IncludeExpressions.Add(includePression);

    protected void AddOrderBy(
        Expression<Func<TEntity, object>> orderByExpression) =>
        OrderByExpression = orderByExpression;

    protected void AddOrderByDescending(
        Expression<Func<TEntity, object>> orderByDescendingExpression) =>
        OrderByDescendingExpression = orderByDescendingExpression;
}
