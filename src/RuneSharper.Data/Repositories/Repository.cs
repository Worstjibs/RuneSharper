using Microsoft.EntityFrameworkCore;
using RuneSharper.Domain.Entities;
using RuneSharper.Data.Specifications;

namespace RuneSharper.Data.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseIntEntity
{
    protected readonly DbSet<TEntity> DbSet;
    protected readonly RuneSharperContext _context;

    public Repository(RuneSharperContext context)
    {
        DbSet = context.Set<TEntity>();
        _context = context;
    }

    public void Delete(TEntity entity)
    {
        DbSet.Remove(entity);
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await DbSet.ToListAsync();
    }

    public async Task<TEntity?> GetAsync(int id)
    {
        return await DbSet.FindAsync(id);
    }

    public void Insert(TEntity entity)
    {
        DbSet.Add(entity);
    }

    public void Update(TEntity entity)
    {
        DbSet.Update(entity);
    }

    public async Task<bool> Complete()
    {
        return await _context.SaveChangesAsync() > 0;
    }

    internal IQueryable<TEntity> ApplySpecification(
        Specification<TEntity> specification)
    {
        return SpecificationEvaluator.GetQuery(
            DbSet, specification);
    }
}
