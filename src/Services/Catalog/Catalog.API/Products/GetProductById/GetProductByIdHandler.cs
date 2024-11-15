﻿



namespace Catalog.API.Products.GetProductById;

public record GetProductByIdResult(Product Product);

public record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdResult>;



internal class GetProductByIdHandler(IDocumentSession session, ILogger<GetProductByIdHandler> logger) 
    : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
    {
    public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("getproductbyidqueryhandler called with {@Query}", query);

        var product = await session.LoadAsync<Product>(query.Id, cancellationToken);

        if (product is null) {
            throw new ProductNotFoundException(query.Id);    
        }
        return new GetProductByIdResult(product);
    }
}

