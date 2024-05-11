# C# Dotnet Environment

## Install:

[Download .NET SDK 8.0](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)

- Run `dotnet --version` to verify that the .NET SDK has been installed correctly.

Then you can **run** my pre-set up dotnet environment using `dotnet run` command.

Or you can **run** the bash scripts in the root directory to quickly run the AVL tree environment.

## To create a dotnet environment/program

1. `dotnet new console -n ProjectName`
2. `cd ProjectName`
3. `dotnet run`

- `Program.cs`: The main C# file as the entry point of the application.
- `.csproj`: The project file with build settings and dependencies. csproj = C# project.
- `bin/`: The output folder for compiled files. bin stands for binary.
- `obj/`: Contains temporary build files. obj stands for object.
- `dotnet run`: The command to compile and run the project.
- `dotnet build`: The command to compile the project.