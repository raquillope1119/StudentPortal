using System.ComponentModel.DataAnnotations;

namespace StudentPortal.Web.Models.Entities
{
    public class Student
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string? Name { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string? Email { get; set; }

        [Required]
        [StringLength(11, MinimumLength = 11)]
        public string? Phone { get; set; }
        public DateTime? EnrollmentDate { get; set; }
        public bool Subscribed { get; set; }

        public ICollection<Enrollment>? Enrollments { get; set; }
    }
}
