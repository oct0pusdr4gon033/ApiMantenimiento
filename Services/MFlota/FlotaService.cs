using ApiMantenimiento.Models.DTOS.MFlota;
using ApiMantenimiento.Models.Entitys.MFlota;
using ApiMantenimiento.Models.Responses;
using ApiMantenimiento.Repositories.Interfaces.MFlota;
using ApiMantenimiento.Services.Interfaces.MFlota;
using AutoMapper;

namespace ApiMantenimiento.Services.MFlota
{
    public class FlotaService : IFlotaService
    {
        private readonly IFlotaRepository _repository;
        private readonly IModeloEquipoRepository _modeloRepo;
        private readonly IEquipoRepository _equipoRepo;
        private readonly IMapper _mapper;

        public FlotaService(
            IFlotaRepository repository,
            IModeloEquipoRepository modeloRepo,
            IEquipoRepository equipoRepo,
            IMapper mapper)
        {
            _repository = repository;
            _modeloRepo = modeloRepo;
            _equipoRepo = equipoRepo;
            _mapper = mapper;
        }

        // ── Listado general ────────────────────────────────────────────────
        public async Task<ApiResponse<IEnumerable<FlotaResponse>>> ListarAsync()
        {
            var entidades = await _repository.ListarAsync();
            var respuesta = _mapper.Map<IEnumerable<FlotaResponse>>(entidades);
            return ApiResponse<IEnumerable<FlotaResponse>>.SuccessResult(respuesta);
        }

        // ── Búsquedas ──────────────────────────────────────────────────────
        public async Task<ApiResponse<FlotaResponse>> BuscarPorIdAsync(int id)
        {
            var entidad = await _repository.BuscarPorIdAsync(id);
            if (entidad == null)
                return ApiResponse<FlotaResponse>.Fail("No se encontró la flota con el ID proporcionado.");

            return ApiResponse<FlotaResponse>.SuccessResult(_mapper.Map<FlotaResponse>(entidad));
        }

        public async Task<ApiResponse<FlotaResponse>> BuscarPorCodigoAsync(string codFlota)
        {
            var entidad = await _repository.BuscarPorCodigoAsync(codFlota.Trim().ToUpper());
            if (entidad == null)
                return ApiResponse<FlotaResponse>.Fail($"No se encontró ninguna flota con el código '{codFlota}'.");

            return ApiResponse<FlotaResponse>.SuccessResult(_mapper.Map<FlotaResponse>(entidad));
        }

        public async Task<ApiResponse<IEnumerable<FlotaResponse>>> BuscarPorTipoEquipoAsync(string nombreTipo)
        {
            if (string.IsNullOrWhiteSpace(nombreTipo))
                return ApiResponse<IEnumerable<FlotaResponse>>.Fail("El texto de búsqueda no puede estar vacío.");

            var entidades = await _repository.BuscarPorNombreTipoAsync(nombreTipo.Trim().ToUpper());

            if (!entidades.Any())
                return ApiResponse<IEnumerable<FlotaResponse>>.Fail($"No se encontraron flotas con tipo de equipo que contenga '{nombreTipo}'.");

            return ApiResponse<IEnumerable<FlotaResponse>>.SuccessResult(_mapper.Map<IEnumerable<FlotaResponse>>(entidades));
        }

        public async Task<ApiResponse<IEnumerable<FlotaResponse>>> BuscarPorModeloAsync(string nombreModelo)
        {
            if (string.IsNullOrWhiteSpace(nombreModelo))
                return ApiResponse<IEnumerable<FlotaResponse>>.Fail("El texto de búsqueda no puede estar vacío.");

            var entidades = await _repository.BuscarPorNombreModeloAsync(nombreModelo.Trim().ToUpper());

            if (!entidades.Any())
                return ApiResponse<IEnumerable<FlotaResponse>>.Fail($"No se encontraron flotas con modelo que contenga '{nombreModelo}'.");

            return ApiResponse<IEnumerable<FlotaResponse>>.SuccessResult(_mapper.Map<IEnumerable<FlotaResponse>>(entidades));
        }

