using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using wuchmiITHome.Data;

namespace wuchmiITHome.Tests.TestInfrastructure;

/// <summary>
/// Test web application factory that uses an in-memory SQLite database for testing.
/// Each test gets a fresh database instance.
/// </summary>
public class SqliteWebApplicationFactory : WebApplicationFactory<Program>
{
    private SqliteConnection? _connection;

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            // Remove the existing DbContext registration
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<wuchmiITHomeContext>));
            
            if (descriptor != null)
            {
                services.Remove(descriptor);
            }

            // Create a new in-memory SQLite connection
            _connection = new SqliteConnection("DataSource=:memory:");
            _connection.Open();

            // Add DbContext using the in-memory SQLite database
            services.AddDbContext<wuchmiITHomeContext>(options =>
            {
                options.UseSqlite(_connection);
            });
        });
    }

    public async Task InitializeDatabaseAsync()
    {
        using (var scope = Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<wuchmiITHomeContext>();
            await dbContext.Database.MigrateAsync();
        }
    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
        if (disposing)
        {
            _connection?.Close();
            _connection?.Dispose();
        }
    }
}

