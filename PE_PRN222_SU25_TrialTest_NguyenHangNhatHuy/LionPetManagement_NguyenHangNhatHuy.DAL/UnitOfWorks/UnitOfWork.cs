

namespace LionPetManagement_NguyenHangNhatHuy.DAL.UnitOfWorks
{
    public class UnitOfWork : IDisposable
    {
        private readonly Su25lionDbContext _dbContext;
        private readonly Dictionary<Type, object> _repositories = new Dictionary<Type, object>();

        public UnitOfWork(Su25lionDbContext myStoreContext)
        {
            _dbContext = myStoreContext;
        }

        public void Save()
        {
            
            var result = _dbContext.SaveChanges();
        }

        public GenericRepository<T> GetRepository<T>() where T : class
        {
            if (!_repositories.ContainsKey(typeof(T)))
            {
                _repositories[typeof(T)] = new GenericRepository<T>(_dbContext);
            }
            return (GenericRepository<T>)_repositories[typeof(T)];
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
