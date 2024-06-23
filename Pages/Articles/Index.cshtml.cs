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

        public IList<Article> Article { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Article = await _context.Article.ToListAsync();
        }
    }
}
