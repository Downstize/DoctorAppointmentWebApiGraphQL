using DoctorAppointmentWebApi.DTOs;
using DoctorAppointmentWebApi.DTOs.InputDTO;
using DoctorAppointmentWebApi.Models;

namespace DoctorAppointmentWebApi.Mutations;

[ExtendObjectType("Mutation")]
public class DoctorScheduleMutation
{
    private readonly ApplicationDbContext _context;

    public DoctorScheduleMutation([Service] ApplicationDbContext context)
    {
        _context = context;
    }

    [GraphQLName("createDoctorSchedule")]
    public async Task<DoctorSchedule> AddDoctorSchedule(DoctorScheduleInputDto doctorScheduleDto)
    {
        var doctorSchedule = new DoctorSchedule
        {
            DoctorId = doctorScheduleDto.DoctorId,
            AvailableFrom = doctorScheduleDto.AvailableFrom,
            AvailableTo = doctorScheduleDto.AvailableTo,
            DayOfWeek = doctorScheduleDto.DayOfWeek
        };

        _context.DoctorSchedules.Add(doctorSchedule);
        await _context.SaveChangesAsync();

        return doctorSchedule;
    }

    [GraphQLName("deleteDoctorSchedule")]
    public async Task<DoctorSchedule> DeleteDoctorSchedule(Guid id)
    {
        var doctorSchedule = await _context.DoctorSchedules.FindAsync(id);
        if (doctorSchedule != null)
        {
            _context.DoctorSchedules.Remove(doctorSchedule);
            await _context.SaveChangesAsync();
        }

        return doctorSchedule;
    }
    
    [GraphQLName("updateDoctorSchedule")]
    public async Task<DoctorSchedule> UpdateDoctorSchedule(Guid id, DoctorScheduleInputDto updatedDoctorScheduleDto)
    {
        var doctorSchedule = await _context.DoctorSchedules.FindAsync(id);
        if (doctorSchedule == null)
        {
            return null;
        }
        
        doctorSchedule.AvailableFrom = updatedDoctorScheduleDto.AvailableFrom;
        doctorSchedule.AvailableTo = updatedDoctorScheduleDto.AvailableTo;
        doctorSchedule.DayOfWeek = updatedDoctorScheduleDto.DayOfWeek;
        
        await _context.SaveChangesAsync();

        return doctorSchedule;
    }
}
