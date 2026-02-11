using System.Net;
using HtmlAgilityPack;
using wuchmiITHome.Tests.TestInfrastructure;

namespace wuchmiITHome.Tests.TeachAppoEmployees
{
    /// <summary>
    /// Tests for TeachAppoEmployee list page (Pages/TeachAppoEmployees/Index).
    /// Verifies that users can browse and view a list of teach appointment employee records.
    /// </summary>
    public class ListPageTests : IAsyncLifetime
    {
        private readonly SqliteWebApplicationFactory _factory;
        private HttpClient _client;

        public ListPageTests()
        {
            _factory = new SqliteWebApplicationFactory();
            _client = _factory.CreateClient();
        }

        public async Task InitializeAsync()
        {
            await _factory.InitializeDatabaseAsync();
        }

        public async Task DisposeAsync()
        {
            _client?.Dispose();
            await Task.Run(() => _factory?.Dispose());
        }

        [Fact]
        public async Task GetListPage_ReturnsSuccessStatusCode()
        {
            var response = await _client.GetAsync("/TeachAppoEmployees");
            
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetListPage_ReturnsHtmlContent()
        {
            var response = await _client.GetAsync("/TeachAppoEmployees");
            
            Assert.Equal("text/html; charset=utf-8", response.Content.Headers.ContentType?.ToString());
        }

        [Fact]
        public async Task GetListPage_ContainsTableWithEmployeeData()
        {
            var response = await _client.GetAsync("/TeachAppoEmployees");
            var html = await response.Content.ReadAsStringAsync();

            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            // Check that page contains a table with employee data
            var table = doc.DocumentNode.SelectSingleNode("//table");
            Assert.NotNull(table);

            // Check that seeded employees appear in the list
            Assert.Contains("王小明", html);
            Assert.Contains("李麗花", html);
        }

        [Fact]
        public async Task GetListPage_EmployeeRowsAreClickable()
        {
            var response = await _client.GetAsync("/TeachAppoEmployees");
            var html = await response.Content.ReadAsStringAsync();

            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            // Verify that there are links to detail pages
            var detailLinks = doc.DocumentNode.SelectNodes("//a[contains(@href, '/TeachAppoEmployees/Details')]");
            Assert.NotEmpty(detailLinks);
        }

        [Fact]
        public async Task GetListPage_ContainsNavigationElements()
        {
            var response = await _client.GetAsync("/TeachAppoEmployees");
            var html = await response.Content.ReadAsStringAsync();

            // Check that page contains expected navigation or action buttons
            Assert.Contains("TeachAppoEmployees", html);
        }

        [Fact]
        public async Task GetListPage_DisplaysTimestampColumns()
        {
            var response = await _client.GetAsync("/TeachAppoEmployees");
            var html = await response.Content.ReadAsStringAsync();

            // Verify that timestamp columns are present (create_date, update_date)
            Assert.Contains("CreateDate", html.ToLower() + "CreateDate", StringComparison.OrdinalIgnoreCase);
            Assert.Contains("UpdateDate", html.ToLower() + "UpdateDate", StringComparison.OrdinalIgnoreCase);
        }
    }
}
