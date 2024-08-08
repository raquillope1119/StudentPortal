using StudentPortal.Web.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace StudentPortal.Web.Models
{
    public class AddCourseViewModel
    {
        public int Id { get; set; }

        [Required]
        public string? Title { get; set; }

        [Required]
        public int? Credits { get; set; }

        public ICollection<Enrollment>? Enrollments { get; set; }
    }
}
