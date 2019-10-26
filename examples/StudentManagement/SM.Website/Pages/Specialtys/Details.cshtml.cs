using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SM.Website.Data;
using SM.Website.Data.Entities;

namespace SM.Website.Pages.Specialtys
{
    [Authorize]
    public class DetailsModel : PageModel
    {
        private readonly SM.Website.Data.ApplicationDbContext _context;

        public DetailsModel(SM.Website.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Specialty Specialty { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Specialty = await _context.Specialtys.FirstOrDefaultAsync(m => m.Id == id);

            if (Specialty == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
