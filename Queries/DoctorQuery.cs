using DoctorAppointmentWebApi.Models;
using DoctorAppointmentWebApi.DTOs;
using HotChocolate;
using HotChocolate.Types;
using Microsoft.EntityFrameworkCore;

namespace DoctorAppointmentWebApi.Queries;

[ExtendObjectType(Name = "Query")]
public class DoctorQuery
{
    private readonly ApplicationDbContext _context;

    public DoctorQuery([Service] ApplicationDbContext context)
    {
        _context = context;
    }
    
    [GraphQLName("allDoctors")]
    public List<DoctorDto> GetAllDoctors() =>
        _context.Doctors
            .Include(d => d.Specialization)
            .Include(d => d.Department)
            .Select(d => new DoctorDto
            {
                DoctorId = d.DoctorId,
                FirstName = d.FirstName,
                LastName = d.LastName,
                SpecializationId = d.SpecializationId,
                DepartmentId = d.DepartmentId,
                PhoneNumber = d.PhoneNumber,
                Email = d.Email,
                RoomNumber = d.RoomNumber,
            })
            .ToList();
    
    [GraphQLName("doctorById")]
    public DoctorDto GetDoctorById(Guid id) =>
        _context.Doctors
            .Include(d => d.Specialization)
            .Include(d => d.Department)
            .Where(d => d.DoctorId == id)
            .Select(d => new DoctorDto
            {
                DoctorId = d.DoctorId,
                FirstName = d.FirstName,
                LastName = d.LastName,
                SpecializationId = d.SpecializationId,
                DepartmentId = d.DepartmentId,
                PhoneNumber = d.PhoneNumber,
                Email = d.Email,
                RoomNumber = d.RoomNumber,
            })
            .FirstOrDefault()!;
}