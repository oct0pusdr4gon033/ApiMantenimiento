using ApiMantenimiento.Models.DTOS.MFlota;
using ApiMantenimiento.Models.Entitys.MFlota;
using ApiMantenimiento.Models.Responses;
using ApiMantenimiento.Repositories.Interfaces.MFlota;
using ApiMantenimiento.Services.Interfaces.MFlota;
using AutoMapper;

namespace ApiMantenimiento.Services.MFlota
{
    public class EquipoService : IEquipoService
    {
        private readonly IEquipoRepository _repository;
        private readonly IFlotaRepository _flotaRepo;
        private readonly IAreaOperacionRepository _areaRepo;
        private readonly IMapper _mapper;

        private static readonly string[] EstadosValidos = { "OPERATIVO", "MANTENIMIENTO", "INACTIVO" };

        public EquipoService(
            IEquipoRepository repository,
            IFlotaRepository flotaRepo,
            IAreaOperacionRepository areaRepo,
            IMapper mapper)
        {
            _repository = repository;
            _flotaRepo = flotaRepo;
            _areaRepo = areaRepo;
            _mapper = mapper;
        }

        public async Task<ApiResponse<IEnumerable<EquipoResponse>>> ListarAsync()
        {
            var entidades = await _repository.ListarAsync();
            var respuesta = _mapper.Map<IEnumerable<EquipoResponse>>(entidades);
            return ApiResponse<IEnumerable<EquipoResponse>>.SuccessResult(respuesta);
        }

        public async Task<ApiResponse<EquipoResponse>> BuscarPorIdAsync(int id)
        {
            var entidad = await _repository.BuscarPorIdAsync(id);
            if (entidad == null)
                return ApiResponse<EquipoResponse>.Fail("No se encontró el equipo con el ID proporcionado.");
            return ApiResponse<EquipoResponse>.SuccessResult(_mapper.Map<EquipoResponse>(entidad));
        }

        public async Task<ApiResponse<EquipoResponse>> BuscarPorCodigoAsync(string codigo)
        {
            var entidad = await _repository.BuscarPorCodigoAsync(codigo.Trim().ToUpper());
            if (entidad == null)
                return ApiResponse<EquipoResponse>.Fail("No se encontró un equipo con ese código.");
            return ApiResponse<EquipoResponse>.SuccessResult(_mapper.Map<EquipoResponse>(entidad));
        }

        public async Task<ApiResponse<EquipoResponse>> BuscarPorPlacaAsync(string placa)
        {
            var entidad = await _repository.BuscarPorPlacaAsync(placa.Trim().ToUpper());
            if (entidad == null)
                return ApiResponse<EquipoResponse>.Fail("No se encontró un equipo con esa placa.");
            return ApiResponse<EquipoResponse>.SuccessResult(_mapper.Map<EquipoResponse>(entidad));
        }

        public async Task<ApiResponse<IEnumerable<EquipoResponse>>> BuscarPorAreaOperacionAsync(string codArea)
        {
            var entidades = await _repository.BuscarPorAreaOperacionAsync(codArea.Trim().ToUpper());
            if (!entidades.Any())
                return ApiResponse<IEnumerable<EquipoResponse>>.Fail("No se encontraron equipos en esa área de operación.");
            return ApiResponse<IEnumerable<EquipoResponse>>.SuccessResult(_mapper.Map<IEnumerable<EquipoResponse>>(entidades));
        }

        public async Task<ApiResponse<IEnumerable<EquipoResponse>>> BuscarPorCodigoFlotaAsync(string codFlota)
        {
            var entidades = await _repository.BuscarPorCodigoFlotaAsync(codFlota.Trim().ToUpper());
            if (!entidades.Any())
                return ApiResponse<IEnumerable<EquipoResponse>>.Fail("No se encontraron equipos para esa flota.");
            return ApiResponse<IEnumerable<EquipoResponse>>.SuccessResult(_mapper.Map<IEnumerable<EquipoResponse>>(entidades));
        }

        public async Task<ApiResponse<IEnumerable<EquipoResponse>>> BuscarPorTipoEquipoAsync(int idTipoEqp)
        {
            var entidades = await _repository.BuscarPorTipoEquipoAsync(idTipoEqp);
            if (!entidades.Any())
                return ApiResponse<IEnumerable<EquipoResponse>>.Fail("No se encontraron equipos para ese tipo de equipo.");
            return ApiResponse<IEnumerable<EquipoResponse>>.SuccessResult(_mapper.Map<IEnumerable<EquipoResponse>>(entidades));
        }

        public async Task<ApiResponse<IEnumerable<EquipoResponse>>> BuscarPorMarcaAsync(int idMarca)
        {
            var entidades = await _repository.BuscarPorMarcaAsync(idMarca);
            if (!entidades.Any())
                return ApiResponse<IEnumerable<EquipoResponse>>.Fail("No se encontraron equipos para esa marca.");
            return ApiResponse<IEnumerable<EquipoResponse>>.SuccessResult(_mapper.Map<IEnumerable<EquipoResponse>>(entidades));
        }

        public async Task<ApiResponse<IEnumerable<EquipoResponse>>> BuscarPorModeloAsync(int idModelo)
        {
            var entidades = await _repository.BuscarPorModeloAsync(idModelo);
            if (!entidades.Any())
                return ApiResponse<IEnumerable<EquipoResponse>>.Fail("No se encontraron equipos para ese modelo.");
            return ApiResponse<IEnumerable<EquipoResponse>>.SuccessResult(_mapper.Map<IEnumerable<EquipoResponse>>(entidades));
        }

