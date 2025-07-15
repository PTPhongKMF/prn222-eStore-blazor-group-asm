using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace DAL;

public class EStoreDatabaseContextFactory : IDesignTimeDbContextFactory<EStoreDatabaseContext>
{
    public EStoreDatabaseContext CreateDbContext(string[] args)
    {
        // Get the current directory and navigate up to the solution directory
        var currentDirectory = Directory.GetCurrentDirectory();
        var solutionDirectory = Directory.GetParent(currentDirectory)?.FullName;
        var configurationPath = Path.Combine(solutionDirectory ?? "", "eStore");

        var configuration = new ConfigurationBuilder()
            .SetBasePath(configurationPath)
            .AddJsonFile("appsettings.json")
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<EStoreDatabaseContext>();
        optionsBuilder.UseSqlServer(configuration.GetConnectionString("eStoreDatabase"));

        return new EStoreDatabaseContext(optionsBuilder.Options);
    }
}