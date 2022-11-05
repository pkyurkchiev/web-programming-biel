using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SM.Website.Data;
using SM.Website.Data.Entities;

namespace SM.Website.Pages.Specialtys
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly SM.Website.Data.ApplicationDbContext _context;

        public CreateModel(SM.Website.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Specialty Specialty { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Specialtys.Add(Specialty);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}