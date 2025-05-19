namespace Catalog.API.Exceptions;
public class ProductNotFoundException(string message = "Product not found") : Exception(message);