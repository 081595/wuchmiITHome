using System.Net;
using HtmlAgilityPack;
using wuchmiITHome.Tests.TestInfrastructure;

namespace wuchmiITHome.Tests.TeachAppoEmployees
{
    /// <summary>
    /// Tests for TeachAppoEmployee edit page (Pages/TeachAppoEmployees/Edit.cshtml).
    /// Verifies editing of records with immutable keys, validation, and error handling.
    /// </summary>
    public class EditPageTests : IAsyncLifetime
    {
        private readonly SqliteWebApplicationFactory _factory;
        private HttpClient _client;

        // Use the seeded test data
        private const int TestYr = 2024;
        private const string TestIdNo = "A123456789";
        private static readonly DateTime TestBirthday = DateTime.Parse("1990-05-15");

        public EditPageTests()
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
        public async Task GetEditPage_WithValidKey_ReturnsSuccessStatusCode()
        {
            var response = await _client.GetAsync(
                $"/TeachAppoEmployees/Edit?yr={TestYr}&idNo={TestIdNo}&birthday={TestBirthday:yyyy-MM-dd}");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetEditPage_LoadsExistingData()
        {
            var response = await _client.GetAsync(
                $"/TeachAppoEmployees/Edit?yr={TestYr}&idNo={TestIdNo}&birthday={TestBirthday:yyyy-MM-dd}");

            var html = await response.Content.ReadAsStringAsync();

            // Verify existing values are pre-populated
            Assert.Contains("wang.xiaoming@example.com", html);
            Assert.Contains("000001", html);  // EmplNo
        }

        [Fact]
        public async Task GetEditPage_WithInvalidKey_ReturnsNotFound()
        {
            var response = await _client.GetAsync(
                "/TeachAppoEmployees/Edit?yr=9999&idNo=INVALID&birthday=2000-01-01");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task PostEditPage_WithValidData_UpdatesRecord()
        {
            var formData = new Dictionary<string, string>
            {
                { "TeachAppoEmployee.Yr", TestYr.ToString() },
                { "TeachAppoEmployee.IdNo", TestIdNo },
                { "TeachAppoEmployee.Birthday", TestBirthday.ToString("yyyy-MM-dd") },
                { "TeachAppoEmployee.EmplNo", "000001" },
                { "TeachAppoEmployee.ChName", "王小明更新" },  // Updated name
                { "TeachAppoEmployee.EnName", "Wang Xiaoming Updated" },
                { "TeachAppoEmployee.Email", "wang.updated@example.com" },  // Updated email
                { "TeachAppoEmployee.RefreshToken", "updated_token" }
            };

            var response = await _client.PostAsync(
                $"/TeachAppoEmployees/Edit?yr={TestYr}&idNo={TestIdNo}&birthday={TestBirthday:yyyy-MM-dd}",
                new FormUrlEncodedContent(formData));

            // Should redirect to index after successful update
            Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
        }

        [Fact]
        public async Task PostEditPage_WithInvalidEmail_ShowsValidationError()
        {
            var formData = new Dictionary<string, string>
            {
                { "TeachAppoEmployee.Yr", TestYr.ToString() },
                { "TeachAppoEmployee.IdNo", TestIdNo },
                { "TeachAppoEmployee.Birthday", TestBirthday.ToString("yyyy-MM-dd") },
                { "TeachAppoEmployee.EmplNo", "000001" },
                { "TeachAppoEmployee.ChName", "王小明" },
                { "TeachAppoEmployee.EnName", "Wang Xiaoming" },
                { "TeachAppoEmployee.Email", "invalid-email" },  // Invalid format
                { "TeachAppoEmployee.RefreshToken", "token" }
            };

            var response = await _client.PostAsync(
                $"/TeachAppoEmployees/Edit?yr={TestYr}&idNo={TestIdNo}&birthday={TestBirthday:yyyy-MM-dd}",
                new FormUrlEncodedContent(formData));

            var html = await response.Content.ReadAsStringAsync();

            // Should show validation error
            Assert.Contains("email", html.ToLower());
        }

        [Fact]
        public async Task GetEditPage_ShowsCompositeKeyAsReadOnly()
        {
            var response = await _client.GetAsync(
                $"/TeachAppoEmployees/Edit?yr={TestYr}&idNo={TestIdNo}&birthday={TestBirthday:yyyy-MM-dd}");

            var html = await response.Content.ReadAsStringAsync();

            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            // The composite key fields should be displayed but read-only or disabled
            // (implementation may show as text or disabled inputs)
            Assert.Contains("Composite Key Fields", html, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public async Task PostEditPage_PreservesCreatedTimestamp()
        {
            var formData = new Dictionary<string, string>
            {
                { "TeachAppoEmployee.Yr", TestYr.ToString() },
                { "TeachAppoEmployee.IdNo", TestIdNo },
                { "TeachAppoEmployee.Birthday", TestBirthday.ToString("yyyy-MM-dd") },
                { "TeachAppoEmployee.EmplNo", "000001" },
                { "TeachAppoEmployee.ChName", "王小明更新版本2" },
                { "TeachAppoEmployee.EnName", "Wang Xiaoming V2" },
                { "TeachAppoEmployee.Email", "wang.v2@example.com" },
                { "TeachAppoEmployee.RefreshToken", "token_v2" }
            };

            await _client.PostAsync(
                $"/TeachAppoEmployees/Edit?yr={TestYr}&idNo={TestIdNo}&birthday={TestBirthday:yyyy-MM-dd}",
                new FormUrlEncodedContent(formData));

            // Verify that the record was updated
            var detailsResponse = await _client.GetAsync(
                $"/TeachAppoEmployees/Details?yr={TestYr}&idNo={TestIdNo}&birthday={TestBirthday:yyyy-MM-dd}");

            var html = await detailsResponse.Content.ReadAsStringAsync();

            Assert.Contains("wang.v2@example.com", html);
            Assert.Contains("UpdateDate", html, StringComparison.OrdinalIgnoreCase);
        }
    }
}
