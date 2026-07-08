using AutoMapper;
using ApiMantenimiento.Models.DTOS.MFlota;
using ApiMantenimiento.Models.Entitys.MFlota;
using ApiMantenimiento.Models.Requests.MFlota;
using ApiMantenimiento.Models.Responses.MFlota;

namespace ApiMantenimiento.Helpers.Profiles
{
    public class MFlotaMappingProfile : Profile
    {
        public MFlotaMappingProfile()
        {
            // ==========================================
            // ÁREA DE OPERACIÓN
            // ==========================================
            CreateMap<AreaOperacion, AreaOperacionResponse>()
                .ForMember(dest => dest.CodigoArea, opt => opt.MapFrom(src => src.cod_area_ope))
                .ForMember(dest => dest.NombreArea, opt => opt.MapFrom(src => src.nomb_area));

            CreateMap<AreaOperacionRequest, AreaOperacion>()
                .ForMember(dest => dest.cod_area_ope, opt => opt.MapFrom(src => src.CodigoArea))
                .ForMember(dest => dest.nomb_area, opt => opt.MapFrom(src => src.NombreArea))
                .ForMember(dest => dest.Equipos, opt => opt.Ignore());

            // ==========================================
            // MARCA EQUIPO
            // ==========================================
            CreateMap<MarcaEquipo, MarcaEquipoResponse>()
                .ForMember(dest => dest.IdMarca, opt => opt.MapFrom(src => src.id_marca))
                .ForMember(dest => dest.NombreMarca, opt => opt.MapFrom(src => src.nombre_marca));

            CreateMap<MarcaEquipoRequest, MarcaEquipo>()
                .ForMember(dest => dest.nombre_marca, opt => opt.MapFrom(src => src.NombreMarca))
                .ForMember(dest => dest.id_marca, opt => opt.Ignore())
                .ForMember(dest => dest.Modelos, opt => opt.Ignore());

            // ==========================================
            // TIPO EQUIPO
            // ==========================================
            CreateMap<TipoEquipo, TipoEquipoResponse>()
                .ForMember(dest => dest.IdTipoEqp, opt => opt.MapFrom(src => src.id_tipo_eqp))
                .ForMember(dest => dest.CodigoEquipo, opt => opt.MapFrom(src => src.cod_equipo != null ? src.cod_equipo.Trim() : string.Empty))
                .ForMember(dest => dest.NombreTipo, opt => opt.MapFrom(src => src.nombre_tipo));

            CreateMap<TipoEquipoRequest, TipoEquipo>()
                .ForMember(dest => dest.cod_equipo, opt => opt.MapFrom(src => src.CodigoEquipo))
                .ForMember(dest => dest.nombre_tipo, opt => opt.MapFrom(src => src.NombreTipo))
                .ForMember(dest => dest.id_tipo_eqp, opt => opt.Ignore())
                .ForMember(dest => dest.ModeloEquipos, opt => opt.Ignore());

            // ==========================================
            // MODELO EQUIPO
            // ==========================================
            CreateMap<ModeloEquipo, ModeloEquipoResponse>()
                .ForMember(dest => dest.IdModelo, opt => opt.MapFrom(src => src.id_modelo))
                .ForMember(dest => dest.NombreModelo, opt => opt.MapFrom(src => src.nomb_modelo))
                .ForMember(dest => dest.IdMarca, opt => opt.MapFrom(src => src.id_marca))
                .ForMember(dest => dest.NombreMarca, opt => opt.MapFrom(src => src.Marca != null ? src.Marca.nombre_marca : string.Empty))
                .ForMember(dest => dest.IdTipoEqp, opt => opt.MapFrom(src => src.id_tipo_eqp))
                .ForMember(dest => dest.NombreTipo, opt => opt.MapFrom(src => src.TipoEquipo != null ? src.TipoEquipo.nombre_tipo : string.Empty));

            CreateMap<ModeloEquipoRequest, ModeloEquipo>()
                .ForMember(dest => dest.id_modelo, opt => opt.Ignore())
                .ForMember(dest => dest.id_marca, opt => opt.MapFrom(src => src.IdMarca))
                .ForMember(dest => dest.id_tipo_eqp, opt => opt.MapFrom(src => src.IdTipoEqp))
                .ForMember(dest => dest.nomb_modelo, opt => opt.MapFrom(src => src.NombreModelo))
                .ForMember(dest => dest.Marca, opt => opt.Ignore())
                .ForMember(dest => dest.TipoEquipo, opt => opt.Ignore())
                .ForMember(dest => dest.Flotas, opt => opt.Ignore());

            // ==========================================
            // FLOTA
            // ==========================================
            CreateMap<Flota, FlotaResponse>()
                .ForMember(dest => dest.IdFlota, opt => opt.MapFrom(src => src.id_flota))
                .ForMember(dest => dest.CodFlota, opt => opt.MapFrom(src => src.cod_flota))
                .ForMember(dest => dest.NombreFlota, opt => opt.MapFrom(src => src.nombre_flota))
                .ForMember(dest => dest.TipoControl, opt => opt.MapFrom(src => src.tipo_control))
                .ForMember(dest => dest.IdModelo, opt => opt.MapFrom(src => src.id_modelo))
                .ForMember(dest => dest.NombreModelo, opt => opt.MapFrom(src => src.ModeloEquipo != null ? src.ModeloEquipo.nomb_modelo : string.Empty))
                .ForMember(dest => dest.NombreMarca, opt => opt.MapFrom(src => src.ModeloEquipo != null && src.ModeloEquipo.Marca != null ? src.ModeloEquipo.Marca.nombre_marca : string.Empty))
                .ForMember(dest => dest.NombreTipo, opt => opt.MapFrom(src => src.ModeloEquipo != null && src.ModeloEquipo.TipoEquipo != null ? src.ModeloEquipo.TipoEquipo.nombre_tipo : string.Empty));

            CreateMap<FlotaRequest, Flota>()
                .ForMember(dest => dest.id_flota, opt => opt.Ignore())
                .ForMember(dest => dest.cod_flota, opt => opt.MapFrom(src => src.CodFlota))
                .ForMember(dest => dest.id_modelo, opt => opt.MapFrom(src => src.IdModelo))
                .ForMember(dest => dest.nombre_flota, opt => opt.MapFrom(src => src.NombreFlota))
                .ForMember(dest => dest.tipo_control, opt => opt.MapFrom(src => src.TipoControl))
                .ForMember(dest => dest.ModeloEquipo, opt => opt.Ignore())
                .ForMember(dest => dest.Equipos, opt => opt.Ignore());

            // ==========================================
            // EQUIPO
            // ==========================================
            CreateMap<Equipo, EquipoResponse>()
                .ForMember(dest => dest.IdEquipo, opt => opt.MapFrom(src => src.id_equipo))
                .ForMember(dest => dest.CodEqp, opt => opt.MapFrom(src => src.cod_eqp != null ? src.cod_eqp.Trim() : string.Empty))
                .ForMember(dest => dest.PlacaEqp, opt => opt.MapFrom(src => src.placa_eqp))
                .ForMember(dest => dest.NumSerie, opt => opt.MapFrom(src => src.num_serie))
                .ForMember(dest => dest.HorometroInicial, opt => opt.MapFrom(src => src.horometro_inicial))
                .ForMember(dest => dest.HorometroActual, opt => opt.MapFrom(src => src.horometro_actual))
                .ForMember(dest => dest.EstadoOperativo, opt => opt.MapFrom(src => src.estado_operativo))
                .ForMember(dest => dest.CodAreaOpe, opt => opt.MapFrom(src => src.cod_are_ope))
                .ForMember(dest => dest.NombreArea, opt => opt.MapFrom(src => src.AreaOperacion != null ? src.AreaOperacion.nomb_area : string.Empty))
                .ForMember(dest => dest.IdFlota, opt => opt.MapFrom(src => src.id_flota))
                .ForMember(dest => dest.CodFlota, opt => opt.MapFrom(src => src.Flota != null ? src.Flota.cod_flota : string.Empty))
                .ForMember(dest => dest.NombreFlota, opt => opt.MapFrom(src => src.Flota != null ? src.Flota.nombre_flota : string.Empty))
                .ForMember(dest => dest.NombreModelo, opt => opt.MapFrom(src => src.Flota != null && src.Flota.ModeloEquipo != null ? src.Flota.ModeloEquipo.nomb_modelo : string.Empty))
                .ForMember(dest => dest.NombreMarca, opt => opt.MapFrom(src => src.Flota != null && src.Flota.ModeloEquipo != null && src.Flota.ModeloEquipo.Marca != null ? src.Flota.ModeloEquipo.Marca.nombre_marca : string.Empty))
                .ForMember(dest => dest.NombreTipo, opt => opt.MapFrom(src => src.Flota != null && src.Flota.ModeloEquipo != null && src.Flota.ModeloEquipo.TipoEquipo != null ? src.Flota.ModeloEquipo.TipoEquipo.nombre_tipo : string.Empty));

            CreateMap<EquipoRequest, Equipo>()
                .ForMember(dest => dest.id_equipo, opt => opt.Ignore())
                .ForMember(dest => dest.id_flota, opt => opt.MapFrom(src => src.IdFlota))
                .ForMember(dest => dest.cod_eqp, opt => opt.MapFrom(src => src.CodEqp))
                .ForMember(dest => dest.placa_eqp, opt => opt.MapFrom(src => src.PlacaEqp))
                .ForMember(dest => dest.num_serie, opt => opt.MapFrom(src => src.NumSerie))
                .ForMember(dest => dest.horometro_inicial, opt => opt.MapFrom(src => src.HorometroInicial))
                .ForMember(dest => dest.horometro_actual, opt => opt.MapFrom(src => src.HorometroActual))
                .ForMember(dest => dest.estado_operativo, opt => opt.MapFrom(src => src.EstadoOperativo))
                .ForMember(dest => dest.cod_are_ope, opt => opt.MapFrom(src => src.CodAreaOpe))
                .ForMember(dest => dest.Flota, opt => opt.Ignore())
                .ForMember(dest => dest.AreaOperacion, opt => opt.Ignore());

            // ==========================================
            // TIPO DOCUMENTO
            // ==========================================
            CreateMap<TipoDocumento, TipoDocumentoResponse>()
                .ForMember(dest => dest.CodTipoDocumento, opt => opt.MapFrom(src => src.cod_tipo_documento))
                .ForMember(dest => dest.NombreTipo, opt => opt.MapFrom(src => src.nombre_tipo));

            CreateMap<TipoDocumentoRequest, TipoDocumento>()
                .ForMember(dest => dest.cod_tipo_documento, opt => opt.MapFrom(src => src.CodTipoDocumento))
                .ForMember(dest => dest.nombre_tipo, opt => opt.MapFrom(src => src.NombreTipo))
                .ForMember(dest => dest.DetallesDocumento, opt => opt.Ignore());

            // ==========================================
            // EXPEDIENTE
            // ==========================================
            CreateMap<Expediente, ExpedienteResponse>()
                .ForMember(dest => dest.CodigoExp, opt => opt.MapFrom(src => src.codigo_exp))
                .ForMember(dest => dest.IdEquipo, opt => opt.MapFrom(src => src.id_equipo))
                .ForMember(dest => dest.CodEquipo, opt => opt.MapFrom(src => src.Equipo != null ? src.Equipo.cod_eqp : string.Empty))
                .ForMember(dest => dest.PlacaEquipo, opt => opt.MapFrom(src => src.Equipo != null ? src.Equipo.placa_eqp : string.Empty))
                .ForMember(dest => dest.Documentos, opt => opt.MapFrom(src => src.DetallesDocumento));

            CreateMap<ExpedienteRequest, Expediente>()
                .ForMember(dest => dest.codigo_exp, opt => opt.MapFrom(src => src.CodigoExp))
                .ForMember(dest => dest.id_equipo, opt => opt.MapFrom(src => src.IdEquipo))
                .ForMember(dest => dest.Equipo, opt => opt.Ignore())
                .ForMember(dest => dest.DetallesDocumento, opt => opt.Ignore());

            // ==========================================
            // EXPEDIENTE DOCUMENTO
            // ==========================================
            CreateMap<ExpedienteDocumento, ExpedienteDocumentoResponse>()
                .ForMember(dest => dest.IdExpedienteDocumento, opt => opt.MapFrom(src => src.id_expediente_documento))
                .ForMember(dest => dest.CodigoExp, opt => opt.MapFrom(src => src.codigo_exp))
                .ForMember(dest => dest.CodTipoDocumento, opt => opt.MapFrom(src => src.cod_tipo_documento))
                .ForMember(dest => dest.NombreTipoDocumento, opt => opt.MapFrom(src => src.TipoDocumento != null ? src.TipoDocumento.nombre_tipo : string.Empty))
                .ForMember(dest => dest.FechaRegistro, opt => opt.MapFrom(src => src.fecha_registro))
                .ForMember(dest => dest.FechaVencimiento, opt => opt.MapFrom(src => src.fecha_vencimiento))
                .ForMember(dest => dest.DocumentoUrl, opt => opt.MapFrom(src => src.documento_url));

            CreateMap<ExpedienteDocumentoRequest, ExpedienteDocumento>()
                .ForMember(dest => dest.id_expediente_documento, opt => opt.Ignore())
                .ForMember(dest => dest.codigo_exp, opt => opt.MapFrom(src => src.CodigoExp))
                .ForMember(dest => dest.cod_tipo_documento, opt => opt.MapFrom(src => src.CodTipoDocumento))
                .ForMember(dest => dest.fecha_registro, opt => opt.MapFrom(src => src.FechaRegistro))
                .ForMember(dest => dest.fecha_vencimiento, opt => opt.MapFrom(src => src.FechaVencimiento))
                .ForMember(dest => dest.documento_url, opt => opt.MapFrom(src => src.DocumentoUrl))
                .ForMember(dest => dest.Expediente, opt => opt.Ignore())
                .ForMember(dest => dest.TipoDocumento, opt => opt.Ignore());

            // ==========================================
            // HISTORIAL HOROMETROS
            // ==========================================
            CreateMap<HistorialHorometros, HistorialHorometroResponse>();
            CreateMap<HistorialHorometroRequest, HistorialHorometros>();
        }
    }
}