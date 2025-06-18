
using Domain.Contracts;

namespace Persistance.Repositories;

internal class GenericRepository<TEntity, Tkey>(StoreDbContext context) : IGenericReposistory<TEntity, Tkey> 
    where TEntity : BaseEntity<Tkey>
{
    public void Add(TEntity entity) => context.Set<TEntity>().Add(entity);

    public void Delete(TEntity entity) => context.Set<TEntity>().Remove(entity);
    public void Update(TEntity entity) => context.Set<TEntity>().Update(entity);

    public async Task<IEnumerable<TEntity>> GetAllAsynce() => await context.Set<TEntity>().ToListAsync();

    public async Task<TEntity?> GetAsynce(Tkey key) => await context.Set<TEntity>().FindAsync(key);

    public async Task<TEntity?> GetAsynce(ISpesification<TEntity> spesification) =>
        await SpecificationsEvaluator.CreateQuary(context.Set<TEntity>(), spesification).FirstOrDefaultAsync();
    

    public async Task<IEnumerable<TEntity>> GetAllAsynce(ISpesification<TEntity> spesification) =>
       await SpecificationsEvaluator.CreateQuary(context.Set<TEntity>(), spesification).ToListAsync();

    public async Task<int> CountAsync(ISpesification<TEntity> spesification) => await SpecificationsEvaluator
        .CreateQuary(context.Set<TEntity>(), spesification).CountAsync();

}
