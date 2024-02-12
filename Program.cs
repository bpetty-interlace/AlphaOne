using AlphaOne;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Azure.Functions.Worker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureServices(services =>
    {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();

        string connectionString = "Host=localhost;Username=postgres;Password=;Database=tmp";
        // SQLite = "Data Source=Entity/dev.db"

        services.AddPooledDbContextFactory<ResourceDbContext>(options =>
            options.UseNpgsql(connectionString));
    })
    .AddGraphQLFunction(b => {
        b.AddFiltering();   // https://chillicream.com/docs/hotchocolate/v13/fetching-data/filtering
        b.AddSorting();     // https://chillicream.com/docs/hotchocolate/v13/fetching-data/sorting
        b.AddProjections(); // Allow for table joining
        b.AddQueryType<Query>()
            .RegisterDbContext<ResourceDbContext>(DbContextKind.Pooled);
        b.AddMutationType<Mutation>()
            .RegisterDbContext<ResourceDbContext>(DbContextKind.Pooled);
    })
    .Build();

host.Run();