        // ── Detalle de flota ───────────────────────────────────────────────
        public async Task<ApiResponse<FlotaDetalleResponse>> ObtenerDetalleAsync(string codFlota)
        {
            var flota = await _repository.BuscarPorCodigoAsync(codFlota.Trim().ToUpper());
            if (flota == null)
                return ApiResponse<FlotaDetalleResponse>.Fail($"No se encontró ninguna flota con el código '{codFlota}'.");

            // Obtener todos los equipos de esta flota (con su jerarquía completa)
            var equipos = await _equipoRepo.BuscarPorCodigoFlotaAsync(flota.cod_flota);
            var equiposResponse = _mapper.Map<IEnumerable<EquipoResponse>>(equipos);

            var detalle = new FlotaDetalleResponse
            {
                IdFlota      = flota.id_flota,
                CodFlota     = flota.cod_flota,
                NombreFlota  = flota.nombre_flota,
                TipoControl  = flota.tipo_control ?? string.Empty,
                IdModelo     = flota.id_modelo,
                NombreModelo = flota.ModeloEquipo?.nomb_modelo ?? string.Empty,
                NombreMarca  = flota.ModeloEquipo?.Marca?.nombre_marca ?? string.Empty,
                NombreTipo   = flota.ModeloEquipo?.TipoEquipo?.nombre_tipo ?? string.Empty,
                TotalEquipos = equiposResponse.Count(),
                Equipos      = equiposResponse
            };

            return ApiResponse<FlotaDetalleResponse>.SuccessResult(detalle,
                $"Detalle de la flota '{flota.cod_flota}' con {detalle.TotalEquipos} equipo(s) asociado(s).");
        }

        // ── Mutaciones ─────────────────────────────────────────────────────
        public async Task<ApiResponse<string>> AgregarAsync(FlotaRequest request)
        {
            request.CodFlota    = request.CodFlota.Trim().ToUpper();
            request.NombreFlota = request.NombreFlota.Trim().ToUpper();

            // Validar que el modelo existe
            var modelo = await _modeloRepo.BuscarPorIdAsync(request.IdModelo);
            if (modelo == null)
                return ApiResponse<string>.Fail($"No existe un modelo de equipo con ID {request.IdModelo}.");

            // Código de flota único
            var existeCodigo = await _repository.BuscarPorCodigoAsync(request.CodFlota);
            if (existeCodigo != null)
                return ApiResponse<string>.Fail($"El código de flota '{request.CodFlota}' ya está registrado en el sistema.");

            var nueva = _mapper.Map<Flota>(request);
            await _repository.AgregarAsync(nueva);

            return ApiResponse<string>.SuccessResult(nueva.cod_flota, "Flota creada con éxito.");
        }

        public async Task<ApiResponse<string>> ActualizarAsync(int id, FlotaRequest request)
        {
            var entidad = await _repository.BuscarPorIdAsync(id);
            if (entidad == null)
                return ApiResponse<string>.Fail("No se encontró la flota a actualizar.");

            request.CodFlota    = request.CodFlota.Trim().ToUpper();
            request.NombreFlota = request.NombreFlota.Trim().ToUpper();

            // Validar que el modelo existe
            var modelo = await _modeloRepo.BuscarPorIdAsync(request.IdModelo);
            if (modelo == null)
                return ApiResponse<string>.Fail($"No existe un modelo de equipo con ID {request.IdModelo}.");

            // Código único excluyendo el propio registro
            var existeCodigo = await _repository.BuscarPorCodigoAsync(request.CodFlota);
            if (existeCodigo != null && existeCodigo.id_flota != id)
                return ApiResponse<string>.Fail($"El código '{request.CodFlota}' ya está en uso por otra flota.");

            entidad.cod_flota    = request.CodFlota;
            entidad.id_modelo    = request.IdModelo;
            entidad.nombre_flota = request.NombreFlota;
            entidad.tipo_control = request.TipoControl;

            await _repository.ActualizarAsync(entidad);
            return ApiResponse<string>.SuccessResult(entidad.cod_flota, "Flota actualizada con éxito.");
        }
    }
}
