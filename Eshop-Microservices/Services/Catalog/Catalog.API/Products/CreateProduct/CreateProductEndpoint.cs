namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductRequestEndpoint(string Name, List<string> Category, string Description, string ImageFile, decimal Price);
    public record CreateProductResponseEndpoint(Guid Id);
    public class CreateProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/products", async (CreateProductRequestEndpoint request, ISender sender) =>
            {
                var command = request.Adapt<CreateProductRequest>();

                var result = await sender.Send(command);

                var response = result.Adapt<CreateProductResponseEndpoint>();

                return Results.Created($"/products/{response.Id}", response);
            })
                .WithName("CreateProduct")
                .Produces<CreateProductResponseEndpoint>(StatusCodes.Status201Created)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Create a new product")
                .WithDescription("Create a new product in the catalog");
        }
    }
}
