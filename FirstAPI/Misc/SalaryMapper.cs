using AutoMapper;
using FirstAPI.Models;
using FirstAPI.Models.DTOs;

namespace FirstAPI.Misc
{
    public class SalaryMapper : Profile
    {
        public SalaryMapper()
        {
            CreateMap<Salary, SalaryResponse>();
            CreateMap<SalaryResponse, Salary> ();

        }
    }
}
