using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UniversityPageSergio.DbContext;

namespace UniversityPageSergio.Pages.Instructor
{
    public class EditInstructorModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public EditInstructorModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public Models.Instructor Instructor { get; set; }

        public void OnGet(int id)
        {
            Instructor = _db.Instructors.Find(id);
        }

        public IActionResult OnPost(Models.Instructor instructor)
        {
            _db.Update(instructor);
            _db.SaveChanges();
            return RedirectToPage("AllInstructor");
        }
    }
}
