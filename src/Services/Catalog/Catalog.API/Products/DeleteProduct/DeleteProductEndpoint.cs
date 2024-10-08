
using Catalog.API.Products.UpdateProduct;

namespace Catalog.API.Products.DeleteProduct;

//public record DeleteProductRequest(Guid Id);

public record DeleteProductResponse(bool isSuccess);

public class DeleteProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/products/delete/{Id}", async(Guid Id, ISender sender) =>
        {
            var result = await sender.Send(new DeleteProductCommand(Id));

            var response = result.Adapt<DeleteProductResponse>();

            return Results.Ok(response);

        }).WithName("DeleteProduct")
            .Produces<DeleteProductResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Delete Product")
            .WithDescription("Delete Product");
    }
}

