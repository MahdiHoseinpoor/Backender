
<p align="center" style="margin-top: 10px;">
  <img width="100"src="./docs/Logo.png"/>
</p>
# Backender

Backender is an open source project in C# language that uses a config file to create the backend of your site in the repository pattern (yet!).

### Demo

![Backender](./docs/Demo.gif)

### Getting Started

1. Install from .NET Core Global Tool  

  ``` shell
    dotnet tool install --global Backender.CLI
  ```

2. Create or edit config file
3. the command line executes the Backender command.
    - Backender
    - wait for prompt to enter the configuration file path (optional: Backender.yml file in the default program root directory)
    - carriage return execution command
4. wait for the end of the task execution.
5. View output directory results
6. Edit the created projects as you need.
7. Enjoy the time saved!

### Sample of config file in yaml
``` yaml
SolutionName: Shopping
SoltionNameSpace: ShoppingCore
SavePath: "C://Sources/"
Domains:
  Entites:
  - EntityName: Customer
    Cols:
    - ColName: FirstName
      ColType: string
      Options: "-r"
    - ColName: LastName
      ColType: string
    - ColName: Email
      ColType: string
      Options: "-g"
  - EntityName: Product
    Cols:
    - ColName: ProductName
      ColType: string
      Options: "-r"
    - ColName: Price
      ColType: string
      Options: "-r"
    - ColName: Description
      ColType: string
  - EntityName: Order
    Cols:
    - ColName: OrderDate
      ColType: DateTime
      Options: "-r"
    - ColName: OrderStatus
      ColType: OrderStatus
      Options: "-r"
  RealationShips:
  - Entity1: Customer
    Entity2: Order
    RealationShipType: O2M
  - Entity1: Product
    Entity2: Order
    RealationShipType: M2M
  Enums:
  - EnumName: OrderStatus
    EnumValues:
    - Name: InOrder
      Value: 1
    - Name: Closed
      Value: 2
    - Name: Recived
      Value: 3
  - EnumName: ProductType
    EnumValues:
    - Name: InJib
      Value: 0
    - Name: NotInJib
      Value: 2
```
1. The options field in the entity Cols can receive multiple options with spaces between them.
    - -r means required, and gives it the required property
    - -g means get, a method is defined in the repository that returns this object based on this field
   
2. The Realations can be
    - O2O : ONE TO ONE
    - O2M : ONE TO MANY
    - M2M : MANY TO MANY

### More Informations
https://medium.com/@hoseinpoor2006/why-you-should-use-backender-for-your-next-asp-net-core-project-de026ec2b898
    
