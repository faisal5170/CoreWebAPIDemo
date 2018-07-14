using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreDemoWithCodeFirst.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreDemoWithCodeFirst.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly DemoDBContext db = new DemoDBContext();

        [Produces("application/json")]
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var list = db.MyFirstTables.ToList();
                return Ok(list);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Produces("application/json")]
        [HttpGet("Get/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                return Ok(db.MyFirstTables.Find(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Produces("application/json")]
        [Consumes("application/json")]
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] MyFirstTables model)
        {
            try
            {
                db.MyFirstTables.Add(model);
                db.SaveChanges();
                return Ok("Success");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Produces("application/json")]
        [Consumes("application/json")]
        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] MyFirstTables model)
        {
            try
            {
                db.Entry(model).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();
                return Ok("Success");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                db.MyFirstTables.Remove(db.MyFirstTables.Find(id));
                db.SaveChanges();
                return Ok("Deleted");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
