using HotChocolate;
using HotChocolate.Resolvers;

namespace AlphaOne;

public class Query
{
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Resource> GetResources(ResourceDbContext context)
        => context.Resources;
}

