using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CoreDemoWithCodeFirst.Models;

namespace CoreDemoWithCodeFirst.Pages.Employee
{
    public class IndexModel : PageModel
    {
        private readonly CoreDemoWithCodeFirst.Models.NewDbContext _context;

        public IndexModel(CoreDemoWithCodeFirst.Models.NewDbContext context)
        {
            _context = context;
        }

        public PaginatedList<MyFirstTables> MyFirstTables { get; set; }

        public async Task OnGetAsync(string sortOrder, string currentFilter, string searchString, int? pageIndex)
        {
            CurrentSort = sortOrder;
            NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            DateSort = sortOrder == "Date" ? "date_desc" : "Date";
            if (searchString != null)
                pageIndex = 1;
            else
                searchString = currentFilter;
            CurrentFilter = searchString;

            IQueryable<MyFirstTables> emp = from s in _context.MyFirstTables
                                            select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                emp = emp.Where(s => s.Name.ToLower().Contains(searchString.ToLower())
                                       || s.Country.ToLower().Contains(searchString.ToLower()));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    emp = emp.OrderByDescending(s => s.Name);
                    break;
                case "Date":
                    emp = emp.OrderBy(s => s.LastModified);
                    break;
                case "date_desc":
                    emp = emp.OrderByDescending(s => s.LastModified);
                    break;
                default:
                    emp = emp.OrderBy(s => s.Name);
                    break;
            }
            int pageSize = 1;
            MyFirstTables = await PaginatedList<MyFirstTables>.CreateAsync(
        emp.AsNoTracking(), pageIndex ?? 1, pageSize);
        }

        public string NameSort { get; set; }
        public string DateSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }
    }
}
