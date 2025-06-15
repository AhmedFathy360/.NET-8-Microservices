namespace Catalog.API.Products.GetProductById;
public record GetProductByIdCommand(Guid Id) : IQuery<GetProductByIdResult>;
public record GetProductByIdResult(Product Product);
internal class GetProductByIdQueryHandler(IDocumentSession session)
    : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
{
    public async Task<GetProductByIdResult>
    Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
    {
        // Use the session to query the database
        var product = await session.LoadAsync<Product>(query.Id, cancellationToken);

        if (product is null)
        {
            throw new ProductNotFoundException(query.Id);
        }
        // Return the product
        return new GetProductByIdResult(product);
    }
}