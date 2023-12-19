namespace DMS.Web.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
         
        protected readonly ApplicationDbContext db;
        internal DbSet<T> dbSet;


        public GenericRepository(ApplicationDbContext db)
        {
            this.db = db;
            dbSet = db.Set<T>();
        } 

        public async Task AddAsync(T entity)
        {
            await dbSet.AddAsync(entity);
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, string? IncludeProperties = null, bool tracked = false)
        {
            IQueryable<T> query = dbSet;
            if (!tracked)
            {
                dbSet.AsNoTracking();
            }
            if (filter is not null)
            {
                query = query.Where(filter);
            }

            if (!string.IsNullOrEmpty(IncludeProperties))
            {
                foreach (var prop in IncludeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(prop);
                }
            }
            return query.ToList();
        }


        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, string? IncludeProperties = null)
        {
            IQueryable<T> query = dbSet;
            if (filter is not null)
            {
                query = query.Where(filter);
            }

            if (!string.IsNullOrEmpty(IncludeProperties))
            {
                foreach (var prop in IncludeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(prop);
                }
            }
            return await query.ToListAsync();
        }

        public async Task<T> GetOneAsync(Expression<Func<T, bool>> filter, string? IncludeProperties = null, bool tracked = false)
        {
            IQueryable<T> query;
            if (tracked)
            {
                query = dbSet;
            }
            else
            {
                query = dbSet.AsNoTracking();
            }
            query = query.Where(filter);
            if (!string.IsNullOrEmpty(IncludeProperties))
            {
                foreach (var prop in IncludeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(prop);
                }
            }
            return await query.FirstOrDefaultAsync();
        }


        public T GetOne(Expression<Func<T, bool>> filter, string? IncludeProperties = null)
        {
            IQueryable<T> query;
          
                query = dbSet;
            
            query = query.Where(filter);
            if (!string.IsNullOrEmpty(IncludeProperties))
            {
                foreach (var prop in IncludeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(prop);
                }
            }
            return  query.FirstOrDefault();
        }


        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }

        
    }

}

