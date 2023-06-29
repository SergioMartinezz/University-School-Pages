using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UniversityPageSergio.DbContext;

namespace UniversityPageSergio.Pages.Student
{
    public class AllStudentModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public AllStudentModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public IEnumerable<Models.Student> Students { get; set; }

        public void OnGet()
        {
            Students = _db.Students;
        }
    }
}
