
<p align="center" style="margin-top: 10px;">
  <img width="100"src="./docs/Logo.png"/>
</p>

### Backender
Backender is an open source project in C# language that uses a Blueprint file to create the backend of your site in the repository pattern (yet!).

## Nuget Packages

| Package | NuGet Stable |  Downloads |
| ------- | -------- | ------- |
| [Backender.CLI](https://www.nuget.org/packages/Backender.CLI/) | [![Backender.CLI](https://img.shields.io/nuget/v/Backender.CLI.svg)](https://www.nuget.org/packages/Backender.CLI/)  | [![Backender.CLI](https://img.shields.io/nuget/dt/Backender.CLI.svg)](https://www.nuget.org/packages/Backender.CLI/) |

### Getting Started

1. Install from .NET Core Global Tool  

  ``` shell
    dotnet tool install --global Backender.CLI --version 2.0.0
  ```

2. Create or edit Blueprint file
3. the command line executes the Backender command.
    - Backender
    - wait for prompt to enter the configuration file path
    - carriage return execution command
4. wait for the end of the task execution.
5. Edit the created projects as you need.
6. Enjoy the time saved!

### Sample of Blueprint File
``` xml
<Blueprint ValidationControl="FluentValidation">
<Solution Name="GoBlog" Namespace="GoBlog"/>
<Domains>
<Enum Name="CommentStatus">
<EnumValue Name="Pending" Value="1"/>
<EnumValue Name="Accepted" Value="2"/>
<EnumValue Name="Failed" Value="3"/>
</Enum>
<Entity Name="Post">
<Col Name="Title" Type="string"/>
<Col Name="Author" Type="string"/>
<Col Name="Content" Type="string"/>
</Entity>
<Entity Name="Comment">
<Col Name="Name" Type="string"/>
<Col Name="CommentStatus" Type="CommentStatus"/>
<Col Name="Email" Type="string" Options="email"/>
<Col Name="Content" Type="string"/>
</Entity>
<Entity Name="Category">
<Col Name="Title" Type="string"/>
<Col Name="Description" Type="string" Options="DisplayName(توضیحات) required"/>
</Entity>
<RelationShip Entity1="Post" Entity2="Comment" Type="O2M"/>
<RelationShip Entity1="Category" Entity2="Post" Type="O2M"/>
<GlobalOption Id="ShortString" EntityCols="root.Title, Post.All(), Post.Comment" Options="Length(0,250) required"/>
</Domains>
</Blueprint>
```
### More Informations
https://medium.com/@mahdihoseinpoor/introducing-backender-2-enhanced-speed-blueprints-validations-and-more-b91d64c59741

https://medium.com/@mahdihoseinpoor/blueprint-the-only-thing-that-backender-needs-61128924aa5c

### Donate
for Iranian People: https://idpay.ir/thisismahdihoseinpoor

    
