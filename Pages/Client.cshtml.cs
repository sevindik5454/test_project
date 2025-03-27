using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace test_project.Pages
{
    [Authorize(Roles = "client, admin")]
    public class ClientModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
