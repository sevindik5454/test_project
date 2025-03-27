using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using test_project.Services;

namespace test_project.Pages
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext context;
        private readonly IWebHostEnvironment environment;
        public DeleteModel(IWebHostEnvironment environment, ApplicationDbContext context)
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

            context.Customers.Remove(customer);
            context.SaveChanges();

            Response.Redirect("/Admin");

        }
    }
}
