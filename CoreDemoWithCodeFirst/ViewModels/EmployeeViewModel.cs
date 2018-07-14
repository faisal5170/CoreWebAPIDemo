using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CoreDemoWithCodeFirst.ViewModels
{
    public class EmployeeViewModel : PageModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public IFormFile Profile { get; set; }
        public string ProfilePath { get; set; }
        //public string Profile { get; set; }
        public string Country { get; set; }
        public DateTime? LastModified { get; set; }
        public List<SelectListItem> CountryList { get; set; }
        public string NameSort { get; set; }
        public string DateSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }
    }
}
