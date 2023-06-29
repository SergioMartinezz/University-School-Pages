using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UniversityPageSergio.DbContext;
using UniversityPageSergio.Models;

namespace UniversityPageSergio.Pages.Enrollment
{
    public class EditEnrollmentModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public EditEnrollmentModel(ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public Models.Enrollment enrollment { get; set; }
        public Models.Course course { get; set; }
        public Models.Student student { get; set; }
        public IList<Models.Course> Courses { get; set; }
        public IList<Models.Student> Students { get; set; }
        public SelectList CoursesList { get; set; }
        public SelectList StudentsList { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            enrollment = await _db.Enrollments.FirstOrDefaultAsync(e => e.EnrollmentId == id);

            if(enrollment == null)
            {
                return NotFound();
            }

            Courses = _db.Courses.ToList();
            Students = _db.Students.ToList();

            var studentVacio = new Models.Student();
            studentVacio.StudentId = 0;
            studentVacio.StudentName = "- Choose a Student -";
            Students.Add(studentVacio);

            var courseVacio = new Models.Course();
            courseVacio.CourseId = 0;
            courseVacio.CourseTitle = "- Choose a Course -";
            Courses.Add(courseVacio);

            Courses = Courses.OrderBy(c => c.CourseTitle).ToList();
            CoursesList = new SelectList(Courses, "CourseId", "CourseTitle");

            Students = Students.OrderBy(s => s.StudentName).ToList();
            StudentsList = new SelectList(Students, "StudentId", "StudentName");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            _db.Attach(enrollment).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) 
            { 
                if (!EnrollmentExists(enrollment.EnrollmentId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToPage("AllEnrollment");
        }

        private bool EnrollmentExists(int id)
        {
            return _db.Enrollments.Any(e => e.EnrollmentId == id);
        }
    }
}
