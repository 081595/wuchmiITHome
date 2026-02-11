using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using wuchmiITHome.Data;
using wuchmiITHome.Models;
using wuchmiITHome.Tests.TestInfrastructure;

namespace wuchmiITHome.Tests.Data
{
    /// <summary>
    /// Tests for DbContext schema and migrations.
    /// Ensures the database is properly configured and migrations apply correctly.
    /// </summary>
    public class DbContextTests : IAsyncLifetime
    {
        private readonly SqliteWebApplicationFactory _factory;

        public DbContextTests()
        {
            _factory = new SqliteWebApplicationFactory();
        }

        public async Task InitializeAsync()
        {
            // Initialize the database with migrations
            await _factory.InitializeDatabaseAsync();
        }

        public async Task DisposeAsync()
        {
            await Task.Run(() => _factory.Dispose());
        }

        [Fact]
        public async Task Database_CanCreateAndQueryTeachAppoEmployees()
        {
            using var scope = _factory.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<wuchmiITHomeContext>();

            // Create a test employee
            var employee = new TeachAppoEmployee
            {
                Yr = 2024,
                IdNo = "TEST123456",
                Birthday = DateTime.Parse("1990-01-01"),
                EmplNo = "999999",
                ChName = "測試員",
                EnName = "Test Employee",
                Email = "test@example.com",
                RefreshToken = "test_token"
            };

            context.TeachAppoEmployees.Add(employee);
            await context.SaveChangesAsync();

            // Query it back
            var retrieved = await context.TeachAppoEmployees
                .FirstOrDefaultAsync(e => e.Yr == 2024 && e.IdNo == "TEST123456" && e.Birthday == DateTime.Parse("1990-01-01"));

            Assert.NotNull(retrieved);
            Assert.Equal("測試員", retrieved.ChName);
            Assert.NotEqual(default(DateTime), retrieved.CreateDate);
        }

        [Fact]
        public async Task Database_CompositeKeyPreventsDuplicates()
        {
            using var scope = _factory.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<wuchmiITHomeContext>();

            // Create first employee
            var employee1 = new TeachAppoEmployee
            {
                Yr = 2024,
                IdNo = "DUP123456",
                Birthday = DateTime.Parse("1990-01-01"),
                EmplNo = "111111",
                ChName = "員工A",
                EnName = "Employee A",
                Email = "empA@example.com",
                RefreshToken = "tokenA"
            };

            context.TeachAppoEmployees.Add(employee1);
            await context.SaveChangesAsync();

            // Try to create duplicate (should fail)
            using var scope2 = _factory.Services.CreateScope();
            var context2 = scope2.ServiceProvider.GetRequiredService<wuchmiITHomeContext>();
            
            var employee2 = new TeachAppoEmployee
            {
                Yr = 2024,
                IdNo = "DUP123456",
                Birthday = DateTime.Parse("1990-01-01"),
                EmplNo = "222222",
                ChName = "員工B",
                EnName = "Employee B",
                Email = "empB@example.com",
                RefreshToken = "tokenB"
            };

            context2.TeachAppoEmployees.Add(employee2);
            
            var exception = await Assert.ThrowsAsync<DbUpdateException>(async () =>
                await context2.SaveChangesAsync());

            Assert.NotNull(exception);
        }

        [Fact]
        public async Task Database_TimestampsAreSetAutomatically()
        {
            using var scope = _factory.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<wuchmiITHomeContext>();

            var employee = new TeachAppoEmployee
            {
                Yr = 2024,
                IdNo = "TIMEST123",
                Birthday = DateTime.Parse("1990-01-01"),
                EmplNo = "333333",
                ChName = "時間測試",
                EnName = "Time Test",
                Email = "time@example.com",
                RefreshToken = "time_token"
            };

            await context.TeachAppoEmployees.AddAsync(employee);
            await context.SaveChangesAsync();

            var retrieved = await context.TeachAppoEmployees
                .FirstOrDefaultAsync(e => e.Yr == 2024 && e.IdNo == "TIMEST123" && e.Birthday == DateTime.Parse("1990-01-01"));

            Assert.NotNull(retrieved);
            Assert.NotEqual(default(DateTime), retrieved.CreateDate);
            Assert.NotEqual(default(DateTime), retrieved.UpdateDate);
            // Both should be equal and very close in time
            Assert.True((retrieved.UpdateDate - retrieved.CreateDate).TotalSeconds < 1,
                $"UpdateDate and CreateDate should be almost equal. CreateDate: {retrieved.CreateDate}, UpdateDate: {retrieved.UpdateDate}");
        }
    }
}
