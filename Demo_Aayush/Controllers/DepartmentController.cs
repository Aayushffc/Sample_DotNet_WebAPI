using Demo_Aayush.DTOs;
using Demo_Aayush.Helper;
using Microsoft.AspNetCore.Mvc;

namespace Demo_Aayush.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentController : ControllerBase
    {
        private readonly IDBHelper _dbHelper;

        public DepartmentController(IDBHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        /// <summary>
        /// Retrieves all departments from the database.
        /// </summary>
        /// <returns>A list of all departments.</returns>
        [HttpGet("GetAllDepartments")]
        public async Task<IActionResult> GetAllDepartments()
        {
            var response = await _dbHelper.GetAllDepartmentsAsync();

            if (response.Status.Contains("Error", StringComparison.OrdinalIgnoreCase))
                return StatusCode(response.StatusCode, new { response.Status, response.Message });

            return Ok(new { response.Status, response.Message, response.Data }); 
        }

        /// <summary>
        /// Retrieves a department's details by its ID.
        /// </summary>
        /// <param name="id">The ID of the department.</param>
        /// <returns>The department details, if found.</returns>
        [HttpGet("GetDepartmentById/{id}")]
        public async Task<IActionResult> GetDepartmentById(int id)
        {
            var response = await _dbHelper.GetDepartmentByIdAsync(id);

            if (response.Status.Contains("Error", StringComparison.OrdinalIgnoreCase))
                return StatusCode(response.StatusCode, new { response.Status, response.Message });

            return Ok(new { response.Status, response.Message, response.Data });
        }

        /// <summary>
        /// Adds a new department to the database.
        /// </summary>
        /// <param name="departmentDto">The details of the department to add.</param>
        /// <returns>The added department's details.</returns>
        [HttpPost("AddDepartment")]
        public async Task<IActionResult> AddDepartment(DTO_Department departmentDto)
        {
            var response = await _dbHelper.AddDepartmentAsync(departmentDto);

            if (response.Status.Contains("Error", StringComparison.OrdinalIgnoreCase))
                return StatusCode(response.StatusCode, new { response.Status, response.Message });

            return StatusCode(201, new { response.Status, response.Message, response.Data });
        }

        /// <summary>
        /// Updates an existing department's details in the database.
        /// </summary>
        /// <param name="departmentDto">The updated details of the department.</param>
        /// <returns>The updated department's details.</returns>
        [HttpPut("UpdateDepartment")]
        public async Task<IActionResult> UpdateDepartment(DTO_Department departmentDto)
        {
            var response = await _dbHelper.UpdateDepartmentAsync(departmentDto);

            if (response.Status.Contains("Error", StringComparison.OrdinalIgnoreCase))
                return StatusCode(response.StatusCode, new { response.Status, response.Message });

            return Ok(new { response.Status, response.Message, response.Data });
        }

        /// <summary>
        /// Deletes a department from the database by ID.
        /// </summary>
        /// <param name="id">The ID of the department to delete.</param>
        /// <returns>A success message if the deletion was successful.</returns>
        [HttpDelete("DeleteDepartment/{id}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            var response = await _dbHelper.DeleteDepartmentAsync(id);

            if (response.Status.Contains("Error", StringComparison.OrdinalIgnoreCase))
                return StatusCode(response.StatusCode, new { response.Status, response.Message });

            return Ok(new { response.Status, response.Message, response.Data });
        }
    }
}
