using AutoMapper;
using Domain.Dtos.Employee;
using Domain.Dtos.Shift;
using Domain.Entities;

namespace Infrastructure.AutomapperProfile;

public class ServiceProfile : Profile
{
    public ServiceProfile()
    {
        CreateMap<Employee, GetEmployeeDto>().ReverseMap();
        CreateMap<AddEmployeeDto, Employee>().ReverseMap();

        CreateMap<Shift, GetShiftDto>().ReverseMap();
        CreateMap<AddShiftDto, Shift>().ReverseMap();
    }
    
}