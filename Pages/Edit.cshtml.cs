using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using test_project.Models;
using test_project.Services;

namespace test_project.Pages
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext context;
        private readonly IWebHostEnvironment environment;

        [BindProperty]
        public CustomerDto CustomerDto { get; set; } = new CustomerDto();
        public Customer Customer { get; set; } = new Customer();

        public string errorMessage = "";
        public string successMessage = "";


        public EditModel(IWebHostEnvironment environment, ApplicationDbContext context) 
        {
            this.environment = environment;
            this.context = context;
            
        }
        public void OnGet(int? id)
        {
            if (id == null)
            {
                Response.Redirect("/Admin");
                return;
            }

            var customer = context.Customers.Find(id);
            if (customer == null)
            {
                Response.Redirect("/Admin");
                return;
            }
            CustomerDto.FirstName = customer.FirstName;
            CustomerDto.LastName = customer.LastName;
            CustomerDto.Email = customer.Email;
            CustomerDto.Region = customer.Region;
            

            Customer = customer;

        }
        public void OnPost(int? id)
        { 
            if (id == null)
            {
                Response.Redirect("/Admin");
                return;
            }

            if (!ModelState.IsValid)
            {
                errorMessage = "Please provide all the required fields";
                return;
            }

            var customer = context.Customers.Find(id);
            if (customer == null)
            {
                Response.Redirect("/Admin");
                return;
            }

            // update the product in the database
            customer.FirstName = CustomerDto.FirstName;
            customer.LastName = CustomerDto.LastName;
            customer.Email = CustomerDto.Email;
            customer.Region = CustomerDto.Region;
            
            

            context.SaveChanges();


            Customer = customer;

            successMessage = "Product updated successfully";

            Response.Redirect("/Admin");
        }
    }
}
