using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SM.Website.Data;
using SM.Website.Data.Entities;

namespace SM.Website.Pages.Specialtys
{
    public class EditModel : PageModel
    {
        private readonly SM.Website.Data.ApplicationDbContext _context;

        public EditModel(SM.Website.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Specialty).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SpecialtyExists(Specialty.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool SpecialtyExists(int id)
        {
            return _context.Specialtys.Any(e => e.Id == id);
        }
    }
}
