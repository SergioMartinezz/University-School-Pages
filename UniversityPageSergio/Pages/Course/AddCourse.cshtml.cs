using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using UniversityPageSergio.DbContext;

namespace UniversityPageSergio.Pages.Course
{
    public class AddCourseModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public AddCourseModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult OnGet()
        {
            Instructors = _db.Instructors.ToList();

            var vacio = new Models.Instructor();
            vacio.InstructorId = 0;
            vacio.InstructorName = "- Elija Instructor -";
            Instructors.Add(vacio);

            Instructors = Instructors.OrderBy(i => i.InstructorName).ToList();
            InstructorList = new SelectList(Instructors, "InstructorId", "InstructorName");

            return Page();
        }

        [BindProperty]
        public Models.Course course { get; set; }
        public Models.Instructor instructor { get; set; }
        public IList<Models.Instructor> Instructors { get; set; }
        public SelectList InstructorList { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}
            instructor = await _db.Instructors.FindAsync(course.Instructor.InstructorId);
            course.Instructor = instructor;

            _db.Courses.Add(course);
            await _db.SaveChangesAsync();

            return RedirectToPage("AllCourse");
        }

        public async Task<IActionResult> OnGetRecibirNombre(string dato)
        {
            return new JsonResult("Hello " + dato);
        }
    }
}
