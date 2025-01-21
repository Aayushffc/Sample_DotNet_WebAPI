namespace Demo_code
{
    public interface IDBHelper
    {
        Task<DTO_Response<IEnumerable<DTO_Employee>>> GetAllEmployeesAsync();
        Task<DTO_Response<DTO_Employee>> GetEmployeeByIdAsync(int id);
        Task<DTO_Response<DTO_Employee>> AddEmployeeAsync(DTO_Employee employeeDto);
        Task<DTO_Response<DTO_Employee>> UpdateEmployeeAsync(DTO_Employee employeeDto);
        Task<DTO_Response<string>> DeleteEmployeeAsync(int id);
        Task<DTO_Response<IEnumerable<DTO_Department>>> GetAllDepartmentsAsync();
        Task<DTO_Response<DTO_Department>> GetDepartmentByIdAsync(int id);
        Task<DTO_Response<DTO_Department>> GetEmployeeDepartmentAsync(int employeeId);
        Task<DTO_Response<DTO_Department>> AddDepartmentAsync(DTO_Department departmentDto);
        Task<DTO_Response<DTO_Department>> UpdateDepartmentAsync(DTO_Department departmentDto);
        Task<DTO_Response<string>> DeleteDepartmentAsync(int id);
    }
}

