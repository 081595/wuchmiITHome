using System.Net;
using HtmlAgilityPack;
using wuchmiITHome.Tests.TestInfrastructure;

namespace wuchmiITHome.Tests.TeachAppoEmployees
{
    /// <summary>
    /// Tests for TeachAppoEmployee details page (Pages/TeachAppoEmployees/Details.cshtml).
    /// Verifies that users can view detailed information about a specific teach appointment employee record,
    /// including read-only composite key fields and timestamp display.
    /// </summary>
    public class DetailsPageTests : IAsyncLifetime
    {
        private readonly SqliteWebApplicationFactory _factory;
        private HttpClient _client;

        // Use the seeded test data from SeedData
        private const int TestYr = 2024;
        private const string TestIdNo = "A123456789";
        private static readonly DateTime TestBirthday = DateTime.Parse("1990-05-15");

        public DetailsPageTests()
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
        public async Task GetDetailsPage_WithValidKey_ReturnsSuccessStatusCode()
        {
            var response = await _client.GetAsync(
                $"/TeachAppoEmployees/Details?yr={TestYr}&idNo={TestIdNo}&birthday={TestBirthday:yyyy-MM-dd}");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetDetailsPage_WithValidKey_DisplaysEmployeeData()
        {
            var response = await _client.GetAsync(
                $"/TeachAppoEmployees/Details?yr={TestYr}&idNo={TestIdNo}&birthday={TestBirthday:yyyy-MM-dd}");

            var html = await response.Content.ReadAsStringAsync();

            Assert.Contains("王小明", html);  // ChName
            Assert.Contains("Wang Xiaoming", html);  // EnName
            Assert.Contains("wang.xiaoming@example.com", html);  // Email
        }

        [Fact]
        public async Task GetDetailsPage_WithInvalidKey_ReturnsNotFound()
        {
            var response = await _client.GetAsync(
                "/TeachAppoEmployees/Details?yr=9999&idNo=INVALID&birthday=2000-01-01");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task GetDetailsPage_DisplaysCompositeKeyFields()
        {
            var response = await _client.GetAsync(
                $"/TeachAppoEmployees/Details?yr={TestYr}&idNo={TestIdNo}&birthday={TestBirthday:yyyy-MM-dd}");

            var html = await response.Content.ReadAsStringAsync();

            // Verify composite key fields are displayed
            Assert.Contains(TestYr.ToString(), html);
            Assert.Contains(TestIdNo, html);
            Assert.Contains(TestBirthday.ToString("yyyy-MM-dd"), html);
        }

        [Fact]
        public async Task GetDetailsPage_DisplaysTimestamps()
        {
            var response = await _client.GetAsync(
                $"/TeachAppoEmployees/Details?yr={TestYr}&idNo={TestIdNo}&birthday={TestBirthday:yyyy-MM-dd}");

            var html = await response.Content.ReadAsStringAsync();

            // Verify timestamps are displayed
            Assert.Contains("CreateDate", html, StringComparison.OrdinalIgnoreCase);
            Assert.Contains("UpdateDate", html, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public async Task GetDetailsPage_ContainsBackLink()
        {
            var response = await _client.GetAsync(
                $"/TeachAppoEmployees/Details?yr={TestYr}&idNo={TestIdNo}&birthday={TestBirthday:yyyy-MM-dd}");

            var html = await response.Content.ReadAsStringAsync();

            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            // Verify there's a link back to the list page
            var backLink = doc.DocumentNode.SelectSingleNode("//a[contains(@href, '/TeachAppoEmployees')]");
            Assert.NotNull(backLink);
        }

        [Fact]
        public async Task GetDetailsPage_ContainsEditAndDeleteLinks()
        {
            var response = await _client.GetAsync(
                $"/TeachAppoEmployees/Details?yr={TestYr}&idNo={TestIdNo}&birthday={TestBirthday:yyyy-MM-dd}");

            var html = await response.Content.ReadAsStringAsync();

            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            // Verify there are links to edit and delete pages
            var editLink = doc.DocumentNode.SelectSingleNode("//a[contains(@href, '/TeachAppoEmployees/Edit')]");
            var deleteLink = doc.DocumentNode.SelectSingleNode("//a[contains(@href, '/TeachAppoEmployees/Delete')]");
            
            Assert.NotNull(editLink);
            Assert.NotNull(deleteLink);
        }
    }
}
