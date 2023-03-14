using Fullstack.api.Data;
using Fullstack.api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fullstack.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployyesController : Controller
    {
        private FullStackDBContext _fullStackDBContext;



        public EmployyesController(FullStackDBContext fullStackDBContext)
        {
            _fullStackDBContext = fullStackDBContext ?? throw new ArgumentNullException(nameof(fullStackDBContext));
        }
        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await _fullStackDBContext.Employees.ToListAsync();
            return Ok(employees);
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody] Employee emp)
        {
            emp.Id = Guid.NewGuid();
            await _fullStackDBContext.Employees.AddAsync(emp);
            await _fullStackDBContext.SaveChangesAsync();

            return Ok(emp);

        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> EditEmployee([FromRoute] Guid id)
        {
            var employee = await _fullStackDBContext.Employees.FirstOrDefaultAsync(x => x.Id == id);
            if (employee == null)
            {
                return NotFound();

            }

            return Ok(employee);
        }
        
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateEmployee([FromRoute] Guid id,Employee updateEmployeeRequest)
        {
            Employee employee =await _fullStackDBContext.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            employee.Name = updateEmployeeRequest.Name;
            employee.Email = updateEmployeeRequest.Email;
            employee.Salary = updateEmployeeRequest.Salary;
            employee.Phone = updateEmployeeRequest.Phone;
            employee.Department = updateEmployeeRequest.Department;

            await _fullStackDBContext.SaveChangesAsync();
            
            return Ok(employee);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteEmployee([FromRoute] Guid id)
        {
            var employee = await _fullStackDBContext.Employees.FindAsync(id);
            if(employee == null)
            {
                return NotFound();
            }

            _fullStackDBContext.Employees.Remove(employee);
            await _fullStackDBContext.SaveChangesAsync();

            return Ok(employee);
        } 


    }
}
