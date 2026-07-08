using ApiMantenimiento.Models.DTOS.MFlota;
using ApiMantenimiento.Models.Entitys.MFlota;
using ApiMantenimiento.Models.Responses;
using ApiMantenimiento.Repositories.Interfaces.MFlota;
using ApiMantenimiento.Services.Interfaces.MFlota;
using AutoMapper;

namespace ApiMantenimiento.Services.MFlota
{
    public class TipoEquipoService : ITipoEquipoService
    {
        private readonly ITipoEquipoRepository _repository;
        private readonly IMapper _mapper;

        public TipoEquipoService(ITipoEquipoRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<IEnumerable<TipoEquipoResponse>>> ListarAsync()
        {
            var entidades = await _repository.ListarAsync();
            var respuesta = _mapper.Map<IEnumerable<TipoEquipoResponse>>(entidades);
            return ApiResponse<IEnumerable<TipoEquipoResponse>>.SuccessResult(respuesta);
        }

        public async Task<ApiResponse<TipoEquipoResponse>> BuscarPorIdAsync(int id)
        {
            var entidad = await _repository.BuscarPorIdAsync(id);
            if (entidad == null)
                return ApiResponse<TipoEquipoResponse>.Fail("No se encontró el tipo de equipo con el ID proporcionado.");

            return ApiResponse<TipoEquipoResponse>.SuccessResult(_mapper.Map<TipoEquipoResponse>(entidad));
        }

        public async Task<ApiResponse<IEnumerable<TipoEquipoResponse>>> BuscarPorFiltroAsync(string filtro)
        {
            if (string.IsNullOrWhiteSpace(filtro))
                return ApiResponse<IEnumerable<TipoEquipoResponse>>.Fail("El texto de búsqueda no puede estar vacío.");

            var entidades = await _repository.BuscarPorFiltroAsync(filtro.Trim().ToUpper());
            
            if (!entidades.Any())
                return ApiResponse<IEnumerable<TipoEquipoResponse>>.Fail("No se encontraron tipos de equipos que coincidan con la búsqueda.");

            var respuesta = _mapper.Map<IEnumerable<TipoEquipoResponse>>(entidades);
            return ApiResponse<IEnumerable<TipoEquipoResponse>>.SuccessResult(respuesta);
        }

        public async Task<ApiResponse<string>> AgregarAsync(TipoEquipoRequest request)
        {
            request.CodigoEquipo = request.CodigoEquipo.Trim().ToUpper();
            request.NombreTipo = request.NombreTipo.Trim().ToUpper();

            var existeCodigo = await _repository.BuscarPorCodigoAsync(request.CodigoEquipo);
            if (existeCodigo != null)
                return ApiResponse<string>.Fail("El código de tipo de equipo ya está registrado en el sistema.");

            var existeNombre = await _repository.BuscarPorNombreAsync(request.NombreTipo);
            if (existeNombre != null)
                return ApiResponse<string>.Fail("El nombre de tipo de equipo ya está registrado en el sistema.");

            var nuevo = _mapper.Map<TipoEquipo>(request);
            await _repository.AgregarAsync(nuevo);

            return ApiResponse<string>.SuccessResult(nuevo.cod_equipo, "Tipo de equipo creado con éxito.");
        }

        public async Task<ApiResponse<string>> ActualizarAsync(int id, TipoEquipoRequest request)
        {
            var entidad = await _repository.BuscarPorIdAsync(id);
            if (entidad == null)
                return ApiResponse<string>.Fail("No se encontró el tipo de equipo a actualizar.");

            request.CodigoEquipo = request.CodigoEquipo.Trim().ToUpper();
            request.NombreTipo = request.NombreTipo.Trim().ToUpper();

            // Validar duplicados excluyendo el propio registro
            var existeCodigo = await _repository.BuscarPorCodigoAsync(request.CodigoEquipo);
            if (existeCodigo != null && existeCodigo.id_tipo_eqp != id)
                return ApiResponse<string>.Fail("El código ya está en uso por otro tipo de equipo.");

            var existeNombre = await _repository.BuscarPorNombreAsync(request.NombreTipo);
            if (existeNombre != null && existeNombre.id_tipo_eqp != id)
                return ApiResponse<string>.Fail("El nombre ya está en uso por otro tipo de equipo.");

            entidad.cod_equipo = request.CodigoEquipo;
            entidad.nombre_tipo = request.NombreTipo;

            await _repository.ActualizarAsync(entidad);
            return ApiResponse<string>.SuccessResult(entidad.cod_equipo, "Tipo de equipo actualizado con éxito.");
        }
    }
}
