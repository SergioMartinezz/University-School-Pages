using System.ComponentModel.DataAnnotations;

namespace UniversityPageSergio.Models
{
    public class Course
    {
        public int CourseId { get; set; }
        [Required, StringLength(50, MinimumLength = 7)]
        public string CourseTitle { get; set; }
        public int SeatCapacity { get; set; }
        public Instructor Instructor { get; set; }
    }
}
