using CaloriasApi.Data;
using CaloriasApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CaloriasApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegistrosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public RegistrosController(AppDbContext context)
        {
            _context = context;
        }

        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Registro>>> GetRegistros(int usuarioId, DateTime? fecha)
        {
            var query = _context.Registros
                .Include(r => r.Alimento)
                .Where(r => r.UsuarioId == usuarioId);

            if (fecha.HasValue)
            {
                var dia = fecha.Value.Date;
                query = query.Where(r => r.Fecha.Date == dia);
            }

            var registros = await query.ToListAsync();
            return Ok(registros);
        }

        
        [HttpPost]
        public async Task<ActionResult<Registro>> PostRegistro(Registro registro)
        {
            registro.Fecha = DateTime.Now;

            _context.Registros.Add(registro);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRegistro), new { id = registro.Id }, registro);
        }

        
        [HttpGet("{id}")]
        public async Task<ActionResult<Registro>> GetRegistro(int id)
        {
            var registro = await _context.Registros
                .Include(r => r.Alimento)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (registro == null)
                return NotFound();

            return registro;
        }

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRegistro(int id)
        {
            var registro = await _context.Registros.FindAsync(id);
            if (registro == null)
                return NotFound();

            _context.Registros.Remove(registro);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
