using ApiMantenimiento.Models.Entitys.MEmpleados;
using ApiMantenimiento.Models.Requests.MEmpleados;
using ApiMantenimiento.Models.Responses;
using ApiMantenimiento.Models.Responses.MEmpleados;
using ApiMantenimiento.Repositories.Interfaces.MEmpleados;
using ApiMantenimiento.Services.Interfaces.MEmpleados;
using AutoMapper;

namespace ApiMantenimiento.Services.MEmpleados
{
    public class RolService : IRolService
    {
        private readonly IRolRepository _rolRepo;
        private readonly IMapper _mapper;

        public RolService(IRolRepository rolRepo, IMapper mapper)
        {
            _rolRepo = rolRepo;
            _mapper = mapper;
        }

        public async Task<ApiResponse<IEnumerable<RolResponse>>> ObtenerTodosAsync()
        {
            var roles = await _rolRepo.ObtenerTodosAsync();
            var res = _mapper.Map<IEnumerable<RolResponse>>(roles);
            return ApiResponse<IEnumerable<RolResponse>>.SuccessResult(res);
        }

        public async Task<ApiResponse<RolResponse>> ObtenerPorIdAsync(int id)
        {
            var rol = await _rolRepo.ObtenerPorIdAsync(id);
            if (rol == null) return ApiResponse<RolResponse>.Fail("Rol no encontrado.");
            return ApiResponse<RolResponse>.SuccessResult(_mapper.Map<RolResponse>(rol));
        }

        public async Task<ApiResponse<RolResponse>> CrearAsync(RolRequest request)
        {
            var rol = _mapper.Map<Rol>(request);
            await _rolRepo.AgregarAsync(rol);
            await _rolRepo.GuardarCambiosAsync();
            return ApiResponse<RolResponse>.SuccessResult(_mapper.Map<RolResponse>(rol));
        }

        public async Task<ApiResponse<RolResponse>> ActualizarAsync(int id, RolRequest request)
        {
            var rol = await _rolRepo.ObtenerPorIdAsync(id);
            if (rol == null) return ApiResponse<RolResponse>.Fail("Rol no encontrado.");

            _mapper.Map(request, rol);
            await _rolRepo.ActualizarAsync(rol);
            await _rolRepo.GuardarCambiosAsync();

            return ApiResponse<RolResponse>.SuccessResult(_mapper.Map<RolResponse>(rol));
        }
    }
}
