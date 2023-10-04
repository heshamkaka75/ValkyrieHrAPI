using AutoMapper;
using System.Diagnostics.Metrics;
using ValkyrieHr.Domain.Dtos.Account;
using ValkyrieHr.Domain.Dtos.Employee;
using ValkyrieHr.Domain.Dtos.Vacation;
using ValkyrieHr.Domain.Models;
using ValkyrieHr.Models;

namespace ValkyrieHr.Helper.Mapping
{
    public class DtosToDomainProfile : Profile
    {
        public DtosToDomainProfile()
        {
            CreateMap<CreateUserRequestDto, ApplicationUser>();
            CreateMap<UpdateAccountRequestDto, ApplicationUser>();

            CreateMap<CreateEmployeeRequestDto, Employee>();
            CreateMap<UpdateEmployeeRequestDto, Employee>();

            CreateMap<CreateVacationRequestDto, VacationBalance>();
            CreateMap<UpdateVacationRequestDto, VacationBalance>();

            CreateMap<CreateEmployeeVacationDto, Vacation>();

        }
    }
}
