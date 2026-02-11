using System.Net;
using HtmlAgilityPack;
using wuchmiITHome.Tests.TestInfrastructure;

namespace wuchmiITHome.Tests.TeachAppoEmployees
{
    /// <summary>
    /// Tests for TeachAppoEmployee delete page (Pages/TeachAppoEmployees/Delete.cshtml).
    /// Verifies deletion of records with confirmation and error handling for missing records.
    /// </summary>
    public class DeletePageTests : IAsyncLifetime
    {
        private readonly SqliteWebApplicationFactory _factory;
        private HttpClient _client;

        // Use the seeded test data
        private const int TestYr = 2024;
        private const string TestIdNo = "B987654321";
        private static readonly DateTime TestBirthday = DateTime.Parse("1988-08-20");

        public DeletePageTests()
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
        public async Task GetDeletePage_WithValidKey_ReturnsSuccessStatusCode()
        {
            var response = await _client.GetAsync(
                $"/TeachAppoEmployees/Delete?yr={TestYr}&idNo={TestIdNo}&birthday={TestBirthday:yyyy-MM-dd}");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetDeletePage_DisplaysRecordDetails()
        {
            var response = await _client.GetAsync(
                $"/TeachAppoEmployees/Delete?yr={TestYr}&idNo={TestIdNo}&birthday={TestBirthday:yyyy-MM-dd}");

            var html = await response.Content.ReadAsStringAsync();

            // Verify record details are displayed for confirmation
            Assert.Contains("李麗花", html);  // ChName
            Assert.Contains("Lee Lihua", html);  // EnName
        }

        [Fact]
        public async Task GetDeletePage_WithInvalidKey_ReturnsNotFound()
        {
            var response = await _client.GetAsync(
                "/TeachAppoEmployees/Delete?yr=9999&idNo=INVALID&birthday=2000-01-01");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task GetDeletePage_ShowsConfirmationMessage()
        {
            var response = await _client.GetAsync(
                $"/TeachAppoEmployees/Delete?yr={TestYr}&idNo={TestIdNo}&birthday={TestBirthday:yyyy-MM-dd}");

            var html = await response.Content.ReadAsStringAsync();

            // Verify confirmation message is shown
            Assert.Contains("confirm", html.ToLower());
        }

        [Fact]
        public async Task PostDeletePage_DeletesRecord()
        {
            // First, verify the record exists
            var beforeResponse = await _client.GetAsync(
                $"/TeachAppoEmployees/Details?yr={TestYr}&idNo={TestIdNo}&birthday={TestBirthday:yyyy-MM-dd}");
            Assert.Equal(System.Net.HttpStatusCode.OK, beforeResponse.StatusCode);

            // Delete the record
            var deleteResponse = await _client.PostAsync(
                $"/TeachAppoEmployees/Delete?yr={TestYr}&idNo={TestIdNo}&birthday={TestBirthday:yyyy-MM-dd}",
                new FormUrlEncodedContent(new Dictionary<string, string>()));

            // Should redirect to index
            Assert.Equal(HttpStatusCode.Redirect, deleteResponse.StatusCode);

            // Verify the record is gone by trying to view it
            var afterResponse = await _client.GetAsync(
                $"/TeachAppoEmployees/Details?yr={TestYr}&idNo={TestIdNo}&birthday={TestBirthday:yyyy-MM-dd}");

            Assert.Equal(HttpStatusCode.NotFound, afterResponse.StatusCode);
        }

        [Fact]
        public async Task PostDeletePage_WithAlreadyDeletedRecord_ShowsFriendlyError()
        {
            // Delete the record first
            await _client.PostAsync(
                $"/TeachAppoEmployees/Delete?yr={TestYr}&idNo={TestIdNo}&birthday={TestBirthday:yyyy-MM-dd}",
                new FormUrlEncodedContent(new Dictionary<string, string>()));

            // Try to delete again
            var response = await _client.PostAsync(
                $"/TeachAppoEmployees/Delete?yr={TestYr}&idNo={TestIdNo}&birthday={TestBirthday:yyyy-MM-dd}",
                new FormUrlEncodedContent(new Dictionary<string, string>()));

            // Should show friendly error message
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task GetDeletePage_ContainsDeleteButton()
        {
            var response = await _client.GetAsync(
                $"/TeachAppoEmployees/Delete?yr={TestYr}&idNo={TestIdNo}&birthday={TestBirthday:yyyy-MM-dd}");

            var html = await response.Content.ReadAsStringAsync();

            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            // Verify there's a delete button/form
            var deleteButton = doc.DocumentNode.SelectSingleNode("//button[@type='submit']") ??
                              doc.DocumentNode.SelectSingleNode("//input[@type='submit']");

            Assert.NotNull(deleteButton);
        }

        [Fact]
        public async Task GetDeletePage_ContainsCancelLink()
        {
            var response = await _client.GetAsync(
                $"/TeachAppoEmployees/Delete?yr={TestYr}&idNo={TestIdNo}&birthday={TestBirthday:yyyy-MM-dd}");

            var html = await response.Content.ReadAsStringAsync();

            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            // Verify there's a cancel/back link
            var cancelLink = doc.DocumentNode.SelectSingleNode("//a[contains(@class, 'btn-secondary')]") ??
                           doc.DocumentNode.SelectSingleNode("//a[contains(@href, '/TeachAppoEmployees')]");

            Assert.NotNull(cancelLink);
        }
    }
}
