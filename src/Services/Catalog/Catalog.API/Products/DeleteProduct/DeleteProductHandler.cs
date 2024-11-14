
using JasperFx.CodeGeneration.Frames;

namespace Catalog.API.Products.DeleteProduct;

    public record DeleteProductCommand(Guid Id) : ICommand<DeleteProductResult>;

    public record DeleteProductResult(bool isSuccess);

internal class DeleteProductHandler(IDocumentSession session, ILogger<DeleteProductHandler> logger) : ICommandHandler<DeleteProductCommand, DeleteProductResult>
{
    public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
    {
        //logger.LogInformation("deleteproductcommandHandler.handle called with {@Command}");

        var product = await session.LoadAsync<Product>(command.Id);

        if (product is null) {
            throw new ProductNotFoundException(command.Id);
        }

        session.Delete<Product>(command.Id);
        await session.SaveChangesAsync(cancellationToken);

        return new DeleteProductResult(true);
    }
}

