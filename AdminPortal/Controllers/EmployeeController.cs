using AdminPortal.Data;
using AdminPortal.Models;
using AdminPortal.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdminPortal.Controllers
{
    //localhost://xxxx/api/employee
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly ApplicationDBContext _dbContext;
        public EmployeeController(ApplicationDBContext dBContext)
        {
            this._dbContext = dBContext;
        }

        private Employee FindEmployee(Guid id)
        {
            return _dbContext.Employees.Find(id);
        }

        [HttpGet]
        public IActionResult GetAllEmployees()
        {
            var allEmployees = _dbContext.Employees.ToList();
            return Ok(allEmployees);
        }

        [HttpPost]
        public IActionResult AddEmployee(EmployeeDTO employeeDTO)
        {
            var employee = new Employee() { Name = employeeDTO.Name, Email = employeeDTO.Email, Phone = employeeDTO.Phone, Salary = employeeDTO.Salary};
            _dbContext.Employees.Add(employee);
            _dbContext.SaveChanges();
            return Ok(employee);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetEmployeeById(Guid id)
        {
            var employee = FindEmployee(id);
            if (employee is null)
            {
                return NotFound();
            }
            return Ok(employee);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public IActionResult UpdateEmployee(Guid id, EmployeeDTO employeeDTO)
        {
            var employee = FindEmployee(id);
            if (employee is null)
            {
                return NotFound();
            }

            employee.Name = employeeDTO.Name;
            employee.Email = employeeDTO.Email;
            employee.Phone = employeeDTO.Phone;
            employee.Salary = employeeDTO.Salary;

            _dbContext.SaveChanges();
            return Ok(employee);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public IActionResult DeleteEmployeeById(Guid id)
        {
            var employee = FindEmployee(id);
            if (employee is null)
            {
                return NotFound();
            }
            _dbContext.Employees.Remove(employee);
            _dbContext.SaveChanges();
            return Ok();
        }

    }
}
