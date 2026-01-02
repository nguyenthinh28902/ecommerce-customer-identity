using CustomerIdentityService.Core.Abstractions.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerIdentityService.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext dbContext;
        private Dictionary<Type, object> _repositories;
        public UnitOfWork(DbContext context)
        {
            dbContext = context;
        }

        public IRepository<T> Repository<T>() where T : class
        {
            IRepository<T> repository = null;
            if (_repositories.ContainsKey(typeof(T)))
            {
                repository = _repositories[typeof(T)] as IRepository<T>;
            }
            else
            {
                repository = new Repository<T>(dbContext);
                _repositories.Add(typeof(T), repository);
            }

            return (Repository<T>)repository;
        }
        public async Task DisposeAsync()
        {
            await dbContext.DisposeAsync();
        }

        public async Task SaveChangesAsync()
        {
            await dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            dbContext.Dispose();
        }

        public void SaveChanges()
        {
            dbContext.SaveChanges();
        }
    }
}
