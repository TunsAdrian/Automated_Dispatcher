# Automated Dispatcher
 
This is a collaborative project made by Daniel Ciucur, Andrei Iovescu, Adrian Tuns and Andrei Opra.

The scope of the application is to automatically dispatch tasks created by one or several managers to a group of employees.

## Getting Started

### SQL Server

The application uses SQL Server for database management. Microsoft SQL Server Management Studio must be installed and then a new database should be created. The scripts from [Scripts_SQL_Server.txt](Scripts_SQL_Server.txt) file must be run in order, from 0 to 6 and then at least one manager and the three statuses have to be created.

### ASP.NET Core Visual Studio

The following NuGet Packages must be installed: Microsoft.EntityFrameworkCore.SqlServer, Microsoft.EntityFrameworkCore.SqlServer.Design, Microsoft.EntityFrameworkCore.Tools.

From NuGet Packet Manager Console the following command must be called, after the SQL Server was properly configured:
`Scaffold-DbContext "Server=Server-Name;Database=webapp;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Data`

The connection string must also be updated  in the project, in webappContext.cs and in appsettings.json. The connection string is:
`"Server=Server-Name;Database=webapp;Trusted_Connection=True;"`

