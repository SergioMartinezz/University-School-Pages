using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using UniversityPageSergio.DbContext;

namespace UniversityPageSergio.Pages.Enrollment
{
    public class DeleteEnrollmentModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public DeleteEnrollmentModel(ApplicationDbContext db)
        {
            _db = db;
        }
        
        [BindProperty]
        public Models.Enrollment enrollment { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            enrollment = await _db.Enrollments.Include(e => e.Course).Include(e => e.Student).FirstOrDefaultAsync(e => e.EnrollmentId == id);

            if (enrollment == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            enrollment = await _db.Enrollments.FindAsync(id);

            if (enrollment != null)
            {
                _db.Enrollments.Remove(enrollment);
                //enrollment.Course.SeatCapacity++;
                await _db.SaveChangesAsync();
            }

            return RedirectToPage("AllEnrollment");
        }
    }
}
