using DoctorAppointmentWebApi.Models;
using DoctorAppointmentWebApi.DTOs;
using Microsoft.EntityFrameworkCore;

namespace DoctorAppointmentWebApi.Queries;

[ExtendObjectType(Name = "Query")]
public class SpecializationQuery
{
    private readonly ApplicationDbContext _context;

    public SpecializationQuery([Service] ApplicationDbContext context)
    {
        _context = context;
    }
    
    [GraphQLName("allSpecializations")]
    public List<SpecializationDto> GetAllSpecializations() =>
        _context.Specializations
            .Select(s => new SpecializationDto
            {
                SpecializationId = s.SpecializationId,
                SpecializationName = s.SpecializationName,
            })
            .ToList();
    
    [GraphQLName("specializationById")]
    public SpecializationDto GetSpecializationById(Guid id) =>
        _context.Specializations
            .Where(s => s.SpecializationId == id)
            .Select(s => new SpecializationDto
            {
                SpecializationId = s.SpecializationId,
                SpecializationName = s.SpecializationName
            })
            .FirstOrDefault()!;
}