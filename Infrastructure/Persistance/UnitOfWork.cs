
using Persistance.Repositories;

namespace Persistance
{
    public class UnitOfWork(StoreDbContext context) : IUnitOFWork
    {
        private readonly Dictionary<string, object> _repositories = [];
        public IGenericReposistory<TEntity, Tkey> GetReposistory<TEntity, Tkey>() where TEntity : BaseEntity<Tkey>
        {
            var TypeName = typeof(TEntity).Name;
            if(_repositories.ContainsKey(TypeName))
            {
                return (IGenericReposistory<TEntity, Tkey>) _repositories[TypeName];
            }
            var repo = new GenericRepository<TEntity, Tkey>(context);
            _repositories.Add(TypeName, repo);
            return repo;
        }

        public IGenericReposistory<TEntity, int> GetReposistory<TEntity>() where TEntity : BaseEntity<int>
        {
            var TypeName = typeof(TEntity).Name;
            if (_repositories.ContainsKey(TypeName))
            {
                return (IGenericReposistory<TEntity, int>)_repositories[TypeName];
            }
            var repo = new GenericRepository<TEntity, int>(context);
            _repositories.Add(TypeName, repo);
            return repo;
        }

        public async Task<int> SaveChangesAsynce() => await context.SaveChangesAsync();
    }
}
