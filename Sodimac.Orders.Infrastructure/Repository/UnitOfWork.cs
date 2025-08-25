using Sodimac.Orders.Application.Interfaces;
using Sodimac.Orders.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sodimac.Orders.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private readonly Dictionary<Type, object> _repositories = new();

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public IRepository<T> Repository<T>() where T : class
        {
            if (!_repositories.ContainsKey(typeof(T)))
            {
                var repositoryInstance = new Repository<T>(_context);
                _repositories.Add(typeof(T), repositoryInstance);
            }
            return (IRepository<T>)_repositories[typeof(T)];
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
