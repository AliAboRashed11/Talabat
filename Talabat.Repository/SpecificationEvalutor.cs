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
            var query = InputQuery;  

            if (spc.wheres is not null)

                query = InputQuery.Where(spc.wheres);

            if (spc.OrderBy is not null)
               query = query.OrderBy(spc.OrderBy);

            if(spc.OrderByDescending is not null)

                query = query.OrderByDescending(spc.OrderByDescending);

            if (spc.IsPaginationEnabled)

                query = query.Skip(spc.Skip).Take(spc.Take);


                query = spc.Includes.Aggregate(query, (currantQuery,incloudExpression) => currantQuery.Include(incloudExpression));

            
            return query;
 

        }
    }
}
