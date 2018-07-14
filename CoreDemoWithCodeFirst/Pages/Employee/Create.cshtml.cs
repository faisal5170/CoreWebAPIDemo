using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CoreDemoWithCodeFirst.Models;

namespace CoreDemoWithCodeFirst.Pages.Employee
{
    public class CreateModel : PageModel
    {
        private readonly CoreDemoWithCodeFirst.Models.NewDbContext _context;

        public CreateModel(CoreDemoWithCodeFirst.Models.NewDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public MyFirstTables MyFirstTables { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.MyFirstTables.Add(MyFirstTables);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}