using ApiMantenimiento.Models.Entitys.MEmpleados;
using ApiMantenimiento.Models.Requests.MEmpleados;
using ApiMantenimiento.Models.Responses.MEmpleados;
using AutoMapper;

namespace ApiMantenimiento.Helpers.Profiles
{
    public class MEmpleadosMappingProfile : Profile
    {
        public MEmpleadosMappingProfile()
        {
            CreateMap<Rol, RolResponse>();
            CreateMap<RolRequest, Rol>();

            CreateMap<Empleado, EmpleadoResponse>()
                .ForMember(dest => dest.nombre_rol, opt => opt.MapFrom(src => src.Rol.nombre_rol));
            
            CreateMap<EmpleadoRequest, Empleado>();
        }
    }
}
