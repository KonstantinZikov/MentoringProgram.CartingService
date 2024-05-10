using DAL.Common;
using DAL.Common.Interface;
using LiteDB;

namespace Infrastructure.Data;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
{
    private readonly ILiteDatabaseConfiguration _config;

    public Repository(ILiteDatabaseConfiguration liteDatabaseConfiguration)
    {
        _config = liteDatabaseConfiguration;
    }

    public async Task<TEntity> GetById(Guid id)
    {
        using (var db = new LiteDatabase(_config.ConnectionString))
        {
            var col = db.GetCollection<TEntity>();
            return col.FindOne(e => e.Id.Equals(id));
        }
    }

    public async Task<IEnumerable<TEntity>> List()
    {
        using (var db = new LiteDatabase(_config.ConnectionString))
        {
            return db.GetCollection<TEntity>().FindAll().ToList();
        }

    }

    public async Task<IEnumerable<TEntity>> List(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate)
    {
        using (var db = new LiteDatabase(_config.ConnectionString))
        {
            return db.GetCollection<TEntity>().Find(predicate).ToList();
        }
    }

    public async Task<Guid> Insert(TEntity entity)
    {
        using (var db = new LiteDatabase(_config.ConnectionString))
        {
            var col = db.GetCollection<TEntity>();
            return col.Insert(entity);
        }
    }

    public async Task Update(TEntity entity)
    {
        using (var db = new LiteDatabase(_config.ConnectionString))
        {
            var col = db.GetCollection<TEntity>();
            col.Update(entity);
        }
    }

    public async Task Delete(int id)
    {
        using (var db = new LiteDatabase(_config.ConnectionString))
        {
            var col = db.GetCollection<TEntity>();
            col.DeleteMany(e => e.Id.Equals(id));
        }
    }
}
