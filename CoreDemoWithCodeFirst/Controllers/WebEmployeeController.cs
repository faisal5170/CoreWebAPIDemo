using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CoreDemoWithCodeFirst.Models;
using CoreDemoWithCodeFirst.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CoreDemoWithCodeFirst.Controllers
{
    public class WebEmployeeController : Controller
    {
        private readonly DemoDBContext db = new DemoDBContext();
        public IActionResult Index()
        {
            List<EmployeeViewModel> list = db.MyFirstTables.ToList().Select(f => new EmployeeViewModel
            {
                Id = f.Id,
                Age = f.Age,
                Name = f.Name,
                ProfilePath = f.Profile,
                Country = f.Country,
                LastModified = f.LastModified
            }).ToList();
            return View(list);
        }

        public IActionResult About()
        {
            return View();
        }

        public async Task<IActionResult> AddEditEmployee(int? id)
        {
            EmployeeViewModel model = new EmployeeViewModel
            {
                CountryList = new List<SelectListItem>()
            };
            model.CountryList = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = "1 Day",
                    Value = "Days_1"
                },
                new SelectListItem
                {
                    Text = "3 Day",
                    Value = "Days_3"
                },
                new SelectListItem
                {
                    Text = "5 Day",
                    Value = "Days_5"
                },
                new SelectListItem
                {
                    Text = "7 Day",
                    Value = "Days_7"
                },
                new SelectListItem
                {
                    Text = "10 Day",
                    Value = "Days_10"
                },
                new SelectListItem
                {
                    Text = "14 Day",
                    Value = "Days_14"
                },
                new SelectListItem
                {
                    Text = "28 Day",
                    Value = "Days_28"
                },
                new SelectListItem
                {
                    Text = "30 Day",
                    Value = "Days_30"
                },
                new SelectListItem
                {
                    Text = "60 Day",
                    Value = "Days_60"
                },
                new SelectListItem
                {
                    Text = "Good Till Canceled (GTC)",
                    Value = "Days_GTC"
                }
            };
            if (id.HasValue)
            {
                var emp = await db.MyFirstTables.FindAsync(id);
                if (emp != null)
                {
                    model.Name = emp.Name;
                    model.Id = emp.Id;
                    model.Age = emp.Age;
                    model.ProfilePath = emp.Profile;
                    model.LastModified = emp.LastModified;
                    model.Country = emp.Country;
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddEditEmployee(EmployeeViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    MyFirstTables emp = new MyFirstTables();
                    bool IsNew = model.Id != 0 ? false : true;
                    if (!IsNew)
                        emp = await db.MyFirstTables.SingleOrDefaultAsync(a => a.Id == model.Id);
                    emp.Name = model.Name;
                    emp.Age = model.Age;
                    var path = "";
                    if (model.Profile.FileName != null)
                    {
                        path = Path.Combine(
                        Directory.GetCurrentDirectory(), "wwwroot\\ProfilePicture", model.Profile.FileName);
                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            await model.Profile.CopyToAsync(stream);
                        }
                    }
                    emp.Profile = model.Profile.FileName != null ? path : emp.Profile;
                    emp.LastModified = model.LastModified;
                    emp.Country = model.Country;
                    if (IsNew)
                        await db.MyFirstTables.AddAsync(emp);
                    await db.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                //return NotFound(ex);
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Download(string filename)
        {
            if (filename == null)
                return Content("filename not present");

            var path = Path.Combine(
                           Directory.GetCurrentDirectory(),
                           "wwwroot\\ProfilePicture", filename);

            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, GetContentType(path), Path.GetFileName(path));
        }

        public async Task<IActionResult> DeleteEmployee(int id)
        {
            try
            {
                var emp = await db.MyFirstTables.SingleOrDefaultAsync(a => a.Id == id);
                if (emp == null) return NotFound();
                db.MyFirstTables.Remove(emp);
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
            return RedirectToAction("Index");
        }

        private string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }

        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                {".xls", "application/vnd.ms-excel"},
                {".xlsx", "application/vnd.openxmlformatsofficedocument.spreadsheetml.sheet"},
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"}
            };
        }
    }
}