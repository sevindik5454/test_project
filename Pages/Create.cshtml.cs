using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using test_project.Models;
using test_project.Services;

namespace test_project.Pages
{
    public class CreateModel : PageModel
    {
        private readonly IWebHostEnvironment environment;
        private readonly ApplicationDbContext context;

        [BindProperty]
        public CustomerDto CustomerDto { get; set; } = new CustomerDto();

        public CreateModel(IWebHostEnvironment environment, ApplicationDbContext context)
        {
            this.environment = environment;
            this.context = context;
        }
        public void OnGet()
        {
        }

        public string errorMessage = "";
        public string succesMessage = "";
        public IActionResult OnPost() // IActionResult döndürülmeli.
        {
            if (!ModelState.IsValid)
            {
                errorMessage = "Please provide all the required fields";
                return Page(); // Hata durumunda sayfayý tekrar göster.
            }
            // save the new product in the database

            Customer customer = new Customer()
            {
                FirstName = CustomerDto.FirstName,
                LastName = CustomerDto.LastName,
                Email = CustomerDto.Email,
                Region = CustomerDto.Region,
                CreatedAt = DateTime.UtcNow,
            };

            context.Customers.Add(customer);
            context.SaveChanges();

            // clear the form
            CustomerDto.FirstName = "";
            CustomerDto.LastName = "";
            CustomerDto.Email = "";
            CustomerDto.Region = "";

            ModelState.Clear();

            succesMessage = "Product created successfully";

            return RedirectToPage("/Admin"); // Admin sayfasýna yönlendir.
        }
    }
}