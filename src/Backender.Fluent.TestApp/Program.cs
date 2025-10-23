using Backender.Translator.Handlers.ValidationControls;
using Backender.Fluent;

Console.WriteLine("Starting code generation with the new Fluent API...");

await backender.Define(
    solutionName: "SmartShop",
    rootNamespace: "SmartShop.Backend",
    savePath: "C:\\Users\\Mahdi Hoseinpoor\\Documents\\Backender")
.WithDomains(domains =>
{
    domains.Enum("OrderStatus")
        .HasValue("Pending", 0)
        .HasValue("Processing", 1)
        .HasValue("Shipped", 2)
        .HasValue("Delivered", 3)
        .HasValue("Cancelled", 4);

    domains.Entity("User", category: "Security")
        .Property<string>("Username").IsRequired().HasLength(3, 50)
        .Property<string>("Email").IsRequired().IsEmailAddress()
        .Property<string>("PasswordHash").IsRequired()
        .HasMany("Order"); 

    domains.Entity("Product")
        .Property<string>("Name").IsRequired().HasMaxLength(200)
        .Property<decimal>("Price").IsRequired()
        .Property<int>("StockQuantity")
        .HasMany("OrderItem");

    domains.Entity("Order")
        .Property<DateTime>("OrderDate")
        .Property("Status", "OrderStatus") 
        .HasOne("User")
        .HasMany("OrderItem");

    domains.Entity("OrderItem")
        .Property<int>("Quantity").IsRequired()
        .Property<decimal>("PriceAtTimeOfOrder").IsRequired()
        .HasOne("Order")
        .HasOne("Product");
})
.UseValidation(ValidationControl.FluentValidation)
.GenerateWithDefaultPipelineAsync(); 

Console.WriteLine("Code generation complete!");