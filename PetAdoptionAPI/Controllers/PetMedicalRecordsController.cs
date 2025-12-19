using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class PetMedicalRecordsController : ControllerBase
{
    private readonly PetAdoptionContext _context;

    public PetMedicalRecordsController(PetAdoptionContext context)
    {
        _context = context;
    }

    // ✅ GET ALL
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PetMedicalRecordDto>>> GetAll()
    {
        var records = await _context.PetMedicalRecords
            .Select(m => new PetMedicalRecordDto
            {
                MedicalRecordId = m.MedicalRecordId,
                VaccinationDetails = m.VaccinationDetails,
                HealthHistory = m.HealthHistory,
                ActiveFlag = m.ActiveFlag,
                PetId = m.PetId
            })
            .ToListAsync();

        return Ok(records);
    }

    // ✅ GET BY ID
    [HttpGet("{id}")]
    public async Task<ActionResult<PetMedicalRecordDto>> GetById(int id)
    {
        var record = await _context.PetMedicalRecords
            .Where(m => m.MedicalRecordId == id)
            .Select(m => new PetMedicalRecordDto
            {
                MedicalRecordId = m.MedicalRecordId,
                VaccinationDetails = m.VaccinationDetails,
                HealthHistory = m.HealthHistory,
                ActiveFlag = m.ActiveFlag,
                PetId = m.PetId
            })
            .FirstOrDefaultAsync();

        if (record == null)
            return NotFound();

        return Ok(record);
    }

    // ✅ POST (CREATE)
    [HttpPost]
    public async Task<ActionResult<PetMedicalRecordDto>> Create(PetMedicalRecordDto dto)
    {
        var record = new PetMedicalRecords
        {
            VaccinationDetails = dto.VaccinationDetails,
            HealthHistory = dto.HealthHistory,
            ActiveFlag = dto.ActiveFlag,
            PetId = dto.PetId
        };

        _context.PetMedicalRecords.Add(record);
        await _context.SaveChangesAsync();

        dto.MedicalRecordId = record.MedicalRecordId;

        return CreatedAtAction(nameof(GetById), new { id = record.MedicalRecordId }, dto);
    }

    // ✅ PUT (UPDATE)
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, PetMedicalRecordDto dto)
    {
        if (id != dto.MedicalRecordId)
            return BadRequest();

        var record = await _context.PetMedicalRecords.FindAsync(id);
        if (record == null)
            return NotFound();

        record.VaccinationDetails = dto.VaccinationDetails;
        record.HealthHistory = dto.HealthHistory;
        record.ActiveFlag = dto.ActiveFlag;
        record.PetId = dto.PetId;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    // ✅ DELETE
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var record = await _context.PetMedicalRecords.FindAsync(id);
        if (record == null)
            return NotFound();

        _context.PetMedicalRecords.Remove(record);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
public class PetMedicalRecordDto
{
    public int MedicalRecordId { get; set; }
    public string VaccinationDetails { get; set; }
    public string HealthHistory { get; set; }
    public bool ActiveFlag { get; set; }
    public int PetId { get; set; }
}

