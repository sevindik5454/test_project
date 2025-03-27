using System.ComponentModel.DataAnnotations;

namespace test_project.Models
{
    public class CustomerDto
    {
        [Required, MaxLength(100)]
        public string FirstName { get; set; } = "";
        [Required, MaxLength(100)]
        public string LastName { get; set; } = "";
        [Required]
        public string Email { get; set; } = "";
        [Required]
        public string Region { get; set; } = "";
    }
}
