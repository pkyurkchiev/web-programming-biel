using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SM.Website.Data.Entities;
using System.Threading.Tasks;

namespace SM.Website.Pages.Specialtys
{
    [Authorize]
    public class DeleteModel : PageModel
    {
        private readonly SM.Website.Data.ApplicationDbContext _context;

        public DeleteModel(SM.Website.Data.ApplicationDbContext context)
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Specialty = await _context.Specialtys.FindAsync(id);

            if (Specialty != null)
            {
                _context.Specialtys.Remove(Specialty);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
