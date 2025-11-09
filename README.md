# LuciferCore

[![NuGet](https://img.shields.io/nuget/v/LuciferCore.svg)](https://www.nuget.org/packages/LuciferCore)
[![NuGet CLI](https://img.shields.io/nuget/v/Lucifer.CLI.svg)](https://www.nuget.org/packages/Lucifer.CLI)

LuciferCore is a powerful .NET library designed for building high-performance servers, handling sessions, and more. It provides essential tools for developers working with .NET ecosystems, including a CLI for streamlined project setup and management.

## Installation

### Installing the LuciferCore Package
To add the latest version of the LuciferCore library to your project manually:

```bash
dotnet add package LuciferCore
```

For a specific version (e.g., for server fine-tuning):

```bash
dotnet add package LuciferCore --version 1.0.9
```

After installation, run `dotnet restore` to ensure all dependencies are loaded properly.

### Installing the Lucifer.CLI Tool
The CLI tool simplifies project initialization and management. Install it globally:

```bash
dotnet tool install --global Lucifer.CLI
```

For a specific version:

```bash
dotnet tool install --global Lucifer.CLI --version 1.0.8.3
```

This makes CLI commands more convenient to use from any directory.

## Usage

Once the CLI is installed, you can use it to bootstrap and manage your LuciferCore projects.

### Initialize a Project
Run the following to create the necessary files and directories:

```bash
lucifer init
```

**Note:** If libraries haven't been loaded yet, run `dotnet restore` after initialization.

### Create Templates
Generate boilerplate files using the `create` command, followed by the template name:

```bash
lucifer create <template-name>
```

Examples of commonly used templates:
- `lucifer create Handler` – Creates a handler template.
- `lucifer create HttpsServer` – Sets up a server configuration.
- `lucifer create HttpsSession` – Initializes session management files.

Explore more templates based on your needs.

### Update Versions
Keep your CLI and Core up to date:

```bash
lucifer update
```

To update all components (CLI and Core) to the latest versions:

```bash
lucifer update -a
# or
lucifer update --all
```

### Clear Resources
Safely remove generated files or reset your setup:

```bash
lucifer clear
```

To clear everything (ensure no important data is present before running):

```bash
lucifer clear -a
# or
lucifer clear --all
```

### Quick Example: Running a Console Application
After completing the setup steps above, you can quickly run a basic console application using LuciferCore. Add the following to your `Program.cs`:

```csharp
using LuciferCore.Main;
using static LuciferCore.Core.Simulation;

GetModel<HostServer>().Run();
```

This will launch a console-based server/host with built-in command handling.

To extend functionality, define console commands using the `[ConsoleCommand]` attribute in your partial `HostServer` class (e.g., under `LuciferCore.Main`). These commands can be entered directly in the running console for interactive management. Examples of predefined or commonly used commands include:

- `[ConsoleCommand("/start host", "Start host")]`.
- `[ConsoleCommand("/stop host", "Stop host")]`.
- `[ConsoleCommand("/restart host", "Restart host")]`.
- `[ConsoleCommand("/start service", "Start services")]`.
- `[ConsoleCommand("/stop service", "Stop services")]`.
- `[ConsoleCommand("/restart service", "Restart services")]`.

Run your application with `dotnet run` to interact with these commands (e.g., type `/start host` in the console).

## NuGet Package
Find the latest releases and documentation on [NuGet.org](https://www.nuget.org/packages/LuciferCore).

## Contributing
We welcome contributions! Please fork the repository, make your changes, and submit a pull request. For collaboration opportunities, contact the author directly.

## Acknowledgments
Thank you to everyone who has contributed to LuciferCore and made this project possible. Your support means the world!

## Contact
- **GitHub:** [thuangf45](https://github.com/thuangf45)
- **Email:** [kingnemacc@gmail.com](mailto:kingnemacc@gmail.com)
- **NuGet:** [LuciferCore on NuGet](https://www.nuget.org/packages/LuciferCore)

---
