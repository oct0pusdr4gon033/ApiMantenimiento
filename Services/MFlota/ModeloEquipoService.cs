using ApiMantenimiento.Models.DTOS.MFlota;
using ApiMantenimiento.Models.Entitys.MFlota;
using ApiMantenimiento.Models.Responses;
using ApiMantenimiento.Repositories.Interfaces.MFlota;
using ApiMantenimiento.Services.Interfaces.MFlota;
using AutoMapper;

namespace ApiMantenimiento.Services.MFlota
{
    public class ModeloEquipoService : IModeloEquipoService
    {
        private readonly IModeloEquipoRepository _repository;
        private readonly IMarcaEquipoRepository _marcaRepo;
        private readonly ITipoEquipoRepository _tipoRepo;
        private readonly IMapper _mapper;

        public ModeloEquipoService(
            IModeloEquipoRepository repository,
            IMarcaEquipoRepository marcaRepo,
            ITipoEquipoRepository tipoRepo,
            IMapper mapper)
        {
            _repository = repository;
            _marcaRepo = marcaRepo;
            _tipoRepo = tipoRepo;
            _mapper = mapper;
        }

        public async Task<ApiResponse<IEnumerable<ModeloEquipoResponse>>> ListarAsync()
        {
            var entidades = await _repository.ListarAsync();
            var respuesta = _mapper.Map<IEnumerable<ModeloEquipoResponse>>(entidades);
            return ApiResponse<IEnumerable<ModeloEquipoResponse>>.SuccessResult(respuesta);
        }

        public async Task<ApiResponse<ModeloEquipoResponse>> BuscarPorIdAsync(int id)
        {
            var entidad = await _repository.BuscarPorIdAsync(id);
            if (entidad == null)
                return ApiResponse<ModeloEquipoResponse>.Fail("No se encontró el modelo de equipo con el ID proporcionado.");

            return ApiResponse<ModeloEquipoResponse>.SuccessResult(_mapper.Map<ModeloEquipoResponse>(entidad));
        }

        public async Task<ApiResponse<string>> AgregarAsync(ModeloEquipoRequest request)
        {
            request.NombreModelo = request.NombreModelo.Trim().ToUpper();

            // Validar que la marca existe
            var marca = await _marcaRepo.BuscarPorIdAsync(request.IdMarca);
            if (marca == null)
                return ApiResponse<string>.Fail($"No existe una marca con ID {request.IdMarca}.");

            // Validar que el tipo de equipo existe
            var tipo = await _tipoRepo.BuscarPorIdAsync(request.IdTipoEqp);
            if (tipo == null)
                return ApiResponse<string>.Fail($"No existe un tipo de equipo con ID {request.IdTipoEqp}.");

            // Validar nombre duplicado
            var existeNombre = await _repository.BuscarPorNombreAsync(request.NombreModelo);
            if (existeNombre != null)
                return ApiResponse<string>.Fail("El nombre del modelo ya está registrado en el sistema.");

            var nuevo = _mapper.Map<ModeloEquipo>(request);
            await _repository.AgregarAsync(nuevo);

            return ApiResponse<string>.SuccessResult(nuevo.nomb_modelo, "Modelo de equipo creado con éxito.");
        }

        public async Task<ApiResponse<string>> ActualizarAsync(int id, ModeloEquipoRequest request)
        {
            var entidad = await _repository.BuscarPorIdAsync(id);
            if (entidad == null)
                return ApiResponse<string>.Fail("No se encontró el modelo de equipo a actualizar.");

            request.NombreModelo = request.NombreModelo.Trim().ToUpper();

            // Validar que la nueva marca existe
            var marca = await _marcaRepo.BuscarPorIdAsync(request.IdMarca);
            if (marca == null)
                return ApiResponse<string>.Fail($"No existe una marca con ID {request.IdMarca}.");

            // Validar que el nuevo tipo existe
            var tipo = await _tipoRepo.BuscarPorIdAsync(request.IdTipoEqp);
            if (tipo == null)
                return ApiResponse<string>.Fail($"No existe un tipo de equipo con ID {request.IdTipoEqp}.");

            // Validar nombre duplicado excluyendo el propio registro
            var existeNombre = await _repository.BuscarPorNombreAsync(request.NombreModelo);
            if (existeNombre != null && existeNombre.id_modelo != id)
                return ApiResponse<string>.Fail("El nombre del modelo ya está en uso por otro modelo.");

            entidad.id_marca = request.IdMarca;
            entidad.id_tipo_eqp = request.IdTipoEqp;
            entidad.nomb_modelo = request.NombreModelo;

            await _repository.ActualizarAsync(entidad);
            return ApiResponse<string>.SuccessResult(entidad.nomb_modelo, "Modelo de equipo actualizado con éxito.");
        }
    }
}
