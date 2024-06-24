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
    public class IndexModel : PageModel
    {
        private readonly wuchmiITHome.Data.wuchmiITHomeContext _context;

        public IndexModel(wuchmiITHome.Data.wuchmiITHomeContext context)
        {
            _context = context;
        }

        public IList<Article> Article { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }

        public async Task OnGetAsync()
        {
            var articles = from a in _context.Article select a;
            if (!string.IsNullOrEmpty(SearchString))
            {
                articles = articles.Where(s => s.Title.Contains(SearchString));
            }

            Article = await articles.ToListAsync();
            // Article = await _context.Article.ToListAsync();
        }
    }
}
