namespace Catalog.API.Products.CreateProduct
{
    // Records
    public record CreateProductRequest(string Name, List<string> Category, string Description, string ImageFile, decimal Price)
        : ICommand<CreateProductResponse>;
    public record CreateProductResponse(Guid Id);

    public class CreateProductRequestValidator : AbstractValidator<CreateProductRequest>
    {
        public CreateProductRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name is required");
            RuleFor(x => x.Category)
                .NotEmpty()
                .WithMessage("Category is required");
            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("Description is required");
            RuleFor(x => x.ImageFile)
                .NotEmpty()
                .WithMessage("ImageFile is required");
            RuleFor(x => x.Price)
                .GreaterThan(0)
                .WithMessage("Price must be greater than 0");
        }
    }
    // Handler
    internal class CreateProductHandler(IDocumentSession session)
        : ICommandHandler<CreateProductRequest, CreateProductResponse>
    {
        public async Task<CreateProductResponse> Handle(CreateProductRequest request, CancellationToken cancellationToken)
        {
            // Create a product entity from a command object
            var product = new Product
            {
                Name = request.Name,
                Category = request.Category,
                Description = request.Description,
                ImageFile = request.ImageFile,
                Price = request.Price
            };
            // Save to a database
            session.Store(product);
            await session.SaveChangesAsync(cancellationToken);

            // Return creates a product result
            return new CreateProductResponse(product.Id);
        }
    }
}
