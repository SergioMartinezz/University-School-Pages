using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UniversityPageSergio.DbContext;

namespace UniversityPageSergio.Pages.Student
{
    public class EditStudentModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public EditStudentModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public Models.Student Student { get; set; }

        public void OnGet(int id)
        {
            Student = _db.Students.Find(id);
        }

        public IActionResult OnPost(Models.Student student)
        {
            _db.Update(student);
            _db.SaveChanges();
            return RedirectToPage("AllStudent");
        }
    }
}
