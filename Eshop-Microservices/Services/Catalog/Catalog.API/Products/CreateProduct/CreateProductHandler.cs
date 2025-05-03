
using BuildingBlocks.CQRS;
using Catalog.API.Models;
using MediatR;

namespace Catalog.API.Products.CreateProduct
{
    // Records
    public record CreateProductRequest(string Name, List<string> Category, string Description, string ImageFile, decimal Price)
        : ICommand<CreateProductResponse>;

    public record CreateProductResponse(Guid Id);

    // Handler
    public class CreateProductHandler : ICommandHandler<CreateProductRequest, CreateProductResponse>
    {
        public async Task<CreateProductResponse> Handle(CreateProductRequest request, CancellationToken cancellationToken)
        {
            // Create product entity from command object
            var product = new Product
            {
                Name = request.Name,
                Category = request.Category,
                Description = request.Description,
                ImageFile = request.ImageFile,
                Price = request.Price
            };
            // Save to databse
            // Return create product result
            return new CreateProductResponse(Guid.NewGuid());
        }
    }
}
