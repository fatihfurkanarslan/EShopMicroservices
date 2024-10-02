
namespace Catalog.API.Products.GetProductByCategory;

public record GetProductByCategoryResult(IEnumerable<Product> Products);

public record GetProductByCategoryQuery(string Category) : IQuery<GetProductByCategoryResult>;

internal class GetProductByCategoryHandler
    (IDocumentSession session, ILogger<GetProductByCategoryHandler> logger)
    : IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
{
    public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken)
    {

        logger.LogInformation("getproductbyCategoryqueryhandler called with {@Query}", query);

        var products = await session.Query<Product>()
            .Where(x => x.Category.Contains(query.Category)).ToListAsync();

        return new GetProductByCategoryResult(products);
    }
}


