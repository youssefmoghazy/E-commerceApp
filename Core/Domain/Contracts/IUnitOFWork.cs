using Domain.Models;

namespace Domain.Contracts
{
    public interface IUnitOFWork
    {
        Task<int> SaveChangesAsynce();

        IGenericReposistory<TEntity, Tkey> GetReposistory<TEntity, Tkey>()
            where TEntity : BaseEntity<Tkey>;
        IGenericReposistory<TEntity, int> GetReposistory<TEntity>()
            where TEntity : BaseEntity<int>;
    }
}
