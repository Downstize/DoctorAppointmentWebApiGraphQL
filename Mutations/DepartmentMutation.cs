using DoctorAppointmentWebApi.DTOs;
using DoctorAppointmentWebApi.DTOs.InputDTO;
using DoctorAppointmentWebApi.Models;

namespace DoctorAppointmentWebApi.Mutations;

[ExtendObjectType("Mutation")]
public class DepartmentMutation
{
    private readonly ApplicationDbContext _context;

    public DepartmentMutation([Service] ApplicationDbContext context)
    {
        _context = context;
    }

    [GraphQLName("createDepartment")]
    public async Task<Department> AddDepartment(DepartmentInputDto departmentDto)
    {
        var department = new Department
        {
            Name = departmentDto.Name,
            Location = departmentDto.Location
        };

        _context.Departments.Add(department);
        await _context.SaveChangesAsync();

        return department;
    }

    [GraphQLName("deleteDepartment")]
    public async Task<Department> DeleteDepartment(Guid id)
    {
        var department = await _context.Departments.FindAsync(id);
        if (department != null)
        {
            _context.Departments.Remove(department);
            await _context.SaveChangesAsync();
        }

        return department;
    }
    
    [GraphQLName("updateDepartment")]
    public async Task<Department> UpdateDepartment(Guid id, DepartmentInputDto updatedDepartmentDto)
    {
        var department = await _context.Departments.FindAsync(id);
        if (department == null)
        {
            return null;
        }
        
        department.Name = updatedDepartmentDto.Name;
        department.Location = updatedDepartmentDto.Location;
        
        await _context.SaveChangesAsync();

        return department;
    }
}
