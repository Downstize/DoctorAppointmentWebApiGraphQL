using DoctorAppointmentWebApi.Models;
using DoctorAppointmentWebApi.DTOs;
using HotChocolate;
using HotChocolate.Types;
using Microsoft.EntityFrameworkCore;

namespace DoctorAppointmentWebApi.Queries;

[ExtendObjectType(Name = "Query")]
public class DoctorScheduleQuery
{
    private readonly ApplicationDbContext _context;

    public DoctorScheduleQuery([Service] ApplicationDbContext context)
    {
        _context = context;
    }
    
    [GraphQLName("allDoctorSchedule")]
    public List<DoctorScheduleDto> GetAllDoctorSchedules() =>
        _context.DoctorSchedules
            .Include(ds => ds.Doctor)
            .Select(ds => new DoctorScheduleDto
            {
                ScheduleId = ds.ScheduleId,
                DoctorId = ds.DoctorId,
                AvailableFrom = ds.AvailableFrom,
                AvailableTo = ds.AvailableTo,
                DayOfWeek = ds.DayOfWeek,
            })
            .ToList();
    
    [GraphQLName("doctorScheduleById")]
    public DoctorScheduleDto GetDoctorScheduleById(Guid id) =>
        _context.DoctorSchedules
            .Include(ds => ds.Doctor)
            .Where(ds => ds.ScheduleId == id)
            .Select(ds => new DoctorScheduleDto
            {
                ScheduleId = ds.ScheduleId,
                DoctorId = ds.DoctorId,
                AvailableFrom = ds.AvailableFrom,
                AvailableTo = ds.AvailableTo,
                DayOfWeek = ds.DayOfWeek,
            })
            .FirstOrDefault()!;
}