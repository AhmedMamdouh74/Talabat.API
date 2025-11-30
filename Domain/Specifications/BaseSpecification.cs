using System.Linq.Expressions;

namespace Domain.Specifications
{
    public class BaseSpecification<T> : ISpecification<T> where T : BaseEntity
    {
        public Expression<Func<T, bool>>? Criteria { get; set; }
        public List<Expression<Func<T, object>>> Includes { get; set; } = new List<Expression<Func<T, object>>>();
        public Expression<Func<T, object>> OrderByDesc { get ; set ; }
        public Expression<Func<T, object>> OrderBy { get ; set ; }

        public BaseSpecification()
        {
        }
        public BaseSpecification(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }
        public void AddOrderByDesc(Expression<Func<T, object>> orderByDesc)
        {
            OrderByDesc = orderByDesc;
        }
        public void AddOrderByAsc(Expression<Func<T, object>> orderByAsc)
        {
            OrderBy = orderByAsc;
        }
    }
}
