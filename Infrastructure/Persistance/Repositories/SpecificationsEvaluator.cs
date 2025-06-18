using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Persistance.Repositories
{
    internal static class SpecificationsEvaluator
    {
        public static IQueryable<T> CreateQuary<T> (IQueryable<T> inputQuary , ISpesification<T> spesifications)
            where T : class
        {
            var quary = inputQuary;
            if(spesifications is not null && spesifications.Criteria is not null)
                quary = quary.Where(spesifications.Criteria);

            quary = spesifications.IncludeExpressions.Aggregate(quary,
                (currentQuary, includes) => currentQuary.Include(includes));
            if (spesifications.OrderBy is not null)
                quary = quary.OrderBy(spesifications.OrderBy);
            else if(spesifications.OrderByDescending is not null)
                    quary = quary.OrderByDescending(spesifications.OrderByDescending);
            if(spesifications.IsPaginated)
            {
                quary = quary.Skip(spesifications.Skip).Take(spesifications.Take);
            }
            return quary;
        }
    }
}
