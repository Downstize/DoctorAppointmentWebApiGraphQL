using DoctorAppointmentWebApi.Models;
using DoctorAppointmentWebApi.DTOs;
using Microsoft.EntityFrameworkCore;

namespace DoctorAppointmentWebApi.Queries;

[ExtendObjectType(Name = "Query")]
public class DepartmentQuery
{
    private readonly ApplicationDbContext _context;

    public DepartmentQuery([Service] ApplicationDbContext context)
    {
        _context = context;
    }
    
    [GraphQLName("allDepartments")]
    public List<DepartmentDto> GetAllDepartments() =>
        _context.Departments
            .Select(d => new DepartmentDto
            {
                DepartmentId = d.DepartmentId,
                Name = d.Name,
                Location = d.Location
            })
            .ToList();
    
    [GraphQLName("departmentById")]
    public DepartmentDto GetDepartmentById(Guid id) =>
        _context.Departments
            .Where(d => d.DepartmentId == id)
            .Select(d => new DepartmentDto
            {
                DepartmentId = d.DepartmentId,
                Name = d.Name,
                Location = d.Location
            })
            .FirstOrDefault()!;
}