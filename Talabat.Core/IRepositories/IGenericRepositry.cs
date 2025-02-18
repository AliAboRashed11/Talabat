using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.ISpecifications;

namespace Talabat.Core.IRepositories
{
   public interface IGenericRepositry <T>  where T : BaseEntity
    {

        Task<IEnumerable<T>>GetAllAsync();

        Task<T> GetbyIdAsync(int id);

        Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecification<T> spec) ;

        Task<T> GetByIdWithSpecAsync(ISpecification<T>spec);
        Task<int>GetCountWithSpecAsync(ISpecification<T> spec);
        Task Add(T entity);

        void Update(T entity);
        void Delete(T entity);
    }
}
