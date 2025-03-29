using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace DataBase;

public class F1DbContextFactory: IDesignTimeDbContextFactory<F1DbContext>
{
    public F1DbContext CreateDbContext(string[] args)
    {
        var homePath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        var settingPath = Path.Combine(homePath, "settings.yaml");
        var config = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddYamlFile(settingPath, optional: false)
            .Build();

        var connectionString = config["F1ApiConnection:DwhConnectionString"];

        var optionBuilder = new DbContextOptionsBuilder<F1DbContext>();
        optionBuilder.UseSqlServer(connectionString, 
            migration=>migration.MigrationsHistoryTable("F1ApiMigration"));
        return new F1DbContext(optionBuilder.Options);
    }
}