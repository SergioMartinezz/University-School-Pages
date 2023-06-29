using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using UniversityPageSergio.DbContext;

namespace UniversityPageSergio.Pages.Course
{
    public class DeleteCourseModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public DeleteCourseModel(ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public Models.Course course { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            
            course = await _db.Courses.Include(c => c.Instructor).FirstOrDefaultAsync(c => c.CourseId == id);

            if(course == null)
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
            
            course = await _db.Courses.FindAsync(id);

            if(course != null)
            {
                _db.Courses.Remove(course);
                await _db.SaveChangesAsync();
            }
            return RedirectToPage("AllCourse");
        }
    }
}
