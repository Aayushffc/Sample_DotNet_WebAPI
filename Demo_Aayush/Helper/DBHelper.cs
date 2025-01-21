using Microsoft.Data.SqlClient;
using System.Data;

namespace Demo_Aayush
{
    public class DBHelper : IDBHelper
    {
        private readonly string _connectionString;

        public DBHelper(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<DTO_Response<IEnumerable<DTO_Employee>>> GetAllEmployeesAsync()
        {
            var employees = new List<DTO_Employee>();
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    using (var command = new SqlCommand("sp_GetAllEmployees", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        await connection.OpenAsync();

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                employees.Add(new DTO_Employee
                                {
                                    Id = reader.GetInt32("Id"),
                                    Name = reader.GetString("Name"),
                                    DepartmentId = reader.GetInt32("DepartmentId"),
                                    Salary = reader.GetDecimal("Salary"),
                                    DateOfJoining = reader.IsDBNull("DateOfJoining") ? null : reader.GetDateTime("DateOfJoining")
                                });
                            }
                        }
                    }
                }
                return new DTO_Response<IEnumerable<DTO_Employee>>
                {
                    Status = "Success",
                    StatusCode = 200,
                    Message = "Employees retrieved successfully",
                    Data = employees
                };
            }
            catch (Exception ex)
            {
                return new DTO_Response<IEnumerable<DTO_Employee>>
                {
                    Status = "Error",
                    StatusCode = 500,
                    Message = $"An error occurred: {ex.Message}",
                    Data = null
                };
            }
        }

        public async Task<DTO_Response<DTO_Employee>> GetEmployeeByIdAsync(int id)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    using (var command = new SqlCommand("sp_GetEmployeeById", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Id", id);

                        await connection.OpenAsync();
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                var employee = new DTO_Employee
                                {
                                    Id = reader.GetInt32("Id"),
                                    Name = reader.GetString("Name"),
                                    DepartmentId = reader.GetInt32("DepartmentId"),
                                    Salary = reader.GetDecimal("Salary"),
                                    DateOfJoining = reader.IsDBNull("DateOfJoining") ? null : reader.GetDateTime("DateOfJoining")
                                };

                                return new DTO_Response<DTO_Employee>
                                {
                                    Status = "Success",
                                    StatusCode = 200,
                                    Message = "Employee retrieved successfully",
                                    Data = employee
                                };
                            }
                        }
                    }
                }

                return new DTO_Response<DTO_Employee>
                {
                    Status = "Error",
                    StatusCode = 404,
                    Message = "Employee not found"
                };
            }
            catch (Exception ex)
            {
                return new DTO_Response<DTO_Employee>
                {
                    Status = "Error",
                    StatusCode = 500,
                    Message = $"An error occurred: {ex.Message}",
                };
            }
        }

        public async Task<DTO_Response<DTO_Employee>> AddEmployeeAsync(DTO_Employee DTO_Employee)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    using (var command = new SqlCommand("sp_AddEmployee", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Name", DTO_Employee.Name);
                        command.Parameters.AddWithValue("@DepartmentId", DTO_Employee.DepartmentId);
                        command.Parameters.AddWithValue("@Salary", DTO_Employee.Salary);
                        command.Parameters.AddWithValue("@DateOfJoining", DTO_Employee.DateOfJoining ?? (object)DBNull.Value);

                        await connection.OpenAsync();
                        var result = await command.ExecuteScalarAsync();

                        if (result != null && int.TryParse(result.ToString(), out var newId))
                        {
                            DTO_Employee.Id = newId;

                            return new DTO_Response<DTO_Employee>
                            {
                                Status = "Success",
                                StatusCode = 201,
                                Message = "Employee added successfully",
                                Data = DTO_Employee
                            };
                        }
                    }
                }

                return new DTO_Response<DTO_Employee>
                {
                    Status = "Error",
                    StatusCode = 500,
                    Message = "Failed to add employee"
                };
            }
            catch (Exception ex)
            {
                return new DTO_Response<DTO_Employee>
                {
                    Status = "Error",
                    StatusCode = 500,
                    Message = $"An error occurred: {ex.Message}"
                };
            }
        }

        public async Task<DTO_Response<DTO_Employee>> UpdateEmployeeAsync(DTO_Employee DTO_Employee)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    using (var command = new SqlCommand("sp_UpdateEmployee", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Id", DTO_Employee.Id);
                        command.Parameters.AddWithValue("@Name", DTO_Employee.Name);
                        command.Parameters.AddWithValue("@DepartmentId", DTO_Employee.DepartmentId);
                        command.Parameters.AddWithValue("@Salary", DTO_Employee.Salary);
                        command.Parameters.AddWithValue("@DateOfJoining", DTO_Employee.DateOfJoining ?? (object)DBNull.Value);

                        await connection.OpenAsync();
                        var rowsAffected = await command.ExecuteNonQueryAsync();

                        if (rowsAffected > 0)
                        {
                            return new DTO_Response<DTO_Employee>
                            {
                                Status = "Success",
                                StatusCode = 200,
                                Message = "Employee updated successfully",
                                Data = DTO_Employee
                            };
                        }
                    }
                }

                return new DTO_Response<DTO_Employee>
                {
                    Status = "Error",
                    StatusCode = 404,
                    Message = "Employee not found"
                };
            }
            catch (Exception ex)
            {
                return new DTO_Response<DTO_Employee>
                {
                    Status = "Error",
                    StatusCode = 500,
                    Message = $"An error occurred: {ex.Message}"
                };
            }
        }

        public async Task<DTO_Response<string>> DeleteEmployeeAsync(int id)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    using (var command = new SqlCommand("sp_DeleteEmployee", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Id", id);

                        await connection.OpenAsync();
                        var rowsAffected = await command.ExecuteNonQueryAsync();

                        if (rowsAffected > 0)
                        {
                            return new DTO_Response<string>
                            {
                                Status = "Success",
                                StatusCode = 200,
                                Message = "Employee deleted successfully",
                                Data = "Employee deleted"
                            };
                        }
                    }
                }

                return new DTO_Response<string>
                {
                    Status = "Error",
                    StatusCode = 404,
                    Message = "Employee not found"
                };
            }
            catch (Exception ex)
            {
                return new DTO_Response<string>
                {
                    Status = "Error",
                    StatusCode = 500,
                    Message = $"An error occurred: {ex.Message}"
                };
            }
        }
        public async Task<DTO_Response<IEnumerable<DTO_Department>>> GetAllDepartmentsAsync()
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    using (var command = new SqlCommand("sp_GetAllDepartments", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        await connection.OpenAsync();
                        var reader = await command.ExecuteReaderAsync();

                        var departments = new List<DTO_Department>();
                        while (await reader.ReadAsync())
                        {
                            departments.Add(new DTO_Department
                            {
                                Id = reader.GetInt32("Id"),
                                Name = reader.GetString("Name")
                            });
                        }

                        return new DTO_Response<IEnumerable<DTO_Department>>
                        {
                            Status = "Success",
                            StatusCode = 200,
                            Message = "Departments retrieved successfully.",
                            Data = departments
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                return new DTO_Response<IEnumerable<DTO_Department>>
                {
                    Status = "Error",
                    StatusCode = 500,
                    Message = $"An error occurred: {ex.Message}"
                };
            }
        }

        public async Task<DTO_Response<DTO_Department>> GetDepartmentByIdAsync(int id)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    using (var command = new SqlCommand("sp_GetDepartmentById", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Id", id);

                        await connection.OpenAsync();
                        var reader = await command.ExecuteReaderAsync();

                        if (await reader.ReadAsync())
                        {
                            return new DTO_Response<DTO_Department>
                            {
                                Status = "Success",
                                StatusCode = 200,
                                Message = "Department retrieved successfully.",
                                Data = new DTO_Department
                                {
                                    Id = reader.GetInt32("Id"),
                                    Name = reader.GetString("Name")
                                }
                            };
                        }
                    }
                }

                return new DTO_Response<DTO_Department>
                {
                    Status = "Error",
                    StatusCode = 404,
                    Message = "Department not found."
                };
            }
            catch (Exception ex)
            {
                return new DTO_Response<DTO_Department>
                {
                    Status = "Error",
                    StatusCode = 500,
                    Message = $"An error occurred: {ex.Message}"
                };
            }
        }

        public async Task<DTO_Response<DTO_Department>> GetEmployeeDepartmentAsync(int employeeId)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    using (var command = new SqlCommand("sp_GetEmployeeDepartment", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@EmployeeId", employeeId);

                        await connection.OpenAsync();
                        var reader = await command.ExecuteReaderAsync();

                        if (await reader.ReadAsync())
                        {
                            return new DTO_Response<DTO_Department>
                            {
                                Status = "Success",
                                StatusCode = 200,
                                Message = "Employee department retrieved successfully.",
                                Data = new DTO_Department
                                {
                                    Id = reader.GetInt32("Id"),
                                    Name = reader.GetString("Name")
                                }
                            };
                        }
                    }
                }

                return new DTO_Response<DTO_Department>
                {
                    Status = "Error",
                    StatusCode = 404,
                    Message = "Employee department not found."
                };
            }
            catch (Exception ex)
            {
                return new DTO_Response<DTO_Department>
                {
                    Status = "Error",
                    StatusCode = 500,
                    Message = $"An error occurred: {ex.Message}"
                };
            }
        }

        public async Task<DTO_Response<DTO_Department>> AddDepartmentAsync(DTO_Department departmentDto)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    using (var command = new SqlCommand("sp_AddDepartment", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Name", departmentDto.Name);

                        await connection.OpenAsync();
                        var rowsAffected = await command.ExecuteNonQueryAsync();

                        if (rowsAffected > 0)
                        {
                            return new DTO_Response<DTO_Department>
                            {
                                Status = "Success",
                                StatusCode = 201,
                                Message = "Department added successfully.",
                                Data = departmentDto
                            };
                        }
                    }
                }

                return new DTO_Response<DTO_Department>
                {
                    Status = "Error",
                    StatusCode = 400,
                    Message = "Failed to add department."
                };
            }
            catch (Exception ex)
            {
                return new DTO_Response<DTO_Department>
                {
                    Status = "Error",
                    StatusCode = 500,
                    Message = $"An error occurred: {ex.Message}"
                };
            }
        }

        public async Task<DTO_Response<DTO_Department>> UpdateDepartmentAsync(DTO_Department departmentDto)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    using (var command = new SqlCommand("sp_UpdateDepartment", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Id", departmentDto.Id);
                        command.Parameters.AddWithValue("@Name", departmentDto.Name);

                        await connection.OpenAsync();
                        var rowsAffected = await command.ExecuteNonQueryAsync();

                        if (rowsAffected > 0)
                        {
                            return new DTO_Response<DTO_Department>
                            {
                                Status = "Success",
                                StatusCode = 200,
                                Message = "Department updated successfully.",
                                Data = departmentDto
                            };
                        }
                    }
                }

                return new DTO_Response<DTO_Department>
                {
                    Status = "Error",
                    StatusCode = 404,
                    Message = "Department not found."
                };
            }
            catch (Exception ex)
            {
                return new DTO_Response<DTO_Department>
                {
                    Status = "Error",
                    StatusCode = 500,
                    Message = $"An error occurred: {ex.Message}"
                };
            }
        }

        public async Task<DTO_Response<string>> DeleteDepartmentAsync(int id)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    using (var command = new SqlCommand("sp_DeleteDepartment", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Id", id);

                        await connection.OpenAsync();
                        var rowsAffected = await command.ExecuteNonQueryAsync();

                        if (rowsAffected > 0)
                        {
                            return new DTO_Response<string>
                            {
                                Status = "Success",
                                StatusCode = 200,
                                Message = "Department deleted successfully.",
                                Data = "Department deleted"
                            };
                        }
                    }
                }

                return new DTO_Response<string>
                {
                    Status = "Error",
                    StatusCode = 404,
                    Message = "Department not found."
                };
            }
            catch (Exception ex)
            {
                return new DTO_Response<string>
                {
                    Status = "Error",
                    StatusCode = 500,
                    Message = $"An error occurred: {ex.Message}"
                };
            }
        }
    }
}