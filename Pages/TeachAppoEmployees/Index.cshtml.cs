using Microsoft.AspNetCore.Mvc.RazorPages;
using wuchmiITHome.Models;
using wuchmiITHome.Services;

namespace wuchmiITHome.Pages.TeachAppoEmployees
{
    /// <summary>
    /// List page model for displaying all teach appointment employee records.
    /// Uses AsNoTracking() for read-only performance optimization.
    /// </summary>
    public class IndexModel : PageModel
    {
        private readonly TeachAppoEmployeeService _service;

        public IList<TeachAppoEmployee> TeachAppoEmployees { get; set; } = default!;

        public IndexModel(TeachAppoEmployeeService service)
        {
            _service = service;
        }

        public async Task OnGetAsync()
        {
            TeachAppoEmployees = await _service.GetAllAsync();
        }
    }
}
