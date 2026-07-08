using ApiMantenimiento.Models.Entitys.MFlota;
using ApiMantenimiento.Models.Requests.MFlota;
using ApiMantenimiento.Models.Responses;
using ApiMantenimiento.Models.Responses.MFlota;
using ApiMantenimiento.Repositories.Interfaces.MFlota;
using ApiMantenimiento.Services.Interfaces.MFlota;
using ApiMantenimiento.Services.Interfaces.MMantenimiento;
using ApiMantenimiento.Repositories.Interfaces.MEmpleados;
using AutoMapper;

namespace ApiMantenimiento.Services.MFlota
{
    public class HistorialHorometroService : IHistorialHorometroService
    {
        private readonly IHistorialHorometroRepository _historialRepo;
        private readonly IEquipoRepository _equipoRepo;
        private readonly IEmpleadoRepository _empleadoRepo;
        private readonly IOrdenTrabajoService _otService;
        private readonly IMapper _mapper;

        public HistorialHorometroService(
            IHistorialHorometroRepository historialRepo,
            IEquipoRepository equipoRepo,
            IEmpleadoRepository empleadoRepo,
            IOrdenTrabajoService otService,
            IMapper mapper)
        {
            _historialRepo = historialRepo;
            _equipoRepo    = equipoRepo;
            _empleadoRepo  = empleadoRepo;
            _otService     = otService;
            _mapper        = mapper;
        }

        public async Task<ApiResponse<IEnumerable<HistorialHorometroResponse>>> ObtenerTodosAsync()
        {
            var historiales = await _historialRepo.ObtenerTodosAsync();
            var response = _mapper.Map<IEnumerable<HistorialHorometroResponse>>(historiales);
            return ApiResponse<IEnumerable<HistorialHorometroResponse>>.SuccessResult(response);
        }

        public async Task<ApiResponse<IEnumerable<HistorialHorometroResponse>>> ObtenerPorEquipoAsync(int idEquipo)
        {
            var historiales = await _historialRepo.ObtenerPorEquipoAsync(idEquipo);
            var response = _mapper.Map<IEnumerable<HistorialHorometroResponse>>(historiales);
            return ApiResponse<IEnumerable<HistorialHorometroResponse>>.SuccessResult(response);
        }

        public async Task<ApiResponse<HistorialHorometroResponse>> ObtenerPorCodigoAsync(string codigo)
        {
            var historial = await _historialRepo.ObtenerPorCodigoAsync(codigo);
            if (historial == null)
                return ApiResponse<HistorialHorometroResponse>.Fail("Historial no encontrado.");

            var response = _mapper.Map<HistorialHorometroResponse>(historial);
            return ApiResponse<HistorialHorometroResponse>.SuccessResult(response);
        }

        public async Task<ApiResponse<HistorialHorometroResponse>> CrearAsync(HistorialHorometroRequest request)
        {
            // Validaciones
            var empleado = await _empleadoRepo.ObtenerPorDniAsync(request.dni_conductor);
            if (empleado == null || !empleado.estado)
                return ApiResponse<HistorialHorometroResponse>.Fail("El empleado conductor no existe o está inactivo.");

            if (empleado.Rol.prefijo != "COND")
                return ApiResponse<HistorialHorometroResponse>.Fail("El empleado no tiene el rol de Conductor.");

            var equipo = await _equipoRepo.BuscarPorIdAsync(request.id_equipo);
            if (equipo == null)
                return ApiResponse<HistorialHorometroResponse>.Fail("El equipo no existe.");

            if (request.lectura_actual <= request.lectura_anterior)
                return ApiResponse<HistorialHorometroResponse>.Fail("La lectura actual debe ser mayor a la lectura anterior.");

            var horasCalculadas = request.lectura_actual - request.lectura_anterior;

            if (horasCalculadas > 24)
                return ApiResponse<HistorialHorometroResponse>.Fail("No se pueden operar más de 24 horas en un solo registro.");

            // Validar que las horas operadas coincidan con el cálculo
            if (request.horas_operadas != horasCalculadas)
                return ApiResponse<HistorialHorometroResponse>.Fail($"El cÃ¡lculo de horas operadas es incorrecto. Debe ser {horasCalculadas}.");

            var nuevoHistorial = _mapper.Map<HistorialHorometros>(request);
            nuevoHistorial.codigo_hist = $"HIST-{Guid.NewGuid().ToString().Substring(0, 8).ToUpper()}";
            nuevoHistorial.fecha_registro = DateTime.Now;

            // Actualizar el horÃ³metro del equipo
            equipo.horometro_actual = request.lectura_actual;

            await _historialRepo.AgregarAsync(nuevoHistorial);
            await _equipoRepo.ActualizarAsync(equipo);
            await _historialRepo.GuardarCambiosAsync();

            // ── Hook automático: delegado a trigger de base de datos TR_Equipo_ActualizarHorometro ──
            // El trigger se dispara al actualizar el horómetro en la tabla Equipo (línea 94 SaveChanges)
            // await _otService.EvaluarUmbralesYGenerarOTsAsync(request.id_equipo, request.lectura_actual);
            // await _otService.RecalcularCalendarioAsync(request.id_equipo);

            var response = _mapper.Map<HistorialHorometroResponse>(nuevoHistorial);
            return ApiResponse<HistorialHorometroResponse>.SuccessResult(response, "Historial registrado correctamente.");
        }
    }
}
