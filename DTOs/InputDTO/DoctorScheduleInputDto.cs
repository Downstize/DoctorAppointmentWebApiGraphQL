namespace DoctorAppointmentWebApi.DTOs.InputDTO;

public class DoctorScheduleInputDto
{
    public Guid DoctorId { get; set; }
    
    public TimeSpan AvailableFrom { get; set; }
    
    public TimeSpan AvailableTo { get; set; }
    
    public string DayOfWeek { get; set; }
}