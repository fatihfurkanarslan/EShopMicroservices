﻿



namespace Catalog.API.Products.UpdateProduct;



public record UpdateProductCommand(Guid Id, string Name, List<string> Category, string Description, string ImageFile, decimal Price)
: ICommand<UpdateProductResult>;
public record UpdateProductResult(bool isSuccess);


public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(command => command.Id).NotEmpty().WithMessage("Product Id is required.");
        RuleFor(command => command.Name)
            .NotEmpty().WithMessage("Product Name is required.")
            .Length(2, 100).WithMessage("Length of name must be between 2 and 100 chars.");
    }
}


internal class UpdateProductCommandHandler(IDocumentSession session, ILogger<UpdateProductCommandHandler> logger) : ICommandHandler<UpdateProductCommand, UpdateProductResult>
{
    public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        //Business logic to implement creating a product 

        var product = await session.LoadAsync<Product>(command.Id, cancellationToken);

        if (product is null)
        {
            throw new ProductNotFoundException(command.Id);
        }


        product.Name = command.Name;
        product.Category = command.Category;
        product.Description = command.Description;
        product.ImageFile = command.ImageFile;
        product.Price = command.Price;


        //TODO
        //save to DB
        session.Update(product);
        await session.SaveChangesAsync(cancellationToken);

        //return result
        return new UpdateProductResult(true);

    }
}



