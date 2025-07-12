using CaloriasApi.Data;
using CaloriasApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CaloriasApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlimentosController : ControllerBase
    {   
        private readonly AppDbContext _context;

        public AlimentosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/alimentos?usuarioId=5
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Alimento>>> GetAlimentos(int? usuarioId)
        {
            var alimentos = await _context.Alimentos
                .Where(a => a.EsGlobal || a.UsuarioId == usuarioId)
                .ToListAsync();

            return Ok(alimentos);
        }


        [HttpPost]
        public async Task<ActionResult<Alimento>> PostAlimento(Alimento alimento)
        {
            _context.Alimentos.Add(alimento);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAlimento), new { id = alimento.Id }, alimento);
        }

       
        [HttpGet("{id}")]
        public async Task<ActionResult<Alimento>> GetAlimento(int id)
        {
            var alimento = await _context.Alimentos.FindAsync(id);

            if (alimento == null)
                return NotFound();

            return alimento;
        }

       
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAlimento(int id)
        {
            var alimento = await _context.Alimentos.FindAsync(id);
            if (alimento == null)
                return NotFound();

            _context.Alimentos.Remove(alimento);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}