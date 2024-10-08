
using JasperFx.CodeGeneration.Frames;

namespace Catalog.API.Products.DeleteProduct;

    public record DeleteProductCommand(Guid Id) : ICommand<DeleteProductResult>;

    public record DeleteProductResult(bool isSuccess);

internal class DeleteProductHandler(IDocumentSession session, ILogger<DeleteProductHandler> logger) : ICommandHandler<DeleteProductCommand, DeleteProductResult>
{
    public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
    {
        var product = await session.LoadAsync<Product>(command.Id);

        if (product is null) {
            throw new ProductNotFoundException();
        }

        session.Delete<Product>(command.Id);
        await session.SaveChangesAsync(cancellationToken);

        return new DeleteProductResult(true);
    }
}

