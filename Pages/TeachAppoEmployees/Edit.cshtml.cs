using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using wuchmiITHome.Models;
using wuchmiITHome.Services;

namespace wuchmiITHome.Pages.TeachAppoEmployees
{
    /// <summary>
    /// Edit page model for updating an existing teach appointment employee record.
    /// Primary key fields (Yr, IdNo, Birthday) are immutable and display as read-only.
    /// </summary>
    public class EditModel : PageModel
    {
        private readonly TeachAppoEmployeeService _service;

        [BindProperty]
        public TeachAppoEmployee TeachAppoEmployee { get; set; } = default!;

        public EditModel(TeachAppoEmployeeService service)
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
                return NotFound("The teach appointment employee record does not exist.");
            }

            TeachAppoEmployee = teachAppoEmployee;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? yr, string? idNo, DateTime? birthday)
        {
            if (yr == null || idNo == null || birthday == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Attempt to update the record
            var result = await _service.UpdateAsync(yr.Value, idNo, birthday.Value, TeachAppoEmployee);

            if (result == null)
            {
                return NotFound("The record you are trying to update no longer exists or has been deleted.");
            }

            return RedirectToPage("Index");
        }
    }
}
