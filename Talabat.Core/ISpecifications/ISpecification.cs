using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.ISpecifications
{
    public interface ISpecification <T> where T : BaseEntity
    {
        //Where Condation
        public Expression<Func<T,bool>> wheres { get; set; }

        public List<Expression<Func<T,Object>>> Includes { get; set; }
    }
}
