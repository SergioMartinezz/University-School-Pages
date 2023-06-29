using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UniversityPageSergio.DbContext;
using UniversityPageSergio.Models;

namespace UniversityPageSergio.Pages.Enrollment
{
    public class AddEnrollmentModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public AddEnrollmentModel(ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public Models.Enrollment enrollment { get; set; }
        public Models.Student student { get; set; }
        public Models.Course course { get; set; }
        public IList<Models.Course> courses { get; set; }
        public IList<Models.Student> students { get; set; }
        public SelectList CoursesList { get; set; }
        public SelectList StudentsList { get; set; }

        public IActionResult OnGet()
        {
            students = _db.Students.ToList();
            courses = _db.Courses.ToList();

            var studentVacio = new Models.Student();
            studentVacio.StudentId = 0;
            studentVacio.StudentName = "- Choose a Student -";
            students.Add(studentVacio);

            var courseVacio = new Models.Course();
            courseVacio.CourseId = 0;
            courseVacio.CourseTitle = "- Choose a Course -";
            courses.Add(courseVacio);

            courses = courses.OrderBy(c => c.CourseTitle).ToList();
            CoursesList = new SelectList(courses, "CourseId", "CourseTitle");

            students = students.OrderBy(s => s.StudentName).ToList();
            StudentsList = new SelectList(students, "StudentId", "StudentName");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            student = await _db.Students.FindAsync(enrollment.Student.StudentId);
            enrollment.Student = student;

            course = await _db.Courses.FindAsync(enrollment.Course.CourseId);
            enrollment.Course = course;

            enrollment.Course.SeatCapacity--;

            _db.Enrollments.Add(enrollment);
            await _db.SaveChangesAsync();

            return RedirectToPage("AllEnrollment");
        }

        public async Task<IActionResult> OnGetCourseBack(int id)
        {
            var course = await _db.Courses.Include(x => x.Instructor).FirstOrDefaultAsync(x => x.CourseId == id);
            return new JsonResult(course);
        }

        public async Task<IActionResult> OnGetCheckEnrollment(int idCourse, int idStudent)
        {
            var course = await _db.Courses.Include(x => x.Instructor).FirstOrDefaultAsync(x => x.CourseId == idCourse);
            var student = await _db.Students.FindAsync(idStudent);

            //var register = await _db.Enrollments.Where(x => x.Student.StudentId == idStudent && x.Course.CourseId == idCourse).SingleOrDefaultAsync();

            var check = await _db.Enrollments.Where(e => e.Student.StudentId == idStudent && e.Course.CourseId == idCourse).FirstOrDefaultAsync();

            if (check == null)
            {
                return new JsonResult(true);
            }
            else
            {
                return new JsonResult(false);
            }
        }
    }
}
