namespace DoctorAppointmentWebApi.DTOs.InputDTO;

public class DoctorScheduleInputDto
{
    public Guid DoctorId { get; set; }
    
    public string AvailableFrom { get; set; }
    
    public string AvailableTo { get; set; }
    
    public string DayOfWeek { get; set; }
}