using DoctorAppointmentWebApi.DTOs.InputDTO;

namespace DoctorAppointmentWebApi.Mutations;

using DoctorAppointmentWebApi;
using DoctorAppointmentWebApi.DTOs;
using DoctorAppointmentWebApi.Models;

[ExtendObjectType("Mutation")]
public class AppointmentMutation
{
    private readonly ApplicationDbContext _context;

    public AppointmentMutation([Service] ApplicationDbContext context)
    {
        _context = context;
    }

    [GraphQLName("createAppointment")]
    public async Task<Appointment> AddAppointment(AppointmentInputDto appointmentDto)
    {
        var appointment = new Appointment
        {
            PatientId = appointmentDto.PatientId,
            DoctorId = appointmentDto.DoctorId,
            AppointmentDateTime = appointmentDto.AppointmentDateTime,
            Status = appointmentDto.Status,
            Notes = appointmentDto.Notes
        };

        _context.Appointments.Add(appointment);
        await _context.SaveChangesAsync();

        return appointment;
    }

    [GraphQLName("deleteAppointment")]
    public async Task<Appointment> DeleteAppointment(Guid id)
    {
        var appointment = await _context.Appointments.FindAsync(id);
        if (appointment != null)
        {
            _context.Appointments.Remove(appointment);
            await _context.SaveChangesAsync();
        }

        return appointment;
    }
    
    [GraphQLName("updateAppointment")]
    public async Task<Appointment> UpdateAppointment(Guid id, AppointmentInputDto updatedAppointmentDto)
    {
        var appointment = await _context.Appointments.FindAsync(id);
        if (appointment == null)
        {
            return null;
        }
        
        appointment.PatientId = updatedAppointmentDto.PatientId;
        appointment.DoctorId = updatedAppointmentDto.DoctorId;
        appointment.AppointmentDateTime = updatedAppointmentDto.AppointmentDateTime;
        appointment.Status = updatedAppointmentDto.Status;
        appointment.Notes = updatedAppointmentDto.Notes;
        
        await _context.SaveChangesAsync();

        return appointment;
    }
}