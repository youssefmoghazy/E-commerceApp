using System.Linq.Expressions;

namespace Domain.Contracts;

public interface ISpesification<T> where T : class
{
    Expression<Func<T,bool>> Criteria { get; }

    List<Expression<Func<T,Object>>> IncludeExpressions { get; } 

    Expression<Func<T, object>> OrderBy { get; }

    Expression<Func<T,object>> OrderByDescending { get; }

    int Skip { get; }
    int Take { get; }
    bool IsPaginated { get; }

}
// where
// include
// select