using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Talabat.Core.Entities;
using Talabat.Core.IRepositories;
using Talabat.Core.ISpecifications;

namespace Talabat.Controllers
{
   
    public class EmployeeController : BaseApiController
    {
        private readonly IGenericRepositry<Employee> _emprepo;

        public EmployeeController(IGenericRepositry<Employee> emprepo)
        {
            _emprepo = emprepo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees() { 
            var spec = new EmployeeWithBrandAndTypeSpecification();

            var emp  = await _emprepo.GetAllWithSpecAsync(spec);
            return Ok(emp);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployeeById(int id)  {
            var spec = new EmployeeWithBrandAndTypeSpecification(id);

            var empid = await _emprepo.GetByIdWithSpecAsync(spec);
            return Ok(empid);

        }

    }
}
