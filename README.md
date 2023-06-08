
<p align="center" style="margin-top: 10px;">
  <img width="100"src="./docs/Logo.png"/>
</p>

### Backender
Backender is an open source project in C# language that uses a config file to create the backend of your site in the repository pattern (yet!).

## Nuget Packages

| Package | NuGet Stable |  Downloads |
| ------- | -------- | ------- |
| [Backender.CLI](https://www.nuget.org/packages/Backender.CLI/) | [![Backender.CLI](https://img.shields.io/nuget/v/Backender.CLI.svg)](https://www.nuget.org/packages/Backender.CLI/)  | [![Backender.CLI](https://img.shields.io/nuget/dt/Backender.CLI.svg)](https://www.nuget.org/packages/Backender.CLI/) |

### Getting Started

1. Install from .NET Core Global Tool  

  ``` shell
    dotnet tool install --global Backender.CLI --version 1.2.1
  ```

2. Create or edit config file
3. the command line executes the Backender command.
    - Backender
    - wait for prompt to enter the configuration file path
    - carriage return execution command
4. wait for the end of the task execution.
5. View output directory results
6. Edit the created projects as you need.
7. Enjoy the time saved!

### Sample of config file in yaml
``` yaml
SolutionName: GoBlog
SoltionNameSpace: GoBlog
SavePath: D:\Backender\Sources
Domains:
  Entites:
  - EntityName: Post
    EntityCategory: Catalog
    Cols:
    - ColName: Title
      ColType: string
      Options: "-r"
    - ColName: Author
      ColType: string
      Options: "-g"
    - ColName: Content
      ColType: string
  - EntityName: Comment
    EntityCategory: Catalog
    Cols:
    - ColName: Content
      ColType: string
      Options: "-r"
    - ColName: Name
      ColType: string
      Options: "-r"
    - ColName: CommentStatus
      ColType: CommentStatus
      Options: "-r"
    - ColName: Email
      ColType: string
  - EntityName: Category
    EntityCategory: Catalog
    Cols:
    - ColName: Title
      ColType: string
      Options: "-r"
    - ColName: Description
      ColType: string
      Options: "-r"
  relationShips:
  - Entity1: Post
    Entity2: Comment
    RelationShipType: O2M
  - Entity1: Category
    Entity2: Post
    RelationShipType: O2M
  Enums:
  - EnumName: CommentStatus
    EnumValues:
    - Name: Pending
      Value: 1
    - Name: Accepted
      Value: 2
    - Name: Failed
      Value: 3
```
1. The options field in the entity Cols can receive multiple options with spaces between them.
    - -r means required, and gives it the required property
    - -g means get, a method is defined in the repository that returns this object based on this field
   
2. The relations can be
    - O2O : ONE TO ONE
    - O2M : ONE TO MANY
    - M2M : MANY TO MANY
    
    
