namespace News.Domain.Core
{
    public interface IRepository<TEntity, K> where TEntity : Entity<K>
    {
        Task<IList<TEntity>> GetAllAsync();
        Task AddAsync(TEntity entityToAdd);
    }
}
