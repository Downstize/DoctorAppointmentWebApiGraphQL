using DoctorAppointmentWebApi.DTOs;
using DoctorAppointmentWebApi.DTOs.InputDTO;
using DoctorAppointmentWebApi.Models;
using HotChocolate;
using HotChocolate.Types;

namespace DoctorAppointmentWebApi.Mutations;

[ExtendObjectType("Mutation")]
public class SpecializationMutation
{
    private readonly ApplicationDbContext _context;

    public SpecializationMutation([Service] ApplicationDbContext context)
    {
        _context = context;
    }

    [GraphQLName("createSpecialization")]
    public async Task<Specialization> AddSpecialization(SpecializationInputDto specializationDto)
    {
        var specialization = new Specialization
        {
            SpecializationName = specializationDto.SpecializationName
        };

        _context.Specializations.Add(specialization);
        await _context.SaveChangesAsync();

        return specialization;
    }

    [GraphQLName("deleteSpecialization")]
    public async Task<Specialization> DeleteSpecialization(Guid id)
    {
        var specialization = await _context.Specializations.FindAsync(id);
        if (specialization != null)
        {
            _context.Specializations.Remove(specialization);
            await _context.SaveChangesAsync();
        }

        return specialization;
    }
    
    [GraphQLName("updateSpecialization")]
    public async Task<Specialization> UpdateSpecialization(Guid id, SpecializationInputDto updatedSpecializationDto)
    {
        var specialization = await _context.Specializations.FindAsync(id);
        if (specialization == null)
        {
            return null;
        }
        
        specialization.SpecializationName = updatedSpecializationDto.SpecializationName;
        
        await _context.SaveChangesAsync();

        return specialization;
    }
}
