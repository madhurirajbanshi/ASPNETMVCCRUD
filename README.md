#Employee Management System with Department Integration

#Overview
This project is an Employee Management System built using ASP.NET Core MVC and Entity Framework Core. The application allows users to manage employee records, departments, and department summaries, providing CRUD functionality and robust interactions between entities. The system makes use of Foreign Key relationships to link Employees with their respective Departments, ensuring referential integrity between entities.

Key Features
Departments Management: Add, update, delete, and activate/deactivate departments. Each department is linked to employees.
Employees Management: Add, update, delete employee details, including name, email, salary, department assignment, and profile image.
Department Summary: View department summaries, including employee names, total count, average salary, and department status.
Foreign Key Relationships: Employees are linked to departments using Foreign Keys, ensuring that each employee is associated with a specific department.
File Upload: Profile images for employees are stored and displayed, using byte arrays to manage image data in the database.

Technologies Used
ASP.NET Core MVC: To build the web application with the MVC pattern.
Entity Framework Core: To handle database operations with the ORM framework.
SQL Server: The database engine for storing employees, departments, and their relationships.
HTML, CSS, JavaScript: For the front-end user interface.

Project Structure
Models
Employee:
-Foreign Key to the Department model.
-Includes fields such as Name, Email, Salary, DateofBirth, ImageData, and ImageFileName.

Department:
-Includes fields such as Name, Status, and Employees.
-One-to-many relationship with the Employee model (One department can have many employees).

DepartmentSummary:
-Displays aggregated data of departments, such as employee count, average salary, and department status.

Foreign Key Relationships
The Employee model has a Foreign Key relationship with the Department model. Each employee belongs to one department, and the department can have multiple employees. The foreign key ensures referential integrity, i.e., an employee cannot exist without a valid department.

Controllers
DepartmentController: Manages department operations such as adding, updating, deleting, and toggling department status.
EmployeeController: Manages employee operations such as adding, updating, and deleting employees, as well as managing profile images.
DepartmentSummaryController: Provides a view of department summaries with aggregated data like employee count and average salary.

Views
The application contains views to handle both employee and department management tasks. Employees are displayed in a tabular format along with their departments, and the department management section includes forms to add or update department details.

Setup Instructions
Prerequisites
Ensure you have the following installed:
.NET SDK (version 6 or later)
SQL Server (or use LocalDB for development)
Visual Studio (or another IDE with ASP.NET Core support)

Clone the Repository
bash
Copy code
git clone https://github.com/yourusername/EmployeeManagementSystem.git
cd EmployeeManagementSystem
Restore Dependencies
Run the following command to restore the project's dependencies:

bash
Copy code
dotnet restore
Set Up the Database
Update Connection String: In the appsettings.json file, update the connection string to point to your SQL Server instance.

Apply Migrations: Run the following command to create the database schema.

bash
Copy code
dotnet ef database update
Run the Application
To run the application locally, use the following command:

bash
Copy code
dotnet run
Navigate to https://localhost:5001 in your browser to interact with the system.

Example Usage
Add a Department: Navigate to the Department section and add a new department with a name and status.
Add Employees: Assign employees to departments with their details, including salary, email, and profile image.
View Department Summary: The Department Summary view shows an aggregated report for active departments, displaying the total number of employees, average salary, and more.
