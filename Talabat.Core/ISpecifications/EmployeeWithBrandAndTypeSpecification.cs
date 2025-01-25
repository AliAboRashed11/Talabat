using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.ISpecifications
{
    public class EmployeeWithBrandAndTypeSpecification : BaseSpecification<Employee>
    {
        public EmployeeWithBrandAndTypeSpecification()
        {
            Includes.Add(a => a.Department);
        }

        public EmployeeWithBrandAndTypeSpecification(int Id) : base(a => a.Id == Id)
        {
            {
                Includes.Add(a => a.Department);
            }
        }
    }
}