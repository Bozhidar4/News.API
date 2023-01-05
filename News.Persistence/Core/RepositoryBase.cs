using Microsoft.EntityFrameworkCore;
using News.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace News.Persistence.Core
{
    public class RepositoryBase<T, C, K> : IRepository<T, K>
        where T : Entity<K>
        where C : DbContext
    {
        protected readonly DbSet<T> _set;
        protected readonly DbContext _dbContext;

        protected DbSet<T> Query
        {
            get { return _set; }
            set { }
        }

        public RepositoryBase(C dbContext)
        {
            _dbContext = dbContext;
            _set = dbContext.Set<T>();
        }

        public async Task<IList<T>> GetAllAsync()
        {
            return await _set.AsNoTracking().ToListAsync();
        }

        public async Task AddAsync(T entityToAdd)
        {
            await _set.AddAsync(entityToAdd);
        }
    }
}
