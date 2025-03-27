using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using test_project.Models;
using test_project.Services;
using System.Linq;

namespace test_project.Pages
{
    [Authorize(Roles = "admin")]
    public class AdminModel : PageModel
    {
        private readonly ApplicationDbContext context;

        //pagination functionality
        public int pageIndex = 1;
        public int totalPages = 0;
        private readonly int pageSize = 5;
        //search functionality
        public string search = "";
        public string filterEmail = "";
        public DateTime? filterCreatedAt = null;
        //filter functionality
        public string filterRegion = "";
        public string filterFirstName = "";

        //sort functionality
        public string column = "Id";
        public string orderBy = "desc";

        public List<Customer> Customers { get; set; } = new List<Customer>();

        public AdminModel(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void OnGet(int? pageIndex, string? search, string? column, string? orderBy, string? filterRegion, string? filterFirstName, string? filterEmail, DateTime? filterCreatedAt)
        {
            // Veritabanýndan her zaman güncel verileri çek
            IQueryable<Customer> query = context.Customers;

            // Filtreleme (Region)
            if (!string.IsNullOrEmpty(filterRegion))
            {
                query = query.Where(p => p.Region == filterRegion);
                this.filterRegion = filterRegion; // Filtre deðerini sakla
            }

            // Filtreleme (FirstName)
            if (!string.IsNullOrEmpty(filterFirstName))
            {
                query = query.Where(p => p.FirstName.Contains(filterFirstName));
                this.filterFirstName = filterFirstName; // Filtre deðerini sakla
            }
            // Filtreleme (Email)
            if (!string.IsNullOrEmpty(filterEmail))
            {
                query = query.Where(p => p.Email.Contains(filterEmail));
                this.filterEmail = filterEmail;
            }

            // Filtreleme (CreatedAt)
            if (filterCreatedAt.HasValue)
            {
                // filterCreatedAt deðerini UTC'ye dönüþtür
                DateTime utcCreatedAt = DateTime.SpecifyKind(filterCreatedAt.Value, DateTimeKind.Utc);
                query = query.Where(p => p.CreatedAt.Date == utcCreatedAt.Date);
                this.filterCreatedAt = filterCreatedAt;
            }

            // Arama
            if (search != null)
            {
                this.search = search;
                query = query.Where(p => p.FirstName.Contains(search) || p.LastName.Contains(search) || p.Email.Contains(search) || p.Region.Contains(search));
            }

            // Sýralama
            string[] validColumns = { "Id", "FirstName", "LastName", "Email", "Region", "CreatedAt" };
            string[] validOrderBy = { "desc", "asc" };

            if (!validColumns.Contains(column))
            {
                column = "Id";
            }

            if (!validOrderBy.Contains(orderBy))
            {
                orderBy = "desc";
            }

            this.column = column;
            this.orderBy = orderBy;

            if (column == "FirstName")
            {
                query = orderBy == "asc" ? query.OrderBy(p => p.FirstName) : query.OrderByDescending(p => p.FirstName);
            }
            else if (column == "LastName")
            {
                query = orderBy == "asc" ? query.OrderBy(p => p.LastName) : query.OrderByDescending(p => p.LastName);
            }
            else if (column == "Email")
            {
                query = orderBy == "asc" ? query.OrderBy(p => p.Email) : query.OrderByDescending(p => p.Email);
            }
            else if (column == "Region")
            {
                query = orderBy == "asc" ? query.OrderBy(p => p.Region) : query.OrderByDescending(p => p.Region);
            }
            else if (column == "CreatedAt")
            {
                query = orderBy == "asc" ? query.OrderBy(p => p.CreatedAt) : query.OrderByDescending(p => p.CreatedAt);
            }
            else
            {
                query = orderBy == "asc" ? query.OrderBy(p => p.Id) : query.OrderByDescending(p => p.Id);
            }

            // Sayfalama
            if (pageIndex == null || pageIndex < 1)
            {
                pageIndex = 1;
            }

            this.pageIndex = (int)pageIndex;

            decimal count = query.Count();
            totalPages = (int)Math.Ceiling(count / pageSize);
            query = query.Skip((this.pageIndex - 1) * pageSize).Take(pageSize);

            Customers = query.ToList();
        }
    }
}