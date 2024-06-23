using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace wuchmiITHome.Data
{
    public class wuchmiITHomeContext: DbContext
    {
        public wuchmiITHomeContext (
            DbContextOptions<wuchmiITHomeContext> options)
            : base(options)
        {
        }

        public DbSet<wuchmiITHome.Models.Article> Article { get; set; }
    }
}