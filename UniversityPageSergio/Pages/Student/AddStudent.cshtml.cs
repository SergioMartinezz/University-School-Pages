using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UniversityPageSergio.DbContext;

namespace UniversityPageSergio.Pages.Student
{
    public class AddStudentModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public AddStudentModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public Models.Student Student { get; set; }

        public void OnGet()
        {
            Student = new Models.Student();
        }

        public IActionResult OnPost(Models.Student student)
        {
            _db.Add(student);
            _db.SaveChanges();
            return RedirectToPage("AllStudent");
        }
    }
}
