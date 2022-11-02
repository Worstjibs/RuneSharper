using Microsoft.EntityFrameworkCore;
using RuneSharper.Shared.Entities;

namespace RuneSharper.Data.Specifications;

internal static class SpecificationEvaluator
{
    public static IQueryable<TEntity> GetQuery<TEntity>(
        IQueryable<TEntity> inputQueryable,
        Specification<TEntity> specification)
        where TEntity : BaseEntity
    {
        var queryable = inputQueryable;

        if (specification.Criteria is not null)
        {
            queryable = queryable.Where(specification.Criteria);
        }

        queryable = specification.IncludeExpressions.Aggregate(
            queryable,
            (current, include) => current.Include(include));

        if (specification.OrderByExpression is not null)
        {
            queryable = queryable.OrderBy(specification.OrderByExpression);
        } else if (specification.OrderByDescendingExpression is not null)
        {
            queryable = queryable.OrderByDescending(specification.OrderByDescendingExpression);
        }

        if (specification.Skip.HasValue)
            queryable.Skip(specification.Skip.Value);

        if (specification.Take.HasValue)
            queryable.Take(specification.Take.Value);

        return queryable;
    }
}
