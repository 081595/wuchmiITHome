using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using wuchmiITHome.Models;
using wuchmiITHome.Services;

namespace wuchmiITHome.Pages.TeachAppoEmployees
{
    /// <summary>
    /// Detail page model for displaying a single teach appointment employee record.
    /// Shows read-only composite key fields and timestamps with user-friendly error handling.
    /// </summary>
    public class DetailsModel : PageModel
    {
        private readonly TeachAppoEmployeeService _service;

        public TeachAppoEmployee TeachAppoEmployee { get; set; } = default!;

        public DetailsModel(TeachAppoEmployeeService service)
        {
            _service = service;
        }

        public async Task<IActionResult> OnGetAsync(int? yr, string? idNo, DateTime? birthday)
        {
            if (yr == null || idNo == null || birthday == null)
            {
                return NotFound();
            }

            var teachAppoEmployee = await _service.GetByIdAsync(yr.Value, idNo, birthday.Value);

            if (teachAppoEmployee == null)
            {
                return NotFound("The teach appointment employee record you are looking for does not exist or has been deleted.");
            }

            TeachAppoEmployee = teachAppoEmployee;
            return Page();
        }
    }
}
