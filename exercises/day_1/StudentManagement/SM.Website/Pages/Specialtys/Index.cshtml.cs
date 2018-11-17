using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SM.Website.Data;
using SM.Website.Data.Entities;

namespace SM.Website.Pages.Specialtys
{
    public class IndexModel : PageModel
    {
        private readonly SM.Website.Data.ApplicationDbContext _context;

        public IndexModel(SM.Website.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Specialty> Specialty { get;set; }

        public async Task OnGetAsync()
        {
            Specialty = await _context.Specialtys.ToListAsync();
        }
    }
}
