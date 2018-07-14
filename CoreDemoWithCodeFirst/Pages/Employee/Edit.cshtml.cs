using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CoreDemoWithCodeFirst.Models;

namespace CoreDemoWithCodeFirst.Pages.Employee
{
    public class EditModel : PageModel
    {
        private readonly CoreDemoWithCodeFirst.Models.NewDbContext _context;

        public EditModel(CoreDemoWithCodeFirst.Models.NewDbContext context)
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

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(MyFirstTables).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MyFirstTablesExists(MyFirstTables.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool MyFirstTablesExists(int id)
        {
            return _context.MyFirstTables.Any(e => e.Id == id);
        }
    }
}
