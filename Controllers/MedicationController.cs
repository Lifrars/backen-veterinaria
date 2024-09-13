using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiVeterinaria.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MedicamentosController : ControllerBase
{
    private readonly PetContext _context;

    public MedicamentosController(PetContext context)
    {
        _context = context;
    }

    // GET: api/Medicamento
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Medicamento>>> GetMedicamentos()
    {
        return await _context.Medicamentos.ToListAsync();
    }

    // GET: api/Medicamento/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Medicamento>> GetMedicamento(int id)
    {
        var medicamento = await _context.Medicamentos.FirstOrDefaultAsync(m => m.Id == id);

        if (medicamento == null)
        {
            return NotFound();
        }

        return medicamento;
    }

    // POST: api/Medicamento
    [HttpPost]
    public async Task<ActionResult<Medicamento>> PostMedicamento(Medicamento medicamento)
    {
        _context.Medicamentos.Add(medicamento);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetMedicamento), new { id = medicamento.Id }, medicamento);
    }

    // PUT: api/Medicamento/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutMedicamento(int id, Medicamento medicamento)
    {
        if (id != medicamento.Id)
        {
            return BadRequest();
        }

        _context.Entry(medicamento).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!MedicamentoExists(id))
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

    // DELETE: api/Medicamento/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMedicamento(int id)
    {
        var medicamento = await _context.Medicamentos.FindAsync(id);
        if (medicamento == null)
        {
            return NotFound();
        }

        _context.Medicamentos.Remove(medicamento);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool MedicamentoExists(int id)
    {
        return _context.Medicamentos.Any(e => e.Id == id);
    }
}