using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreDemoWithCodeFirst.Models;
using Microsoft.AspNetCore.Mvc;

namespace CoreDemoWithCodeFirst.Controllers
{
    public class WebEmployeeController : Controller
    {
        private readonly DemoDBContext db = new DemoDBContext();
        public async Task<IActionResult> Index()
        {
            var list = db.MyFirstTables.ToList();
            return View(list);
        }

        public IActionResult About()
        {
            return View();
        }
    }
}