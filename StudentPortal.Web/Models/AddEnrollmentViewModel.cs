using StudentPortal.Web.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace StudentPortal.Web.Models
{
    public class AddEnrollmentViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The Course field is required.")]
        public int? CourseId { get; set; }

        [Required(ErrorMessage = "The Student field is required.")]
        public int? StudentId { get; set; }

        [Required]
        public Grade? Grade { get; set; }

        public Course? Course { get; set; }

        public Student? Student { get; set; }
    }
}
