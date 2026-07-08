using ApiMantenimiento.Models.DTOS.MFlota;
using ApiMantenimiento.Models.Entitys.MFlota;
using ApiMantenimiento.Models.Responses;
using ApiMantenimiento.Repositories.Interfaces.MFlota;
using ApiMantenimiento.Services.Interfaces.MFlota;
using AutoMapper;

namespace ApiMantenimiento.Services.MFlota
{
    public class MarcaEquipoService : IMarcaEquipoService
    {
        private readonly IMarcaEquipoRepository _repository;
        private readonly IMapper _mapper;

        public MarcaEquipoService(IMarcaEquipoRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<IEnumerable<MarcaEquipoResponse>>> ListarAsync()
        {
            var entidades = await _repository.ListarMarcasAsync();
            var respuesta = _mapper.Map<IEnumerable<MarcaEquipoResponse>>(entidades);
            return ApiResponse<IEnumerable<MarcaEquipoResponse>>.SuccessResult(respuesta);
        }

        public async Task<ApiResponse<string>> AgregarAsync(MarcaEquipoRequest request)
        {
            request.NombreMarca = request.NombreMarca.Trim().ToUpper();

            var existe = await _repository.BuscarPorNombreAsync(request.NombreMarca);
            if (existe != null)
                return ApiResponse<string>.Fail("La marca ya está registrada en el sistema.");

            var nuevaMarca = _mapper.Map<MarcaEquipo>(request);
            await _repository.AgregarAsync(nuevaMarca);

            return ApiResponse<string>.SuccessResult(nuevaMarca.nombre_marca, "Marca registrada con éxito.");
        }

        public async Task<ApiResponse<string>> ActualizarAsync(int id, MarcaEquipoRequest request)
        {
            var entidad = await _repository.BuscarPorIdAsync(id);
            if (entidad == null)
                return ApiResponse<string>.Fail("No se encontró la marca a actualizar.");

            request.NombreMarca = request.NombreMarca.Trim().ToUpper();

            var existeNombre = await _repository.BuscarPorNombreAsync(request.NombreMarca);
            if (existeNombre != null && existeNombre.id_marca != id)
                return ApiResponse<string>.Fail("El nombre de marca ya está en uso por otra marca.");

            entidad.nombre_marca = request.NombreMarca;
            await _repository.ActualizarAsync(entidad);

            return ApiResponse<string>.SuccessResult(entidad.nombre_marca, "Marca actualizada con éxito.");
        }
    }
}
