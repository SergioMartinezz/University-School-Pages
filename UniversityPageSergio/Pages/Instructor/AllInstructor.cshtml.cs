using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UniversityPageSergio.DbContext;

namespace UniversityPageSergio.Pages.Instructor
{
    public class AllInstructorModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public AllInstructorModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public IEnumerable<Models.Instructor> Instructors { get; set; }

        public void OnGet()
        {
            Instructors = _db.Instructors;
        }
    }
}
