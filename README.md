# Project Name

CodePulse - Explore Coding Blogs and gain some technical knowledge facts.

## Setup Environment

### Prerequisites

Make sure you have the following installed:

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) (with .NET Desktop Development workload)
- [Git](https://git-scm.com/)

### Clone the Repository
```bash
git clone https://github.com/mihir-pithva/CodePulse-Backend.git
```
### Visual Studio Setup
1. Open Visual Studio 2022.
2. Go to File > Clone Repository.
3. Enter the repository URL and local path.
4. Click Clone.
5. Visual Studio should automatically detect the project type and open it. If not, you can open the project by navigating to the cloned directory.

### Configure Database Connection:
1. Locate the appsettings.json file in the project. This file typically resides in the root directory of the project or within a Config folder.
2. Modify the ConnectionStrings section to point to your database.
3. open the Package Manager Console (Tools > NuGet Package Manager > Package Manager Console).
4. Ensure the default project selected in the console is your project.
5. Run the following commands:
```bash
Add-Migration InitialCreate
Update-Database
```
- Replace InitialCreate with an appropriate migration name.

### Build and Run the Project:
1. Press F5 or click on the Start button in Visual Studio to build and run the project.
2. Visual Studio will build the solution and launch the web API. It will open your default browser with the URL of your API.

### Visual Studio Code Setup
1. Open Visual Studio Code.
2. Install the following extensions (if not already installed):
       C# for Visual Studio Code (ms-dotnettools.csharp)
3. Open the project folder in Visual Studio Code.
4. Open a terminal in Visual Studio Code (Ctrl+`).
5. Restore dependencies and build the project:
```bash
dotnet restore
dotnet build
```
6. Ensure Database Setup:
       - Look for a configuration file that typically contains database connection settings. Commonly, this is `appsettings.json` under the project's root directory or in a Config folder.
       - Modify the ConnectionStrings section to point to your database.
       - Open a terminal or command prompt.
       - Navigate to the project directory containing the .csproj file.
       - Run the following commands:
       ```bash
       dotnet ef migrations add InitialCreate
       dotnet ef database update
       ```
   -Replace InitialCreate with an appropriate migration name.
8. Run the project:
   - Use the following commands to build and run:
```bash
dotnet build
dotnet run
```
### Configuration
AppSettings: Update appsettings.json in the project root with your preferred settings.(Include your database connection string and JWT settings)

