#SQL Server

The application uses SQL Server for database management. Microsoft SQL Server Management Studio must be installed and then a new database should be created. The scripts from Scripts_SQL_Server.txt file must be run in order, from 0 to 6 and then at least one manager and the three statuses should be created.

#ASP.NET Core

From ... the following command must be called: 
Scaffold-DbContext "Server=Server-Name;Database=webapp;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Data
