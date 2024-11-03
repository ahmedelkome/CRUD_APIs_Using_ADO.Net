using ADO.Net.Models.Employee;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ADO.Net.Controllers
{

    [ApiController]
    [Route("[controller]")]
   public class EmployeeController : ControllerBase
    {
        private readonly IEmployee _employee;

        public EmployeeController(IEmployee employee)
        {
            _employee = employee;
        }

        [HttpGet]
        public IActionResult selectEmployee(int id)
        {
            var employee = _employee.SelectEmployee(id);
            if (employee != null)
            {
                return Ok(employee);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult SaveEmployee(Employee employee)
        {
            var emp = _employee.SaveEmployee(employee);
            if (emp != null)
            {
                return Ok(employee);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPut]
        public IActionResult UpdateEmployee(Employee employee)
        {
            var emp = _employee.SaveEmployee(employee);
            if (emp != null)
            {
                return Ok(employee);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete]
        public IActionResult DeleteEmployee([Required]int id)
        {
            var result = _employee.DeleteEmployee(id);
            if (result)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
