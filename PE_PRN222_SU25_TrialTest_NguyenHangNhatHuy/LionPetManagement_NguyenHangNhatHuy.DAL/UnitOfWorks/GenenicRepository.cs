
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LionPetManagement_NguyenHangNhatHuy.DAL.UnitOfWorks
{
    public class GenericRepository<T> where T : class
    {
        protected readonly Su25lionDbContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(Su25lionDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public List<T> GetAll(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return query.ToList();
        }

        //Lấy data có phân trang
        public IQueryable<T> GetFiltered(int? page = null, int? pageSize = null, params Expression<Func<T, object>>[]? includes)
        {
            IQueryable<T> query = _dbSet;

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            if (page.HasValue && pageSize.HasValue && page > 0 && pageSize > 0)
            {
                query = query.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value);
            }

            return query;
        }

        //Lấy theo ID
        public T GetById(object id, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;

            var entityType = _context.Model.FindEntityType(typeof(T));
            var keyName = entityType?.FindPrimaryKey()?.Properties.FirstOrDefault()?.Name;

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return query.FirstOrDefault(e => EF.Property<object>(e, keyName).Equals(id));
        }

        //VD: var user = userRepository.FindByCondition(u => u.Email == "example@gmail.com");
        public T FindByCondition(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet.Where(predicate);
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            return query.FirstOrDefault();
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }
    }
}
