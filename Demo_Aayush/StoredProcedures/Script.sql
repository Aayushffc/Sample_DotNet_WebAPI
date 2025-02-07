GO
/**************************************************************************************
PROCEDURE NAME  : sp_GetAllEmployees
CREATED BY      : Aayush
CREATED DATE    : ---
DESCRIPTION     : Retrieves all employee records from the Employees table.
**************************************************************************************/
CREATE OR ALTER PROCEDURE [dbo].[sp_GetAllEmployees]
AS 
BEGIN TRY
    SELECT [Id], [Name], [DepartmentId], [Salary], [DateOfJoining]
    FROM [dbo].[Employees]
END TRY
BEGIN CATCH
    SELECT ERROR_MESSAGE() AS [Error]
END CATCH
GO


/**************************************************************************************
PROCEDURE NAME  : sp_GetEmployeeById
CREATED BY      : Aayushg
CREATED DATE    : ---
DESCRIPTION     : Retrieves an employee record by its Id.
**************************************************************************************/
CREATE OR ALTER PROCEDURE [dbo].[sp_GetEmployeeById]
    @Id INT
AS
BEGIN TRY
    SELECT [Id], [Name], [DepartmentId], [Salary], [DateOfJoining]
    FROM [dbo].[Employees]
    WHERE [Id] = @Id
END TRY
BEGIN CATCH
    SELECT ERROR_MESSAGE() AS [Error]
END CATCH
GO


/**************************************************************************************
PROCEDURE NAME  : sp_AddEmployee
CREATED BY      : Aayush
CREATED DATE    : ---
DESCRIPTION     : Adds a new employee record to the Employees table.
**************************************************************************************/
CREATE OR ALTER PROCEDURE [dbo].[sp_AddEmployee]
    @Name NVARCHAR(100),
    @DepartmentId INT,
    @Salary DECIMAL(18, 2),
    @DateOfJoining DATETIME
AS
BEGIN TRY
    INSERT INTO [dbo].[Employees] ([Name], [DepartmentId], [Salary], [DateOfJoining])
    VALUES (@Name, @DepartmentId, @Salary, @DateOfJoining)

    SELECT SCOPE_IDENTITY() AS [Id]
END TRY
BEGIN CATCH
    SELECT ERROR_MESSAGE() AS [Error]
END CATCH
GO


/**************************************************************************************
PROCEDURE NAME  : sp_UpdateEmployee
CREATED BY      : Aayush
CREATED DATE    : ---
DESCRIPTION     : Updates an existing employee record.
**************************************************************************************/
CREATE OR ALTER PROCEDURE [dbo].[sp_UpdateEmployee]
    @Id INT,
    @Name NVARCHAR(100),
    @DepartmentId INT,
    @Salary DECIMAL(18, 2),
    @DateOfJoining DATETIME
AS
BEGIN TRY
    UPDATE [dbo].[Employees]
    SET [Name] = @Name,
        [DepartmentId] = @DepartmentId,
        [Salary] = @Salary,
        [DateOfJoining] = @DateOfJoining
    WHERE [Id] = @Id
END TRY
BEGIN CATCH
    SELECT ERROR_MESSAGE() AS [Error]
END CATCH
GO
/**************************************************************************************
PROCEDURE NAME  : sp_DeleteEmployee
CREATED BY      : Aayush
CREATED DATE    : ---
DESCRIPTION     : Deletes an employee record by Id.
**************************************************************************************/
CREATE OR ALTER PROCEDURE [dbo].[sp_DeleteEmployee]
    @Id INT
AS
BEGIN TRY
    DELETE FROM [dbo].[Employees]
    WHERE [Id] = @Id
END TRY
BEGIN CATCH
    SELECT ERROR_MESSAGE() AS [Error]
END CATCH
GO
/**************************************************************************************
PROCEDURE NAME  : sp_GetAllDepartments
CREATED BY      : Aayush
CREATED DATE    : ---
DESCRIPTION     : Retrieves all department records from the Departments table.
**************************************************************************************/
CREATE OR ALTER PROCEDURE [dbo].[sp_GetAllDepartments]
AS
BEGIN TRY
    SELECT [Id], [Name]
    FROM [dbo].[Departments]
END TRY
BEGIN CATCH
    SELECT ERROR_MESSAGE() AS [Error]
END CATCH
GO
/**************************************************************************************
PROCEDURE NAME  : sp_GetDepartmentById
CREATED BY      : Aayush
CREATED DATE    : ---
DESCRIPTION     : Retrieves a department record by its Id.
**************************************************************************************/
CREATE OR ALTER PROCEDURE [dbo].[sp_GetDepartmentById]
    @Id INT
AS
BEGIN TRY
    SELECT [Id], [Name]
    FROM [dbo].[Departments]
    WHERE [Id] = @Id
END TRY
BEGIN CATCH
    SELECT ERROR_MESSAGE() AS [Error]
END CATCH
GO
/**************************************************************************************
PROCEDURE NAME  : sp_AddDepartment
CREATED BY      : Aayush
CREATED DATE    : ---
DESCRIPTION     : Adds a new department record to the Departments table.
**************************************************************************************/
CREATE OR ALTER PROCEDURE [dbo].[sp_AddDepartment]
    @Name NVARCHAR(100)
AS
BEGIN TRY
    INSERT INTO [dbo].[Departments] ([Name])
    VALUES (@Name)

    SELECT SCOPE_IDENTITY() AS [Id]
END TRY
BEGIN CATCH
    SELECT ERROR_MESSAGE() AS [Error]
END CATCH
GO
/**************************************************************************************
PROCEDURE NAME  : sp_UpdateDepartment
CREATED BY      : Aayush
CREATED DATE    : ---
DESCRIPTION     : Updates an existing department record.
**************************************************************************************/
CREATE OR ALTER PROCEDURE [dbo].[sp_UpdateDepartment]
    @Id INT,
    @Name NVARCHAR(100)
AS
BEGIN TRY
    UPDATE [dbo].[Departments]
    SET [Name] = @Name
    WHERE [Id] = @Id
END TRY
BEGIN CATCH
    SELECT ERROR_MESSAGE() AS [Error]
END CATCH
GO
/**************************************************************************************
PROCEDURE NAME  : sp_DeleteDepartment
CREATED BY      : Aayush
CREATED DATE    : ---
DESCRIPTION     : Deletes a department record by Id.
**************************************************************************************/
CREATE OR ALTER PROCEDURE [dbo].[sp_DeleteDepartment]
    @Id INT
AS
BEGIN TRY
    DELETE FROM [dbo].[Departments]
    WHERE [Id] = @Id
END TRY
BEGIN CATCH
    SELECT ERROR_MESSAGE() AS [Error]
END CATCH
GO
/**************************************************************************************
PROCEDURE NAME  : sp_GetEmployeeDepartment
CREATED BY      : Aayush
CREATED DATE    : ---
DESCRIPTION     : Retrieves the department for a specific employee by their EmployeeId.
**************************************************************************************/
CREATE OR ALTER PROCEDURE [dbo].[sp_GetEmployeeDepartment]
    @EmployeeId INT
AS
BEGIN TRY
    SELECT d.[Id] AS DepartmentId, d.[Name] AS DepartmentName
    FROM [dbo].[Departments] d
    INNER JOIN [dbo].[Employees] e ON e.DepartmentId = d.Id
    WHERE e.Id = @EmployeeId
END TRY
BEGIN CATCH
    SELECT ERROR_MESSAGE() AS [Error]
END CATCH
GO