# Employee Management API

A simple .NET Web API for managing employees and departments in an organization. The API supports CRUD operations for employees and departments and uses SQL Server stored procedures for database interactions. Accessibility is handled through Swagger.

## Features

- Manage Employees: Add, update, retrieve, and delete employee records.
- Manage Departments: Add, update, retrieve, and delete department records.
- Retrieve employee-specific department details.

## Setup Instructions

```bash
# Prerequisites
1. Install .NET 8.0 SDK
2. Install SQL Server
3. Install Visual Studio

# Installation Steps
1. Clone the repository:
   git clone https://github.com/your-username/employee-management-api.git
   cd employee-management-api

2. Open the solution in Visual Studio.

3. Create the database:
   # In SQL Server, create a database named `Employee_db`.
   # Open the Package Manager Console in Visual Studio and run:
   Update-Database

4. Set up stored procedures:
   # Open the `Employee_db` database in SQL Server.
   # Copy the contents of `/StoredProcedures/Script.sql` and execute it.
   USE Employee_db
   -- Paste the script contents here

# Running the Application
1. Start the Server from Visual Studio.
2. Access the Swagger UI at `https://localhost:<port>/swagger` (port depends on your local configuration).
```

## Notes
- The API is entirely Swagger-based for accessibility. Use Swagger to explore available endpoints and test functionality.
- Ensure the database connection string in the appsettings.json file matches your local SQL Server setup.
