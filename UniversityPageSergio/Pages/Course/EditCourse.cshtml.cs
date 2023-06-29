using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UniversityPageSergio.DbContext;

namespace UniversityPageSergio.Pages.Course
{
    public class EditCourseModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public EditCourseModel(ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public Models.Course course { get; set; }
        public Models.Instructor instructor { get; set; }
        public IList<Models.Instructor> Instructors { get; set; }
        public SelectList InstructorList { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            course = await _db.Courses.FirstOrDefaultAsync(c => c.CourseId == id);
            if(course == null)
            {
                return NotFound();
            }
            Instructors = _db.Instructors.ToList();

            var vacio = new Models.Instructor();
            vacio.InstructorId = 0;
            vacio.InstructorName = "- Choose a Instructor -";
            Instructors.Add(vacio);

            Instructors = Instructors.OrderBy(i => i.InstructorId).ToList();
            InstructorList = new SelectList(Instructors, "InstructorId", "InstructorName");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            _db.Attach(course).State = EntityState.Modified;
            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourseExists(course.CourseId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToPage("AllCourse");
        }

        private bool CourseExists(int id)
        {
            return _db.Courses.Any(e => e.CourseId == id);
        }
    }
}
