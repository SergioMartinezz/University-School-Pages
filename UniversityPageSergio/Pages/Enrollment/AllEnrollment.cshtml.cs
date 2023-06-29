using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using UniversityPageSergio.DbContext;

namespace UniversityPageSergio.Pages.Enrollment
{
    public class AllEnrollmentModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public AllEnrollmentModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public IList<Models.Enrollment> Enrollments { get; set; }

        public async Task OnGetAsync()
        {
            Enrollments = await _db.Enrollments.Include(c => c.Student).Include(c => c.Course).ToListAsync();
        }
    }
}
