using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Entities.Order_Aggregate;
using Talabat.Core.IRepositories;
using Talabat.Repository.Data;

namespace Talabat.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreContext _context;
        private Hashtable _repositories;
  
        public UnitOfWork(StoreContext context)
        {
            _context = context;
            _repositories = new Hashtable();
        }



        public IGenericRepositry<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
           var type = typeof(TEntity).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repository = new GenericRepository<TEntity>(_context);
                _repositories.Add(type, repository);
            }
            return _repositories[type] as IGenericRepositry<TEntity>;
        }


        public async Task<int> Complete()
              => await _context.SaveChangesAsync();
        

        public async ValueTask DisposeAsync()
        =>
          await _context.DisposeAsync();
        


    }
}
