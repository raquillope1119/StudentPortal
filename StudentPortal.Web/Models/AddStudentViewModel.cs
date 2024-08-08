using System.ComponentModel.DataAnnotations;

namespace StudentPortal.Web.Models
{
    public class AddStudentViewModel
    {

        [Required]
        [StringLength(50)]
        public required string Name { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string? Email { get; set; }

        [Required]
        [StringLength(11, MinimumLength = 11)]
        public string? Phone { get; set; }
        public bool Subscribed { get; set; }
    }
}
