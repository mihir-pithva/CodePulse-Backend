# Project Name

CodePulse - Explore Coding Blogs and gain some technical knowledge facts.

## Setup Environment

### Prerequisites

Make sure you have the following installed:

- [.NET 6 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) (with .NET Desktop Development workload)
- [Git](https://git-scm.com/)

### Clone the Repository

```bash
git clone https://github.com/mihir-pithva/CodePulse-Backend.git
cd CodePulse
```
### Visual Studio Setup
1. Open Visual Studio 2022.
2. Select File > Open > Project/Solution.
3. Navigate to the cloned repository folder and select the .csproj file.
4. Visual Studio will restore the NuGet packages automatically.
5. Press F5 to build and run the project.

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
6. Run the project:
```bash
dotnet run
```
### Configuration
AppSettings: Update appsettings.json in the project root with your preferred settings.(Include your database connection string and JWT settings)

