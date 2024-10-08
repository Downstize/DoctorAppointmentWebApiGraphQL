using DoctorAppointmentWebApi.DTOs;
using DoctorAppointmentWebApi.DTOs.InputDTO;
using DoctorAppointmentWebApi.Models;

namespace DoctorAppointmentWebApi.Mutations;

[ExtendObjectType("Mutation")]
public class PatientMutation
{
    private readonly ApplicationDbContext _context;

    public PatientMutation([Service] ApplicationDbContext context)
    {
        _context = context;
    }

    [GraphQLName("createPatient")]
    public async Task<Patient> AddPatient(PatientInputDto patientDto)
    {
        var patient = new Patient
        {
            FirstName = patientDto.FirstName,
            LastName = patientDto.LastName,
            DateOfBirth = patientDto.DateOfBirth,
            Gender = patientDto.Gender,
            PhoneNumber = patientDto.PhoneNumber,
            Email = patientDto.Email,
            Address = patientDto.Address,
            InsuranceNumber = patientDto.InsuranceNumber
        };

        _context.Patients.Add(patient);
        await _context.SaveChangesAsync();

        return patient;
    }

    [GraphQLName("deletePatient")]
    public async Task<Patient> DeletePatient(Guid id)
    {
        var patient = await _context.Patients.FindAsync(id);
        if (patient != null)
        {
            _context.Patients.Remove(patient);
            await _context.SaveChangesAsync();
        }

        return patient;
    }
    
    [GraphQLName("updatePatient")]
    public async Task<Patient> UpdatePatient(Guid id, PatientInputDto updatedPatientDto)
    {
        var patient = await _context.Patients.FindAsync(id);
        if (patient == null)
        {
            return null;
        }
        
        patient.FirstName = updatedPatientDto.FirstName;
        patient.LastName = updatedPatientDto.LastName;
        patient.DateOfBirth = updatedPatientDto.DateOfBirth;
        patient.Gender = updatedPatientDto.Gender;
        patient.PhoneNumber = updatedPatientDto.PhoneNumber;
        patient.Email = updatedPatientDto.Email;
        patient.Address = updatedPatientDto.Address;
        patient.InsuranceNumber = updatedPatientDto.InsuranceNumber;
        
        await _context.SaveChangesAsync();

        return patient;
    }
}
