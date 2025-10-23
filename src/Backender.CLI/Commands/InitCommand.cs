using System;
using System.IO;
using System.Threading.Tasks;

namespace Backender.Cli.Commands
{
    public static class InitCommand
    {
        private const string DefaultBlueprintFileName = "blueprint.xml";
        private const string BlueprintTemplate = @"<?xml version=""1.0"" encoding=""utf-8""?>
<Blueprint SavePath=""./output"" ValidationControl=""DataAnnotation"">
    
    <!-- 
      Solution Configuration:
      - Name: The name of the solution and the default project prefix.
      - Namespace: The root namespace for all generated code.
      - UseDefaultStructure: If true, creates a standard Core/Data/Services project structure.
                             Set to false to define your own projects below.
    -->
    <Solution Name=""MyAwesomeProject"" Namespace=""MyAwesomeProject"" UseDefaultStructure=""true"">
        <!-- 
          You can define a custom project structure here if UseDefaultStructure is false.
          Example:
          <Projects>
            <Project Name=""{Solution.Name}.Domain"" Role=""Core"" Path=""src"" Sdk=""Microsoft.NET.Sdk""/>
          </Projects>
        -->
    </Solution>

    <Domains>
        <!-- 
          Define Enums here.
          They can be used as a Col Type in your entities.
        -->
        <Enum Name=""TaskStatus"">
            <EnumValue Name=""Todo"" Value=""0"" />
            <EnumValue Name=""InProgress"" Value=""1"" />
            <EnumValue Name=""Done"" Value=""2"" />
        </Enum>

        <!-- 
          Define Entities (your domain models) here.
          - Name: The class name of the entity.
          - Category: (Optional) A sub-folder/namespace for organization.
        -->
        <Entity Name=""User"" Category=""Identity"">
            <Col Name=""Username"" Type=""string"" Options=""-r -l(3,50)"" />
            <Col Name=""Email"" Type=""string"" Options=""-r -e"" />
            <Col Name=""PasswordHash"" Type=""string"" Options=""-r"" />
        </Entity>

        <Entity Name=""TaskItem"" Category=""Tasks"">
            <Col Name=""Title"" Type=""string"" Options=""-r -l(255)"" />
            <Col Name=""Description"" Type=""string"" Options="""" />
            <Col Name=""Status"" Type=""TaskStatus"" Options="""" />
        </Entity>

        <!-- 
          Define Relationships between entities.
          - Type: O2M (One-to-Many), M2M (Many-to-Many), O2O (One-to-One).
        -->
        <RelationShip Entity1=""User"" Entity2=""TaskItem"" Type=""O2M"" />

    </Domains>
</Blueprint>";

        public static async Task ExecuteAsync(string fileName)
        {
            var filePath = string.IsNullOrEmpty(fileName) ? DefaultBlueprintFileName : fileName;

            if (File.Exists(filePath))
            {
                ConsoleHelper.WriteWarning($"The file '{filePath}' already exists. Overwrite? (y/n)");
                var response = Console.ReadLine()?.ToLower();
                if (response != "y")
                {
                    ConsoleHelper.WriteMessage("Operation cancelled.");
                    return;
                }
            }

            try
            {
                await File.WriteAllTextAsync(filePath, BlueprintTemplate);
                ConsoleHelper.WriteSuccess($"Successfully created blueprint template at: {Path.GetFullPath(filePath)}");
            }
            catch (Exception ex)
            {
                ConsoleHelper.WriteError($"Failed to create blueprint file: {ex.Message}");
            }
        }
    }
}