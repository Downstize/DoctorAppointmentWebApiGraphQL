using DoctorAppointmentWebApi.DTOs;
using DoctorAppointmentWebApi.Models;

namespace DoctorAppointmentWebApi.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class SpecializationController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public SpecializationController(ApplicationDbContext context)
    {
        _context = context;
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<SpecializationDto>>> GetSpecializations()
    {
        var specializations = await _context.Specializations.ToListAsync();

        var specializationsDtos = specializations.Select(specialization => new SpecializationDto
        {
            SpecializationId = specialization.SpecializationId,
            SpecializationName = specialization.SpecializationName
            
        }).ToList();

        foreach (var specializationsDto in specializationsDtos)
        {
            specializationsDto.Links.Add(new Link(
                href: Url.Action(nameof(GetSpecializationById), new { id = specializationsDto.SpecializationId }),
                rel: "self",
                method: "GET"));
        
            specializationsDto.Links.Add(new Link(
                href: Url.Action(nameof(UpdateSpecialization), new { id = specializationsDto.SpecializationId }),
                rel: "update",
                method: "PUT"));
        
            specializationsDto.Links.Add(new Link(
                href: Url.Action(nameof(DeleteSpecialization), new { id = specializationsDto.SpecializationId }),
                rel: "delete",
                method: "DELETE"));
        }

        return Ok(specializationsDtos);
    }
    
    [HttpPost("Create a new specialization", Name = nameof(CreateSpecialization))]
    public async Task<ActionResult<SpecializationDto>> CreateSpecialization(SpecializationDto specializationDto)
    {
        var specialization = new Specialization
        {
            SpecializationId = specializationDto.SpecializationId,
            SpecializationName = specializationDto.SpecializationName,
        };

        _context.Specializations.Add(specialization);
        await _context.SaveChangesAsync();

        var createedSpecialization = new SpecializationDto
        {
            SpecializationId = specialization.SpecializationId,
            SpecializationName = specialization.SpecializationName
        };
        
        createedSpecialization.Links.Add(new Link(
            href: Url.Action(nameof(GetSpecializationById), new { id = createedSpecialization.SpecializationId }),
            rel: "self",
            method: "GET"));

        createedSpecialization.Links.Add(new Link(
            href: Url.Action(nameof(UpdateSpecialization), new { id = createedSpecialization.SpecializationId }),
            rel: "update",
            method: "PUT"));

        createedSpecialization.Links.Add(new Link(
            href: Url.Action(nameof(DeleteSpecialization), new { id = createedSpecialization.SpecializationId }),
            rel: "delete",
            method: "DELETE"));

        return Ok(createedSpecialization);
    }
    
    [HttpGet("{id}", Name = nameof(GetSpecializationById))]
    public async Task<ActionResult<SpecializationDto>> GetSpecializationById(int id)
    {
        var specialization = await _context.Specializations.FindAsync(id);

        if (specialization == null)
        {
            return NotFound();
        }

        var specializationDto = new SpecializationDto
        {
            SpecializationId = specialization.SpecializationId,
            SpecializationName = specialization.SpecializationName
        };
        
        specializationDto.Links.Add(new Link(
            href: Url.Action(nameof(GetSpecializationById), new { id = specializationDto.SpecializationId }),
            rel: "self",
            method: "GET"));

        specializationDto.Links.Add(new Link(
            href: Url.Action(nameof(UpdateSpecialization), new { id = specializationDto.SpecializationId }),
            rel: "update",
            method: "PUT"));

        specializationDto.Links.Add(new Link(
            href: Url.Action(nameof(DeleteSpecialization), new { id = specializationDto.SpecializationId }),
            rel: "delete",
            method: "DELETE"));

        return Ok(specializationDto);
    }
    
    [HttpPut("{id}", Name = nameof(UpdateSpecialization))]
    public async Task<IActionResult> UpdateSpecialization(int id, SpecializationDto specializationDto)
    {
        if (id.ToString() != specializationDto.SpecializationId.ToString())
        {
            return BadRequest();
        }

        var specialization = await _context.Specializations.FindAsync(id);
        if (specialization == null)
        {
            return NotFound();
        }
        
        specialization.SpecializationName = specializationDto.SpecializationName;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!SpecializationExists(id))
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
    
    
    [HttpDelete("{id}", Name = nameof(DeleteSpecialization))]
    public async Task<IActionResult> DeleteSpecialization(int id)
    {
        var specialization = await _context.Specializations.FindAsync(id);
        if (specialization == null)
        {
            return NotFound();
        }

        _context.Specializations.Remove(specialization);
        await _context.SaveChangesAsync();

        return NoContent();
    }
    
    
    private bool SpecializationExists(int id)
    {
        return _context.Specializations.Any(e => e.SpecializationId.ToString() == id.ToString());
    }
    
}