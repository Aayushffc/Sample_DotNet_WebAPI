using Demo_Aayush.DTOs;
using Demo_Aayush.Helper;
using Microsoft.AspNetCore.Mvc;

namespace Demo_Aayush.Controllers
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

        /// <summary>
        /// Retrieves all employees from the database.
        /// </summary>
        /// <returns>A list of all employees.</returns>
        [HttpGet("GetAllEmployees")]
        public async Task<IActionResult> GetAllEmployees() 
        {
            var response = await _dbHelper.GetAllEmployeesAsync();

            if (response.Status.Contains("Error", StringComparison.OrdinalIgnoreCase))
                return StatusCode(response.StatusCode, new { response.Status, response.Message });

            return Ok(new { response.Status, response.Message, response.Data });
        }

        /// <summary>
        /// Retrieves an employee's details by their ID.
        /// </summary>
        /// <param name="id">The ID of the employee.</param>
        /// <returns>The employee details, if found.</returns>
        [HttpGet("GetEmployeeById/{id}")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            var response = await _dbHelper.GetEmployeeByIdAsync(id);

            if (response.Status.Contains("Error", StringComparison.OrdinalIgnoreCase))
                return StatusCode(response.StatusCode, new { response.Status, response.Message });

            return Ok(new { response.Status, response.Message, response.Data });
        }

        /// <summary>
        /// Adds a new employee to the database.
        /// </summary>
        /// <param name="employeeDto">The details of the employee to add.</param>
        /// <returns>The added employee's details.</returns>
        [HttpPost("AddEmployee")]
        public async Task<IActionResult> AddEmployee(DTO_Employee employeeDto)
        {
            var response = await _dbHelper.AddEmployeeAsync(employeeDto);

            if (response.Status.Contains("Error", StringComparison.OrdinalIgnoreCase))
                return StatusCode(response.StatusCode, new { response.Status, response.Message });

            return StatusCode(201, new { response.Status, response.Message, response.Data });
        }

        /// <summary>
        /// Updates an existing employee's details in the database.
        /// </summary>
        /// <param name="employeeDto">The updated details of the employee.</param>
        /// <returns>The updated employee's details.</returns>
        [HttpPut("UpdateEmployee")]
        public async Task<IActionResult> UpdateEmployee(DTO_Employee employeeDto)
        {
            var response = await _dbHelper.UpdateEmployeeAsync(employeeDto);

            if (response.Status.Contains("Error", StringComparison.OrdinalIgnoreCase))
                return StatusCode(response.StatusCode, new { response.Status, response.Message });

            return Ok(new { response.Status, response.Message, response.Data });
        }

        /// <summary>
        /// Retrieves the department details of a specific employee.
        /// </summary>
        /// <param name="employeeId">The ID of the employee.</param>
        /// <returns>The department details of the employee.</returns>
        [HttpGet("GetEmployeeDepartment/{employeeId}")]
        public async Task<IActionResult> GetEmployeeDepartment(int employeeId)
        {
            var response = await _dbHelper.GetEmployeeDepartmentAsync(employeeId);

            if (response.Status.Contains("Error", StringComparison.OrdinalIgnoreCase))
                return StatusCode(response.StatusCode, new { response.Status, response.Message });

            return Ok(new { response.Status, response.Message, response.Data });
        }

        /// <summary>
        /// Deletes an employee record from the database by ID.
        /// </summary>
        /// <param name="id">The ID of the employee to delete.</param>
        /// <returns>A success message if the deletion was successful.</returns>
        [HttpDelete("DeleteEmployee/{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var response = await _dbHelper.DeleteEmployeeAsync(id);

            if (response.Status.Contains("Error", StringComparison.OrdinalIgnoreCase))
                return StatusCode(response.StatusCode, new { response.Status, response.Message });

            return Ok(new { response.Status, response.Message, response.Data });
        }
    }
}