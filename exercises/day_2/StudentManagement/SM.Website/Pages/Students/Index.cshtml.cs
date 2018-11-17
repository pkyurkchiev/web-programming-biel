using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SM.Website.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SM.Website.Pages.Students
{
    public class IndexModel : PageModel
    {
        private readonly SM.Website.Data.ApplicationDbContext _context;

        public IndexModel(SM.Website.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Student> Student { get;set; }
        public string StudentName { get; set; }
        public SelectList Specialties { get; set; }
        public string StudentSpecialty { get; set; }

        public async Task OnGetAsync(string studentName, int? studentSpecialty)
        {
            var students = from s in _context.Students
                         select s;

            if (!String.IsNullOrEmpty(studentName))
                students = students.Where(s => s.FirstName.Contains(studentName));

            if (studentSpecialty.HasValue)
                students = students.Where(s => s.SpecialtyId == studentSpecialty.Value);

            Student = await students.Include(x => x.Specialty).ToListAsync();
            StudentName = studentName;
            Specialties = new SelectList(await _context.Specialtys.ToListAsync(), "Id", "Name");
        }
    }
}
