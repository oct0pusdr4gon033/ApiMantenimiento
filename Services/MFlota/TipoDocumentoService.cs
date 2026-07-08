using ApiMantenimiento.Models.DTOS.MFlota;
using ApiMantenimiento.Models.Entitys.MFlota;
using ApiMantenimiento.Models.Responses;
using ApiMantenimiento.Repositories.Interfaces.MFlota;
using ApiMantenimiento.Services.Interfaces.MFlota;
using AutoMapper;

namespace ApiMantenimiento.Services.MFlota
{
    public class TipoDocumentoService : ITipoDocumentoService
    {
        private readonly ITipoDocumentoRepository _repository;
        private readonly IMapper _mapper;

        public TipoDocumentoService(ITipoDocumentoRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<IEnumerable<TipoDocumentoResponse>>> ListarAsync()
        {
            var entidades = await _repository.ListarAsync();
            var respuesta = _mapper.Map<IEnumerable<TipoDocumentoResponse>>(entidades);
            return ApiResponse<IEnumerable<TipoDocumentoResponse>>.SuccessResult(respuesta);
        }

        public async Task<ApiResponse<TipoDocumentoResponse>> BuscarPorCodigoAsync(string codigo)
        {
            var entidad = await _repository.BuscarPorCodigoAsync(codigo.Trim().ToUpper());
            if (entidad == null)
                return ApiResponse<TipoDocumentoResponse>.Fail("No se encontró el tipo de documento con ese código.");
            return ApiResponse<TipoDocumentoResponse>.SuccessResult(_mapper.Map<TipoDocumentoResponse>(entidad));
        }

        public async Task<ApiResponse<string>> AgregarAsync(TipoDocumentoRequest request)
        {
            request.CodTipoDocumento = request.CodTipoDocumento.Trim().ToUpper();
            request.NombreTipo = request.NombreTipo.Trim();

            // Validar que el código no esté duplicado
            var existe = await _repository.BuscarPorCodigoAsync(request.CodTipoDocumento);
            if (existe != null)
                return ApiResponse<string>.Fail($"Ya existe un tipo de documento con el código '{request.CodTipoDocumento}'.");

            var nuevo = _mapper.Map<TipoDocumento>(request);
            await _repository.AgregarAsync(nuevo);

            return ApiResponse<string>.SuccessResult(nuevo.cod_tipo_documento, "Tipo de documento registrado con éxito.");
        }

        public async Task<ApiResponse<string>> ActualizarAsync(string codigo, TipoDocumentoRequest request)
        {
            var entidad = await _repository.BuscarPorCodigoAsync(codigo.Trim().ToUpper());
            if (entidad == null)
                return ApiResponse<string>.Fail("No se encontró el tipo de documento a actualizar.");

            // Si el usuario cambia el código, validar que el nuevo no exista
            var nuevocodigo = request.CodTipoDocumento.Trim().ToUpper();
            if (nuevocodigo != entidad.cod_tipo_documento)
            {
                var duplicado = await _repository.BuscarPorCodigoAsync(nuevocodigo);
                if (duplicado != null)
                    return ApiResponse<string>.Fail($"El código '{nuevocodigo}' ya está en uso por otro tipo de documento.");
            }

            entidad.cod_tipo_documento = nuevocodigo;
            entidad.nombre_tipo = request.NombreTipo.Trim();

            await _repository.ActualizarAsync(entidad);
            return ApiResponse<string>.SuccessResult(entidad.cod_tipo_documento, "Tipo de documento actualizado con éxito.");
        }
    }
}
