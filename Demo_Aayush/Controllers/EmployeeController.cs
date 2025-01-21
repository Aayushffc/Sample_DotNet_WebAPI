using Microsoft.AspNetCore.Mvc;

namespace Demo_Aayush
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IDBHelper _dbHelper;

        public EmployeeController(IDBHelper helper)
        {
            _dbHelper = helper;
        }

        [HttpGet("GetAllEmployees")]
        public async Task<IActionResult> GetAllEmployees()
        {
            var response = await _dbHelper.GetAllEmployeesAsync();

            if (response.Status.Contains("Error", System.StringComparison.OrdinalIgnoreCase))
                return StatusCode(response.StatusCode, new { response.Status, response.Message });

            return Ok(new { response.Status, response.Message, response.Data });
        }

        [HttpGet("GetEmployeeById/{id}")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            var response = await _dbHelper.GetEmployeeByIdAsync(id);

            if (response.Status.Contains("Error", System.StringComparison.OrdinalIgnoreCase))
                return StatusCode(response.StatusCode, new { response.Status, response.Message });

            return Ok(new { response.Status, response.Message, response.Data });
        }

        [HttpPost("AddEmployee")]
        public async Task<IActionResult> AddEmployee(DTO_Employee employeeDto)
        {
            var response = await _dbHelper.AddEmployeeAsync(employeeDto);

            if (response.Status.Contains("Error", System.StringComparison.OrdinalIgnoreCase))
                return StatusCode(response.StatusCode, new { response.Status, response.Message });

            return StatusCode(201, new { response.Status, response.Message, response.Data });
        }

        [HttpPut("UpdateEmployee")]
        public async Task<IActionResult> UpdateEmployee(DTO_Employee employeeDto)
        {
            var response = await _dbHelper.UpdateEmployeeAsync(employeeDto);

            if (response.Status.Contains("Error", System.StringComparison.OrdinalIgnoreCase))
                return StatusCode(response.StatusCode, new { response.Status, response.Message });

            return Ok(new { response.Status, response.Message, response.Data });
        }

        [HttpGet("GetEmployeeDepartment/{employeeId}")]
        public async Task<IActionResult> GetEmployeeDepartment(int employeeId)
        {
            var response = await _dbHelper.GetEmployeeDepartmentAsync(employeeId);

            if (response.Status.Contains("Error", System.StringComparison.OrdinalIgnoreCase))
                return StatusCode(response.StatusCode, new { response.Status, response.Message });

            return Ok(new { response.Status, response.Message, response.Data });
        }

        [HttpDelete("DeleteEmployee/{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var response = await _dbHelper.DeleteEmployeeAsync(id);

            if (response.Status.Contains("Error", System.StringComparison.OrdinalIgnoreCase))
                return StatusCode(response.StatusCode, new { response.Status, response.Message });

            return Ok(new { response.Status, response.Message, response.Data });
        }
    }
}