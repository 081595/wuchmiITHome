using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using wuchmiITHome.Data;
using wuchmiITHome.Models;

namespace wuchmiITHome.Pages_Articles
{
    public class DetailsModel : PageModel
    {
        private readonly wuchmiITHome.Data.wuchmiITHomeContext _context;

        public DetailsModel(wuchmiITHome.Data.wuchmiITHomeContext context)
        {
            _context = context;
        }

        public Article Article { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article = await _context.Article.FirstOrDefaultAsync(m => m.ID == id);
            if (article == null)
            {
                return NotFound();
            }
            else
            {
                Article = article;
            }
            return Page();
        }
    }
}
