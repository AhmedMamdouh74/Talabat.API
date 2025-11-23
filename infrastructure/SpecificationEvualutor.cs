using Domain;
using Domain.Specifications;
using Microsoft.EntityFrameworkCore;

namespace infrastructure
{
    internal static class SpecificationEvaluator<TEntity> where TEntity : BaseEntity
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecification<TEntity> spec)
        {
            var query = inputQuery;
            // modify the IQueryable using the specification's criteria expression
            if (spec.Criteria != null)
            {
                query = query.Where(spec.Criteria);
            }
            // Includes all expression-based includes
            query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));
            return query;
        }
    }
}
