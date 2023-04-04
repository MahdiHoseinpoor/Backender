
# Backender

Backender is an open source project in C# language that uses a config file to create the backend of your site in the repository pattern (yet!).

## Nuget Packages

| Package | NuGet Stable |  Downloads |
| ------- | -------- | ------- |
| [Backender.CLI](https://www.nuget.org/packages/SmartCode.CLI/) | [![Backender.CLI](https://img.shields.io/nuget/v/SmartCode.CLI.svg)](https://www.nuget.org/packages/SmartCode.CLI/)  | [![Backender.CLI](https://img.shields.io/nuget/dt/SmartCode.CLI.svg)](https://www.nuget.org/packages/SmartCode.CLI/) |

### Demo


### Getting Started

1. Install from .NET Core Global Tool  

  ``` shell
    dotnet tool install --global Backender.CLI
  ```

2. Create or edit config file (default: Backender.yml)
3. the command line executes the Backender command.
    - Backender
    - wait for prompt to enter the configuration file path (optional: Backender.yml file in the default program root directory)
    - carriage return execution command
4. wait for the end of the task execution.
5. View output directory results
6. Edit the created projects as you need.
7. Enjoy the time saved!
