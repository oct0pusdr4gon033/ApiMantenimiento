using ApiMantenimiento.Models.Entitys.MEmpleados;
using ApiMantenimiento.Models.Requests.MEmpleados;
using ApiMantenimiento.Models.Responses;
using ApiMantenimiento.Models.Responses.MEmpleados;
using ApiMantenimiento.Repositories.Interfaces.MEmpleados;
using ApiMantenimiento.Services.Interfaces.MEmpleados;
using AutoMapper;

namespace ApiMantenimiento.Services.MEmpleados
{
    public class EmpleadoService : IEmpleadoService
    {
        private readonly IEmpleadoRepository _empleadoRepo;
        private readonly IRolRepository _rolRepo;
        private readonly IMapper _mapper;

        public EmpleadoService(IEmpleadoRepository empleadoRepo, IRolRepository rolRepo, IMapper mapper)
        {
            _empleadoRepo = empleadoRepo;
            _rolRepo = rolRepo;
            _mapper = mapper;
        }

        public async Task<ApiResponse<IEnumerable<EmpleadoResponse>>> ObtenerTodosActivosAsync()
        {
            var empleados = await _empleadoRepo.ObtenerTodosActivosAsync();
            return ApiResponse<IEnumerable<EmpleadoResponse>>.SuccessResult(_mapper.Map<IEnumerable<EmpleadoResponse>>(empleados));
        }

        public async Task<ApiResponse<IEnumerable<EmpleadoResponse>>> ObtenerTodosAsync()
        {
            var empleados = await _empleadoRepo.ObtenerTodosAsync();
            return ApiResponse<IEnumerable<EmpleadoResponse>>.SuccessResult(_mapper.Map<IEnumerable<EmpleadoResponse>>(empleados));
        }

        public async Task<ApiResponse<EmpleadoResponse>> ObtenerPorDniAsync(string dni)
        {
            var empleado = await _empleadoRepo.ObtenerPorDniAsync(dni);
            if (empleado == null) return ApiResponse<EmpleadoResponse>.Fail("Empleado no encontrado.");
            return ApiResponse<EmpleadoResponse>.SuccessResult(_mapper.Map<EmpleadoResponse>(empleado));
        }

        public async Task<ApiResponse<EmpleadoResponse>> CrearAsync(EmpleadoRequest request)
        {
            var existe = await _empleadoRepo.ObtenerPorDniAsync(request.dni_empleado);
            if (existe != null) return ApiResponse<EmpleadoResponse>.Fail("Ya existe un empleado con este DNI.");

            var rol = await _rolRepo.ObtenerPorIdAsync(request.id_rol);
            if (rol == null) return ApiResponse<EmpleadoResponse>.Fail("El rol seleccionado no existe.");

            // Generar código
            int correlativo = await _empleadoRepo.ObtenerConteoPorPrefijoAsync(rol.prefijo) + 1;
            string nuevoCodigo = $"{rol.prefijo}-{correlativo:D4}";

            var empleado = _mapper.Map<Empleado>(request);
            empleado.codigo_empleado = nuevoCodigo;
            empleado.estado = true;

            await _empleadoRepo.AgregarAsync(empleado);
            await _empleadoRepo.GuardarCambiosAsync();

            return ApiResponse<EmpleadoResponse>.SuccessResult(_mapper.Map<EmpleadoResponse>(empleado));
        }

        public async Task<ApiResponse<EmpleadoResponse>> ActualizarAsync(string dni, EmpleadoRequest request)
        {
            var empleado = await _empleadoRepo.ObtenerPorDniAsync(dni);
            if (empleado == null) return ApiResponse<EmpleadoResponse>.Fail("Empleado no encontrado.");

            var rol = await _rolRepo.ObtenerPorIdAsync(request.id_rol);
            if (rol == null) return ApiResponse<EmpleadoResponse>.Fail("El rol seleccionado no existe.");

            // Si cambió de rol, generar nuevo código
            if (empleado.id_rol != request.id_rol)
            {
                int correlativo = await _empleadoRepo.ObtenerConteoPorPrefijoAsync(rol.prefijo) + 1;
                empleado.codigo_empleado = $"{rol.prefijo}-{correlativo:D4}";
            }

            _mapper.Map(request, empleado);
            await _empleadoRepo.ActualizarAsync(empleado);
            await _empleadoRepo.GuardarCambiosAsync();

            return ApiResponse<EmpleadoResponse>.SuccessResult(_mapper.Map<EmpleadoResponse>(empleado));
        }

        public async Task<ApiResponse<bool>> EliminarLogicoAsync(string dni)
        {
            var empleado = await _empleadoRepo.ObtenerPorDniAsync(dni);
            if (empleado == null) return ApiResponse<bool>.Fail("Empleado no encontrado.");

            empleado.estado = false;
            await _empleadoRepo.ActualizarAsync(empleado);
            await _empleadoRepo.GuardarCambiosAsync();

            return ApiResponse<bool>.SuccessResult(true, "Empleado desactivado correctamente.");
        }
    }
}
