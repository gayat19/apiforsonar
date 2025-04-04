using FirstAPI.Contexts;
using FirstAPI.Interfaces;

namespace FirstAPI.Repositories
{
    public abstract class Repository<K ,T> : IRepository<K, T> where T : class
    {
        public abstract Task<IEnumerable<T>> GetAll();
        public abstract Task<T> GetById(K id);
        protected readonly EmployeeManagementContext _context;
        protected Repository(EmployeeManagementContext context)
        {
            _context = context;
        }

        public async Task<T> Add(T entity)
        {
           _context.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<T> Delete(K id)
        {
            var entity = await GetById(id);
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        
        public async Task<T> Update(K key,T entity)
        {
            var newEntity = await GetById(key);
            _context.Entry(newEntity).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
