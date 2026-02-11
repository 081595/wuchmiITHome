using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using wuchmiITHome.Models;
using wuchmiITHome.Services;

namespace wuchmiITHome.Pages.TeachAppoEmployees
{
    /// <summary>
    /// Create page model for adding a new teach appointment employee record.
    /// Handles validation including composite key uniqueness (duplicate key detection).
    /// </summary>
    public class CreateModel : PageModel
    {
        private readonly TeachAppoEmployeeService _service;

        [BindProperty]
        public TeachAppoEmployee TeachAppoEmployee { get; set; } = default!;

        [TempData]
        public string? ErrorMessage { get; set; }

        public CreateModel(TeachAppoEmployeeService service)
        {
            _service = service;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Check for duplicate composite key
            var existingRecord = await _service.GetByIdAsync(
                TeachAppoEmployee.Yr,
                TeachAppoEmployee.IdNo,
                TeachAppoEmployee.Birthday);

            if (existingRecord != null)
            {
                ModelState.AddModelError("",
                    "A record with the same Year, Identity Number, and Birthday already exists. " +
                    "This combination must be unique.");
                return Page();
            }

            // Attempt to create the record
            var result = await _service.CreateAsync(TeachAppoEmployee);

            if (result == null)
            {
                ModelState.AddModelError("",
                    "Failed to create the record. A duplicate key or database error occurred.");
                return Page();
            }

            return RedirectToPage("Index");
        }
    }
}
