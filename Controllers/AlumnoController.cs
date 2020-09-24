using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlumnosWebApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AlumnosWebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlumnoController : ControllerBase
    {
        private readonly AlumnoContext _context;

        public AlumnoController(AlumnoContext context)
        {
            _context = context;

            if (_context.Alumnos.Count() == 0)
            {
                _context.Alumnos.Add(new Alumno { Name = "Miguel" });
                _context.SaveChanges();
            }
        }

        //GET: api/Alumno
        [HttpGet]

        public async Task<ActionResult<IEnumerable<Alumno>>> GetAlumnos()
        {
            return await _context.Alumnos.ToListAsync();
        }

        //GET: api/Alumno/1
        [HttpGet("{id}")]

        public async Task<ActionResult<Alumno>> GetAlumno(long id)
        {
            var alumno = await _context.Alumnos.FirstOrDefaultAsync(q => q.Id == id);

            if (alumno == null)
            {
                return NotFound();
            }

            return alumno;
        }
        
        //POST: api/Alumno
        [HttpPost]

        public async Task<ActionResult<Alumno>> PostAlumno (Alumno item)
        {
            _context.Alumnos.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAlumno), new { id = item.Id }, item);
        }

        //PUT: api/Alumno
        [HttpPut("{id}")]

        public async Task<IActionResult> PutAlumno(long id, Alumno item)
        {
            if(id != item.Id)
            {
                return BadRequest();
            }

            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        //DELETE: api/Alumno
        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteAlumno(long id)
        {
            var alumno = await _context.Alumnos.FindAsync(id);

            if(alumno == null)
            {
                return NotFound();
            }

            _context.Alumnos.Remove(alumno);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
