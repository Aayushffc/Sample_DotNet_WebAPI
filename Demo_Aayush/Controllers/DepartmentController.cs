using Microsoft.AspNetCore.Mvc;

namespace Demo_Aayush
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

        [HttpGet("GetAllDepartments")]
        public async Task<IActionResult> GetAllDepartments()
        {
            var response = await _dbHelper.GetAllDepartmentsAsync();

            if (response.Status.Contains("Error", System.StringComparison.OrdinalIgnoreCase))
                return StatusCode(response.StatusCode, new { response.Status, response.Message });

            return Ok(new { response.Status, response.Message, response.Data });
        }

        [HttpGet("GetDepartmentById/{id}")]
        public async Task<IActionResult> GetDepartmentById(int id)
        {
            var response = await _dbHelper.GetDepartmentByIdAsync(id);

            if (response.Status.Contains("Error", System.StringComparison.OrdinalIgnoreCase))
                return StatusCode(response.StatusCode, new { response.Status, response.Message });

            return Ok(new { response.Status, response.Message, response.Data });
        }

        [HttpPost("AddDepartment")]
        public async Task<IActionResult> AddDepartment(DTO_Department departmentDto)
        {
            var response = await _dbHelper.AddDepartmentAsync(departmentDto);

            if (response.Status.Contains("Error", System.StringComparison.OrdinalIgnoreCase))
                return StatusCode(response.StatusCode, new { response.Status, response.Message });

            return StatusCode(201, new { response.Status, response.Message, response.Data });
        }

        [HttpPut("UpdateDepartment")]
        public async Task<IActionResult> UpdateDepartment(DTO_Department departmentDto)
        {
            var response = await _dbHelper.UpdateDepartmentAsync(departmentDto);

            if (response.Status.Contains("Error", System.StringComparison.OrdinalIgnoreCase))
                return StatusCode(response.StatusCode, new { response.Status, response.Message });

            return Ok(new { response.Status, response.Message, response.Data });
        }

        [HttpDelete("DeleteDepartment/{id}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            var response = await _dbHelper.DeleteDepartmentAsync(id);

            if (response.Status.Contains("Error", System.StringComparison.OrdinalIgnoreCase))
                return StatusCode(response.StatusCode, new { response.Status, response.Message });

            return Ok(new { response.Status, response.Message, response.Data });
        }
    }
}
