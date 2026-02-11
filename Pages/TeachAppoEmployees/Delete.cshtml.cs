using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using wuchmiITHome.Models;
using wuchmiITHome.Services;

namespace wuchmiITHome.Pages.TeachAppoEmployees
{
    /// <summary>
    /// Delete page model for removing teach appointment employee records.
    /// Shows confirmation with record details before deletion and handles errors if record already deleted.
    /// </summary>
    public class DeleteModel : PageModel
    {
        private readonly TeachAppoEmployeeService _service;

        public TeachAppoEmployee TeachAppoEmployee { get; set; } = default!;

        public DeleteModel(TeachAppoEmployeeService service)
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
                return NotFound("The teach appointment employee record cannot be found. " +
                               "It may have been deleted already.");
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

            var result = await _service.DeleteAsync(yr.Value, idNo, birthday.Value);

            if (!result)
            {
                return NotFound("Cannot delete: the record you are trying to delete no longer exists. " +
                               "It may have been deleted already by another user.");
            }

            return RedirectToPage("Index");
        }
    }
}
