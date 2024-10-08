using DoctorAppointmentWebApi.DTOs;
using DoctorAppointmentWebApi.DTOs.InputDTO;
using DoctorAppointmentWebApi.Models;

namespace DoctorAppointmentWebApi.Mutations;

[ExtendObjectType("Mutation")]
public class DoctorMutation
{
    private readonly ApplicationDbContext _context;

    public DoctorMutation([Service] ApplicationDbContext context)
    {
        _context = context;
    }

    [GraphQLName("createDoctor")]
    public async Task<Doctor> AddDoctor(DoctorInputDto doctorDto)
    {
        var doctor = new Doctor
        {
            FirstName = doctorDto.FirstName,
            LastName = doctorDto.LastName,
            SpecializationId = doctorDto.SpecializationId,
            DepartmentId = doctorDto.DepartmentId,
            PhoneNumber = doctorDto.PhoneNumber,
            Email = doctorDto.Email,
            RoomNumber = doctorDto.RoomNumber
        };

        _context.Doctors.Add(doctor);
        await _context.SaveChangesAsync();

        return doctor;
    }

    [GraphQLName("deleteDoctor")]
    public async Task<Doctor> DeleteDoctor(Guid id)
    {
        var doctor = await _context.Doctors.FindAsync(id);
        if (doctor != null)
        {
            _context.Doctors.Remove(doctor);
            await _context.SaveChangesAsync();
        }

        return doctor;
    }
    
    [GraphQLName("updateDoctor")]
    public async Task<Doctor> UpdateDoctor(Guid id, DoctorInputDto updatedDoctorDto)
    {
        var doctor = await _context.Doctors.FindAsync(id);
        if (doctor == null)
        {
            return null;
        }
        
        doctor.FirstName = updatedDoctorDto.FirstName;
        doctor.LastName = updatedDoctorDto.LastName;
        doctor.SpecializationId = updatedDoctorDto.SpecializationId;
        doctor.DepartmentId = updatedDoctorDto.DepartmentId;
        doctor.PhoneNumber = updatedDoctorDto.PhoneNumber;
        doctor.Email = updatedDoctorDto.Email;
        doctor.RoomNumber = updatedDoctorDto.RoomNumber;
        
        await _context.SaveChangesAsync();

        return doctor;
    }
}
