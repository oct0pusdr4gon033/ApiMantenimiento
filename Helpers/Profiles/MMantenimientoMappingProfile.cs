using AutoMapper;
using ApiMantenimiento.Models.Entitys.MMantenimiento;
using ApiMantenimiento.Models.DTOS.MMantenimiento;

namespace ApiMantenimiento.Helpers.Profiles
{
    public class MMantenimientoMappingProfile : Profile
    {
        public MMantenimientoMappingProfile()
        {
            CreateMap<Estrategia, EstrategiaResponse>()
                .ForMember(dest => dest.cod_flota, opt => opt.MapFrom(src => src.Flota != null ? src.Flota.cod_flota : null))
                .ForMember(dest => dest.nombre_flota, opt => opt.MapFrom(src => src.Flota != null ? src.Flota.nombre_flota : null))
                .ForMember(dest => dest.cod_equipo, opt => opt.MapFrom(src => src.Equipo != null ? src.Equipo.cod_eqp : null));
            
            CreateMap<EstrategiaRequest, Estrategia>();

            CreateMap<EstrategiaDetalle, EstrategiaDetalleResponse>();
            CreateMap<EstrategiaDetalleRequest, EstrategiaDetalle>();

            // Catálogo Actividades
            CreateMap<SistemaEquipo, SistemaEquipoResponse>();
            CreateMap<SistemaEquipoRequest, SistemaEquipo>();

            CreateMap<SubSistemaEquipo, SubSistemaResponse>();
            CreateMap<SubSistemaRequest, SubSistemaEquipo>();

            CreateMap<ActividadSistema, ActividadSistemaResponse>();
            CreateMap<ActividadSistemaRequest, ActividadSistema>();
            CreateMap<ActividadSistemaUpdateRequest, ActividadSistema>();
        }
    }
}
