using System.Text.Json.Serialization;

namespace DoctorAppointmentWebApi.DTOs;

public class SpecializationDto
{
    [JsonIgnore]
    public Guid SpecializationId { get; set; }
    
    public string? SpecializationName { get; set; }
    
}