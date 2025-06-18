using Domain.Models;

namespace Domain.Contracts;

public interface IGenericReposistory<TEntity ,Tkey> where TEntity : BaseEntity<Tkey>
{
    void Add(TEntity entity);
    void Update(TEntity entity);
    void Delete(TEntity entity);
    Task<TEntity?> GetAsynce(Tkey key);
    Task<int> CountAsync (ISpesification<TEntity> spesification);
    Task<TEntity?> GetAsynce(ISpesification<TEntity> spesification);
    Task<IEnumerable<TEntity>> GetAllAsynce();
    Task<IEnumerable<TEntity>> GetAllAsynce(ISpesification<TEntity> spesification);
}
