using DoctorAppointmentWebApi.Models;
using DoctorAppointmentWebApi.DTOs;
using Microsoft.EntityFrameworkCore;

namespace DoctorAppointmentWebApi.Queries;

[ExtendObjectType(Name = "Query")]
public class PatientQuery
{
    private readonly ApplicationDbContext _context;

    public PatientQuery([Service] ApplicationDbContext context)
    {
        _context = context;
    }
    
    [GraphQLName("allPatients")]
    public List<PatientDto> GetAllPatients() =>
        _context.Patients
            .Select(p => new PatientDto
            {
                PatientID = p.PatientID,
                FirstName = p.FirstName,
                LastName = p.LastName,
                DateOfBirth = p.DateOfBirth,
                Gender = p.Gender,
                PhoneNumber = p.PhoneNumber,
                Email = p.Email,
                Address = p.Address,
                InsuranceNumber = p.InsuranceNumber
            })
            .ToList();
    
    [GraphQLName("patientById")]
    public PatientDto GetPatientById(Guid id) =>
        _context.Patients
            .Where(p => p.PatientID == id)
            .Select(p => new PatientDto
            {
                PatientID = p.PatientID,
                FirstName = p.FirstName,
                LastName = p.LastName,
                DateOfBirth = p.DateOfBirth,
                Gender = p.Gender,
                PhoneNumber = p.PhoneNumber,
                Email = p.Email,
                Address = p.Address,
                InsuranceNumber = p.InsuranceNumber
            })
            .FirstOrDefault()!;
}