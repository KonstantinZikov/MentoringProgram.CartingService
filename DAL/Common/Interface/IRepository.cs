using System.Linq.Expressions;

namespace DAL.Common.Interface
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        Task<TEntity?> GetById(Guid id);

        Task<IEnumerable<TEntity>> List();

        Task<IEnumerable<TEntity>> List(Expression<Func<TEntity, bool>> predicate);

        Task<Guid> Insert(TEntity entity);

        Task Update(TEntity entity);

        Task Delete(int id);      
    }
}
