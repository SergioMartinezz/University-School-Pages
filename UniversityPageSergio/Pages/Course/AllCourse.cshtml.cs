using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using UniversityPageSergio.DbContext;

namespace UniversityPageSergio.Pages.Course
{
    public class AllCourseModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public AllCourseModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public IList<Models.Course> Courses { get; set; }

        public async Task OnGetAsync()
        {
            Courses = await _db.Courses.Include(c => c.Instructor).ToListAsync();
        }
    }
}
