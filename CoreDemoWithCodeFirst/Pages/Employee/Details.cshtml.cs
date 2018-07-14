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
    public class DetailsModel : PageModel
    {
        private readonly CoreDemoWithCodeFirst.Models.NewDbContext _context;

        public DetailsModel(CoreDemoWithCodeFirst.Models.NewDbContext context)
        {
            _context = context;
        }

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
    }
}
