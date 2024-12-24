using System.Text.Json.Serialization;

namespace DoctorAppointmentWebApi.DTOs;

public class DoctorScheduleDto
{
    [JsonIgnore]
    public Guid ScheduleId { get; set; }
    
    public Guid DoctorId { get; set; }
    
    public TimeSpan AvailableFrom { get; set; }
    
    public TimeSpan AvailableTo { get; set; }
    
    public string DayOfWeek { get; set; }
    
}