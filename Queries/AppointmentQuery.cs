using DoctorAppointmentWebApi.DTOs;
using DoctorAppointmentWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace DoctorAppointmentWebApi.Queries;

[ExtendObjectType(Name = "Query")]
public class AppointmentQuery
{
    private readonly ApplicationDbContext _context;

    public AppointmentQuery([Service] ApplicationDbContext context)
    {
        _context = context;
    }

    [GraphQLName("allAppointments")]
    public async Task<List<AppointmentDto>> GetAllAppointmentsAsync() =>
        await _context.Appointments
            .Include(a => a.Patient)
            .Include(a => a.Doctor)
            .Select(a => new AppointmentDto
            {
                AppointmentId = a.AppointmentId,
                PatientId = a.PatientId,
                DoctorId = a.DoctorId,
                AppointmentDateTime = a.AppointmentDateTime,
                Status = a.Status,
                Notes = a.Notes
            })
            .ToListAsync();




    [GraphQLName("appointmentById")]
    public AppointmentDto GetAppointmentById(Guid id) =>
        _context.Appointments
            .Include(a => a.Patient) // Включаем связь с пациентом
            .Include(a => a.Doctor)  // Включаем связь с врачом
            .Where(a => a.AppointmentId == id) // Фильтрация по ID
            .Select(a => new AppointmentDto
            {
                AppointmentId = a.AppointmentId,
                PatientId = a.PatientId,
                DoctorId = a.DoctorId,
                AppointmentDateTime = a.AppointmentDateTime,
                Status = a.Status,
                Notes = a.Notes
            })
            .FirstOrDefault()!; // Возвращаем первый найденный результат
}
