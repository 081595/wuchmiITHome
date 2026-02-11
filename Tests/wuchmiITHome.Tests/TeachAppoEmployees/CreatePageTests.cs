using System.Net;
using HtmlAgilityPack;
using wuchmiITHome.Tests.TestInfrastructure;

namespace wuchmiITHome.Tests.TeachAppoEmployees
{
    /// <summary>
    /// Tests for TeachAppoEmployee create page (Pages/TeachAppoEmployees/Create.cshtml).
    /// Verifies creation of new records with validation, error handling, and duplicate key prevention.
    /// </summary>
    public class CreatePageTests : IAsyncLifetime
    {
        private readonly SqliteWebApplicationFactory _factory;
        private HttpClient _client;

        public CreatePageTests()
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
        public async Task GetCreatePage_ReturnsSuccessStatusCode()
        {
            var response = await _client.GetAsync("/TeachAppoEmployees/Create");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetCreatePage_ReturnsHtmlWithForm()
        {
            var response = await _client.GetAsync("/TeachAppoEmployees/Create");
            var html = await response.Content.ReadAsStringAsync();

            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            // Verify form exists with all required fields
            var form = doc.DocumentNode.SelectSingleNode("//form");
            Assert.NotNull(form);

            // Check for required input fields
            Assert.NotNull(doc.DocumentNode.SelectSingleNode("//input[@name='TeachAppoEmployee.Yr']"));
            Assert.NotNull(doc.DocumentNode.SelectSingleNode("//input[@name='TeachAppoEmployee.IdNo']"));
            Assert.NotNull(doc.DocumentNode.SelectSingleNode("//input[@name='TeachAppoEmployee.Birthday']"));
            Assert.NotNull(doc.DocumentNode.SelectSingleNode("//input[@name='TeachAppoEmployee.EmplNo']"));
            Assert.NotNull(doc.DocumentNode.SelectSingleNode("//input[@name='TeachAppoEmployee.ChName']"));
            Assert.NotNull(doc.DocumentNode.SelectSingleNode("//input[@name='TeachAppoEmployee.EnName']"));
            Assert.NotNull(doc.DocumentNode.SelectSingleNode("//input[@name='TeachAppoEmployee.Email']"));
        }

        [Fact]
        public async Task PostCreatePage_WithValidData_CreatesNewRecord()
        {
            var formData = new Dictionary<string, string>
            {
                { "TeachAppoEmployee.Yr", "2024" },
                { "TeachAppoEmployee.IdNo", "NEWTEST123" },
                { "TeachAppoEmployee.Birthday", "1995-06-20" },
                { "TeachAppoEmployee.EmplNo", "654321" },
                { "TeachAppoEmployee.ChName", "新員工" },
                { "TeachAppoEmployee.EnName", "New Employee" },
                { "TeachAppoEmployee.Email", "new@example.com" },
                { "TeachAppoEmployee.RefreshToken", "new_token" }
            };

            var response = await _client.PostAsync("/TeachAppoEmployees/Create",
                new FormUrlEncodedContent(formData));

            // Should redirect to index after successful creation
            Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
            Assert.True(response.Headers.Location?.ToString().Contains("/TeachAppoEmployees") ?? false);
        }

        [Fact]
        public async Task PostCreatePage_WithDuplicateKey_ShowsError()
        {
            // Create first record successfully
            var formData1 = new Dictionary<string, string>
            {
                { "TeachAppoEmployee.Yr", "2026" },
                { "TeachAppoEmployee.IdNo", "DUPTEST123" },
                { "TeachAppoEmployee.Birthday", "1993-08-15" },
                { "TeachAppoEmployee.EmplNo", "111111" },
                { "TeachAppoEmployee.ChName", "員工一" },
                { "TeachAppoEmployee.EnName", "Employee One" },
                { "TeachAppoEmployee.Email", "emp1@example.com" },
                { "TeachAppoEmployee.RefreshToken", "token_one" }
            };

            await _client.PostAsync("/TeachAppoEmployees/Create",
                new FormUrlEncodedContent(formData1));

            // Try to create duplicate
            var formData2 = new Dictionary<string, string>
            {
                { "TeachAppoEmployee.Yr", "2026" },
                { "TeachAppoEmployee.IdNo", "DUPTEST123" },
                { "TeachAppoEmployee.Birthday", "1993-08-15" },
                { "TeachAppoEmployee.EmplNo", "222222" },
                { "TeachAppoEmployee.ChName", "員工二" },
                { "TeachAppoEmployee.EnName", "Employee Two" },
                { "TeachAppoEmployee.Email", "emp2@example.com" },
                { "TeachAppoEmployee.RefreshToken", "token_two" }
            };

            var response = await _client.PostAsync("/TeachAppoEmployees/Create",
                new FormUrlEncodedContent(formData2));

            var html = await response.Content.ReadAsStringAsync();

            // Should show error message about duplicate key
            Assert.Contains("duplicate", html.ToLower());
        }

        [Fact]
        public async Task PostCreatePage_WithMissingRequiredField_ShowsValidationError()
        {
            var formData = new Dictionary<string, string>
            {
                { "TeachAppoEmployee.Yr", "2024" },
                // Missing IdNo (required field)
                { "TeachAppoEmployee.Birthday", "1995-06-20" },
                { "TeachAppoEmployee.EmplNo", "654321" }
                // Missing other required fields
            };

            var response = await _client.PostAsync("/TeachAppoEmployees/Create",
                new FormUrlEncodedContent(formData));

            var html = await response.Content.ReadAsStringAsync();

            // Should return the form with validation messages
            Assert.Contains("required", html.ToLower());
        }

        [Fact]
        public async Task PostCreatePage_WithInvalidEmail_ShowsValidationError()
        {
            var formData = new Dictionary<string, string>
            {
                { "TeachAppoEmployee.Yr", "2024" },
                { "TeachAppoEmployee.IdNo", "VALID123456" },
                { "TeachAppoEmployee.Birthday", "1995-06-20" },
                { "TeachAppoEmployee.EmplNo", "654321" },
                { "TeachAppoEmployee.ChName", "測試員" },
                { "TeachAppoEmployee.EnName", "Test User" },
                { "TeachAppoEmployee.Email", "invalid-email" },  // Invalid format
                { "TeachAppoEmployee.RefreshToken", "token" }
            };

            var response = await _client.PostAsync("/TeachAppoEmployees/Create",
                new FormUrlEncodedContent(formData));

            var html = await response.Content.ReadAsStringAsync();

            // Should show email validation error
            Assert.Contains("email", html.ToLower());
        }

        [Fact]
        public async Task PostCreatePage_WithInvalidDateFormat_ShowsValidationError()
        {
            var formData = new Dictionary<string, string>
            {
                { "TeachAppoEmployee.Yr", "2024" },
                { "TeachAppoEmployee.IdNo", "DATETEST123" },
                { "TeachAppoEmployee.Birthday", "invalid-date" },  // Invalid date format
                { "TeachAppoEmployee.EmplNo", "654321" },
                { "TeachAppoEmployee.ChName", "日期測試" },
                { "TeachAppoEmployee.EnName", "Date Test" },
                { "TeachAppoEmployee.Email", "date@example.com" },
                { "TeachAppoEmployee.RefreshToken", "token" }
            };

            var response = await _client.PostAsync("/TeachAppoEmployees/Create",
                new FormUrlEncodedContent(formData));

            var html = await response.Content.ReadAsStringAsync();

            // Should show date/format validation error
            Assert.Contains("date", html.ToLower());
        }
    }
}
