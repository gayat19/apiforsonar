using AutoMapper;
using FirstAPI.Models;
using FirstAPI.Models.DTOs;

namespace FirstAPI.Misc
{
    public class EmployeeSalaryMapper :Profile
    {
        public EmployeeSalaryMapper()
        {
            CreateMap<EmployeeSalaryRequest, EmployeeSalary>();
            CreateMap<EmployeeSalary, EmployeeSalaryRequest>();
        }
    }
}
