using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.ISpecifications;

namespace Talabat.Repository
{
    public static class SpecificationEvalutor<TEntity> where TEntity : BaseEntity
    {

        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> InputQuery, ISpecification<TEntity> spc)
        {
            var query = InputQuery;  // _context.Products

            if (spc.wheres is not null)

                query = InputQuery.Where(spc.wheres);// _context.Products.Where(a=> a. Id == 1)

            query = spc.Includes.Aggregate(query, (currantQuery,incloudExpression) => currantQuery.Include(incloudExpression));

            return query;
 

        }
    }
}
