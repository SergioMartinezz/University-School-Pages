using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UniversityPageSergio.DbContext;

namespace UniversityPageSergio.Pages.Instructor
{
    public class AddInstructorModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public AddInstructorModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public Models.Instructor Instructor { get; set; }

        public void OnGet()
        {
            Instructor = new Models.Instructor();
        }

        public IActionResult OnPost(Models.Instructor instructor)
        {
            _db.Add(instructor);
            _db.SaveChanges();
            return RedirectToPage("AllInstructor");
        }
    }
}
