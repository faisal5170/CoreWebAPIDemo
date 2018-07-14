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
    public class DeleteModel : PageModel
    {
        private readonly CoreDemoWithCodeFirst.Models.NewDbContext _context;

        public DeleteModel(CoreDemoWithCodeFirst.Models.NewDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public MyFirstTables MyFirstTables { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            MyFirstTables = await _context.MyFirstTables.FirstOrDefaultAsync(m => m.Id == id);

            if (MyFirstTables == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            MyFirstTables = await _context.MyFirstTables.FindAsync(id);

            if (MyFirstTables != null)
            {
                _context.MyFirstTables.Remove(MyFirstTables);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