        public async Task<ApiResponse<string>> AgregarAsync(EquipoRequest request)
        {
            // Normalizar
            request.CodEqp = request.CodEqp.Trim().ToUpper();
            request.EstadoOperativo = request.EstadoOperativo.Trim().ToUpper();
            request.CodAreaOpe = request.CodAreaOpe.Trim().ToUpper();
            if (!string.IsNullOrEmpty(request.PlacaEqp))
                request.PlacaEqp = request.PlacaEqp.Trim().ToUpper();

            // Validar estado operativo
            if (!EstadosValidos.Contains(request.EstadoOperativo))
                return ApiResponse<string>.Fail("El estado operativo debe ser: OPERATIVO, MANTENIMIENTO o INACTIVO.");

            // Validar horómetro
            if (request.HorometroActual < request.HorometroInicial)
                return ApiResponse<string>.Fail("El horómetro actual no puede ser menor al horómetro inicial.");

            // Validar que la flota existe
            var flota = await _flotaRepo.BuscarPorIdAsync(request.IdFlota);
            if (flota == null)
                return ApiResponse<string>.Fail($"No existe una flota con ID {request.IdFlota}.");

            // Validar que el área de operación existe
            var area = await _areaRepo.BuscarPorCodigoAsync(request.CodAreaOpe);
            if (area == null)
                return ApiResponse<string>.Fail($"No existe un área de operación con código '{request.CodAreaOpe}'.");

            // Validar código único
            var existeCodigo = await _repository.BuscarPorCodigoAsync(request.CodEqp);
            if (existeCodigo != null)
                return ApiResponse<string>.Fail("El código de equipo ya está registrado en el sistema.");

            // Validar placa única (si se proporcionó)
            if (!string.IsNullOrEmpty(request.PlacaEqp))
            {
                var existePlaca = await _repository.BuscarPorPlacaAsync(request.PlacaEqp);
                if (existePlaca != null)
                    return ApiResponse<string>.Fail("La placa del equipo ya está registrada en el sistema.");
            }

            var nuevoEquipo = _mapper.Map<Equipo>(request);
            await _repository.AgregarAsync(nuevoEquipo);

            return ApiResponse<string>.SuccessResult(nuevoEquipo.cod_eqp, "Equipo registrado con éxito.");
        }

        public async Task<ApiResponse<string>> ActualizarAsync(int id, EquipoRequest request)
        {
            var entidad = await _repository.BuscarPorIdAsync(id);
            if (entidad == null)
                return ApiResponse<string>.Fail("No se encontró el equipo a actualizar.");

            // Normalizar
            request.CodEqp = request.CodEqp.Trim().ToUpper();
            request.EstadoOperativo = request.EstadoOperativo.Trim().ToUpper();
            request.CodAreaOpe = request.CodAreaOpe.Trim().ToUpper();
            if (!string.IsNullOrEmpty(request.PlacaEqp))
                request.PlacaEqp = request.PlacaEqp.Trim().ToUpper();

            // Validar estado operativo
            if (!EstadosValidos.Contains(request.EstadoOperativo))
                return ApiResponse<string>.Fail("El estado operativo debe ser: OPERATIVO, MANTENIMIENTO o INACTIVO.");

            // Validar horómetro
            if (request.HorometroActual < request.HorometroInicial)
                return ApiResponse<string>.Fail("El horómetro actual no puede ser menor al horómetro inicial.");

            // Validar que la flota existe
            var flota = await _flotaRepo.BuscarPorIdAsync(request.IdFlota);
            if (flota == null)
                return ApiResponse<string>.Fail($"No existe una flota con ID {request.IdFlota}.");

            // Validar que el área existe
            var area = await _areaRepo.BuscarPorCodigoAsync(request.CodAreaOpe);
            if (area == null)
                return ApiResponse<string>.Fail($"No existe un área de operación con código '{request.CodAreaOpe}'.");

            // Validar código único excluyendo el propio registro
            var existeCodigo = await _repository.BuscarPorCodigoAsync(request.CodEqp);
            if (existeCodigo != null && existeCodigo.id_equipo != id)
                return ApiResponse<string>.Fail("El código de equipo ya está en uso.");

            // Validar placa única excluyendo el propio registro
            if (!string.IsNullOrEmpty(request.PlacaEqp))
            {
                var existePlaca = await _repository.BuscarPorPlacaAsync(request.PlacaEqp);
                if (existePlaca != null && existePlaca.id_equipo != id)
                    return ApiResponse<string>.Fail("La placa ya está en uso por otro equipo.");
            }

            entidad.id_flota = request.IdFlota;
            entidad.cod_eqp = request.CodEqp;
            entidad.placa_eqp = request.PlacaEqp;
            entidad.num_serie = request.NumSerie;
            entidad.horometro_inicial = request.HorometroInicial;
            entidad.horometro_actual = request.HorometroActual;
            entidad.estado_operativo = request.EstadoOperativo;
            entidad.cod_are_ope = request.CodAreaOpe;

            await _repository.ActualizarAsync(entidad);
            return ApiResponse<string>.SuccessResult(entidad.cod_eqp, "Equipo actualizado con éxito.");
        }
    }
}
