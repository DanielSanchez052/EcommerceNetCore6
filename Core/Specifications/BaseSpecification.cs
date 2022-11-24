
using System.Linq.Expressions;

namespace Core.Specifications
{
    public class BaseSpecification<T> : ISpecification<T>
    {
        public Expression<Func<T, bool>> Criteria { get; }
        public List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();
        public List<string> IncludeStrings { get; } = new List<string>();

        public Expression<Func<T, object>> OrderBy { get; private set; }

        public Expression<Func<T, object>> OrderByDesc { get; private set; }

        public int Take { get; private set; }

        public int Skip { get; private set; }

        public bool IsPagingEnabled  { get; private set; }

        public BaseSpecification() { }
        public BaseSpecification(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }

        protected void AddInclude(Expression<Func<T, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }

        protected void AddInclude(string IncludeString)
        {
            IncludeStrings.Add(IncludeString);
        }

        protected void AddOrderBy(Expression<Func<T, object>> orderByException) {
            OrderBy = orderByException;
        }
        protected void AddOrderByDesc(Expression<Func<T, object>> orderByDescException)
        {
            OrderByDesc = orderByDescException;
        }

        protected void ApplyPaging(int skip, int take) {
            Skip = skip;
            Take = take;
            IsPagingEnabled = true;
        }
    }
}