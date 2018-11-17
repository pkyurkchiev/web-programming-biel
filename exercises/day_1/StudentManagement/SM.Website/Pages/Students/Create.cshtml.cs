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

namespace SM.Website.Pages.Students
{
    public class CreateModel : PageModel
    {
        private readonly SM.Website.Data.ApplicationDbContext _context;

        public CreateModel(SM.Website.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            Specialtys = new SelectList(await _context.Specialtys.ToListAsync(), "Id", "Name");
            return Page();
        }

        [BindProperty]
        public Student Student { get; set; }
        public SelectList Specialtys { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Students.Add(Student);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}