using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebServer.Models;

namespace WebServer.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class InstrumentosController : ControllerBase
    {
        private readonly InstrumentoContext _context;
        public static IHostingEnvironment _environment;

        public InstrumentosController(InstrumentoContext context, IHostingEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // GET: api/Instrumentos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Instrumento>>> GetInstrumentos()
        {
            return await _context.Instrumentos.ToListAsync();
        }

        // GET: api/Instrumentos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Instrumento>> GetInstrumento(long id)
        {
            var instrumento = await _context.Instrumentos.FindAsync(id);

            if (instrumento == null)
            {
                return NotFound();
            }

            return instrumento;
        }

        // PUT: api/Instrumentos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInstrumento(long id, Instrumento instrumento)
        {
            if (id != instrumento.Id)
            {
                return BadRequest();
            }

            _context.Entry(instrumento).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InstrumentoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Instrumentos
        [HttpPost]
        public async Task<ActionResult<Instrumento>> PostInstrumento(Instrumento instrumento)
        {
            _context.Instrumentos.Add(instrumento);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInstrumento", new { id = instrumento.Id }, instrumento);
        }

        // DELETE: api/Instrumentos/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Instrumento>> DeleteInstrumento(long id)
        {
            var instrumento = await _context.Instrumentos.FindAsync(id);
            if (instrumento == null)
            {
                return NotFound();
            }

            _context.Instrumentos.Remove(instrumento);
            await _context.SaveChangesAsync();

            return instrumento;
        }

        private bool InstrumentoExists(long id)
        {
            return _context.Instrumentos.Any(e => e.Id == id);
        }

        // POST: /api/Instrumentos/UploadImage/1
        [HttpPost("UploadImage/{id}"), DisableRequestSizeLimit]
        public async Task<string> UploadFile([FromForm] IFormFile image, long id)
        {
            string path = Path.Combine(_environment.ContentRootPath, "Images/" + image.FileName);
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }

            AsignarImagen(image.FileName, id).Wait();
            return image.FileName;

        }

        private async Task<bool> AsignarImagen(string FileName, long id)
        {
            var instrumento = GetInstrumento(id).Result.Value;
            instrumento.imagen = FileName;
            PutInstrumento(id, instrumento);
            return true;
        }

        // GET: /api/Instrumentos/Image/default.jpeg
        [HttpGet("Image/{fileName}")]
        public IActionResult GetImage(string fileName)
        {

            string path = Path.Combine(_environment.ContentRootPath, "Images/" + fileName);
            string defaultPath = Path.Combine(_environment.ContentRootPath, "Images/" + "default.jpg");
            Console.WriteLine(fileName);
            try
            {
                var image = System.IO.File.OpenRead(path);
                return File(image, "image/jpeg");
            }
            catch (Exception)
            {
            }

            var imageDefault = System.IO.File.OpenRead(defaultPath);
            return File(imageDefault, "image/jpeg");
        }

        // GET: /api/Instrumentos/Image/
        [HttpGet("Image/")]
        public IActionResult GetDefault()
        {
            return GetImage("");
        }


    }
}




