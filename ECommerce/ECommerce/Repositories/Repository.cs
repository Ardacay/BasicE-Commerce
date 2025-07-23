using ECommerce.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ECommerce.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly AppDbContext _context;
        public Repository(AppDbContext context)
        {
            _context = context;
        }

        public void Add(T Model)
        {
            _context.Set<T>().Add(Model);
            _context.SaveChanges();

        }

        public void AddRange(IEnumerable<T> Model)
        {
            _context.Set<T>().AddRange(Model);
            _context.SaveChanges();
        }

        public int Count()
        {
            return _context.Set<T>().Count();
        }

        public async Task<int> CountAsync()
        {
            return await _context.Set<T>().CountAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public T? Get(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().FirstOrDefault(predicate);
        }

        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await Task.Run(() => _context.Set<T>());
        }

        public async Task<T?> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(predicate);
        }

        public T? GetId(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public async Task<T?> GetIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public IEnumerable<T> GetList(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Where<T>(predicate).ToList();
        }

        public async Task<IEnumerable<T>> GetListAsync(Expression<Func<T, bool>> predicate)
        {
            return await Task.Run(() => _context.Set<T>().Where<T>(predicate));
        }

        public void Remove(T Model)
        {
            _context.Set<T>().Remove(Model);
            _context.SaveChanges();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(T Model)
        {
            _context.Update(Model);
            _context.SaveChanges();
        }
    }
}
