namespace Catalog.API.Products.GetProducts.GetProductsEndpoint.cs;
// public record GetProductsRequest();
public record GetProductsResponse(IEnumerable<Product> Products);


public class GetProductsEndpoint : CarterModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products", async (ISender sender, CancellationToken cancellationToken) =>
        {
            var result = await sender.Send(new GetProductsQuery(), cancellationToken);
            var response = result.Adapt<GetProductsResponse>();
            return Results.Ok(response);
        })
            .WithName("GetProducts")
            .Produces<GetProductsResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized)
            .Produces(StatusCodes.Status403Forbidden)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError)
            .WithSummary("Get all products")
            .WithDescription("Get all products");
    }
}