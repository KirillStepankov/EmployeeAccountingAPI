using AutoMapper;
using EmployeeAccounting.DTO;
using EmployeeAccounting.Models;

namespace EmployeeAccounting.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Employee, EmployeeDto>();
            CreateMap<EmployeeDto, Employee>();
            CreateMap<Department, DepartmentDto>();
            CreateMap<DepartmentDto, Department>();
            CreateMap<Post, PostDto>();
            CreateMap<PostDto, Post>();
        }
    }
}
