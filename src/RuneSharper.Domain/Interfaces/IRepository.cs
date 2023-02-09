using RuneSharper.Domain.Entities;

namespace RuneSharper.Domain.Interfaces;

public interface IRepository<TEntity> where TEntity : BaseIntEntity
{
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<TEntity?> GetAsync(int id);
    void Insert(TEntity entity);
    void Update(TEntity entity);
    void Delete(TEntity entity);
    Task<bool> Complete();
}
