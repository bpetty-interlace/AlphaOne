using HotChocolate;

namespace AlphaOne;

public class Mutation
{

    public async Task<Resource> UpsertResourceAsync(ResourceDbContext context, Resource resource)
    {
        context.Resources.Update(resource);
        await context.SaveChangesAsync();

        return resource;
    }

    public async Task<Resource?> DeleteResourceAsync(ResourceDbContext context, int id)
    {
        var resource = await context.Resources.FindAsync(id);
        if (resource == null)
        {
            return await Task.FromResult<Resource?>(null);
        }
        context.Resources.Remove(resource);
        await context.SaveChangesAsync();

        return resource;
    }
}

