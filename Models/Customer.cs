using System.ComponentModel.DataAnnotations;

namespace test_project.Models
{
    public class Customer
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string FirstName { get; set; } = "";
        [MaxLength(100)]
        public string LastName { get; set; } = "";
        
        public string Email { get; set; } = "";

        public string Region { get; set; } = "";

        public DateTime CreatedAt { get; set; }



    }
}
