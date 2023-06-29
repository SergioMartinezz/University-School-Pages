namespace UniversityPageSergio.Models
{
    public class Enrollment
    {
        public int EnrollmentId { get; set; }
        public Student Student { get; set; }
        public Course Course { get; set; }
    }
}
