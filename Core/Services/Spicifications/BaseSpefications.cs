using System.Linq.Expressions;
using Domain.Contracts;

namespace Services.Spicifications
{
    public abstract class BaseSpefications<T> : ISpesification<T> where T : class
    {
        protected BaseSpefications(Expression<Func<T, bool>>? criteria) => Criteria = criteria;
        public Expression<Func<T, bool>> Criteria { get; private set; }

        public List<Expression<Func<T, object>>> IncludeExpressions { get; } = [] ;

        public Expression<Func<T, object>> OrderBy { get; private set; }

        public Expression<Func<T, object>> OrderByDescending { get; private set; }

        public int Skip {  get; private set; }

        public int Take {  get; private set; }

        public bool IsPaginated {  get; private set; }

        protected void ApplyPagination (int pageSize, int pageIndex )
        {
            IsPaginated = true;
            Take = pageSize;
            Skip = (pageIndex - 1) * pageSize;
        }

        protected void addInclude(Expression<Func<T, object>> expression)
            => IncludeExpressions.Add(expression);

        protected void addOrderBy(Expression<Func<T, object>> orderBy)
            => OrderBy= orderBy;
        protected void addOrderyDescending(Expression<Func<T, object>> orderByDesc)
            => OrderByDescending = orderByDesc;

    }
}
