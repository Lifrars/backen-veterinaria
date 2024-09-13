using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiVeterinaria.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MascotasController : ControllerBase
{
    private readonly PetContext _context;

    public MascotasController(PetContext context)
    {
        _context = context;
    }

    // GET: api/Mascota
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Mascota>>> GetMascotas()
    {
        return await _context.Mascotas.Include(m => m.Cliente).ToListAsync();
    }

    // GET: api/Mascota/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Mascota>> GetMascota(int id)
    {
        var mascota = await _context.Mascotas.Include(m => m.Cliente).FirstOrDefaultAsync(m => m.Identificacion == id);

        if (mascota == null)
        {
            return NotFound();
        }

        return mascota;
    }

    // POST: api/Mascota
    [HttpPost]
    public async Task<ActionResult<Mascota>> PostMascota(Mascota mascota)
    {
        _context.Mascotas.Add(mascota);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetMascota), new { id = mascota.Identificacion }, mascota);
    }

    // PUT: api/Mascota/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutMascota(int id, Mascota mascota)
    {
        if (id != mascota.Identificacion)
        {
            return BadRequest();
        }

        _context.Entry(mascota).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!MascotaExists(id))
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

    // DELETE: api/Mascota/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMascota(int id)
    {
        var mascota = await _context.Mascotas.FindAsync(id);
        if (mascota == null)
        {
            return NotFound();
        }

        _context.Mascotas.Remove(mascota);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool MascotaExists(int id)
    {
        return _context.Mascotas.Any(e => e.Identificacion == id);
    }
}