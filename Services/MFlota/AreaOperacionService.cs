using ApiMantenimiento.Models.DTOS.MFlota;
using ApiMantenimiento.Models.Entitys.MFlota;
using ApiMantenimiento.Models.Responses;
using ApiMantenimiento.Repositories.Interfaces.MFlota;
using ApiMantenimiento.Repositories.MFlota;
using ApiMantenimiento.Services.Interfaces;
using ApiMantenimiento.Services.Interfaces.MFlota;
using AutoMapper;
using Azure.Core;
using System.Text.RegularExpressions;

namespace ApiMantenimiento.Services.MFlota
{
    public class AreaOperacionService : IAreaOperacionService
    {
        private readonly IAreaOperacionRepository _repository;
        private readonly IMapper _mapper;
        public AreaOperacionService(IAreaOperacionRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<IEnumerable<AreaOperacionResponse>>> ListarAreasAsync()
        {
            // 1. El repositorio trae las entidades de la base de datos
            var areasEntidad = await _repository.ListarAreasAsync();

            // 2. AutoMapper las transforma en la lista de respuestas DTO
            var areasResponse = _mapper.Map<IEnumerable<AreaOperacionResponse>>(areasEntidad);

            // 3. Devolvemos el resultado envuelto en tu ApiResponse genérica
            return ApiResponse<IEnumerable<AreaOperacionResponse>>.SuccessResult(areasResponse);
        }
        
        public async Task<ApiResponse<AreaOperacionResponse>> BuscarPorCodigoAsync(string codigoArea)
        {
            var areaEntidad = await _repository.BuscarPorCodigoAsync(codigoArea);
            if (areaEntidad == null)
            {
                return ApiResponse<AreaOperacionResponse>.Fail("No se encontró un área de operación con el código proporcionado.");
            }
            var areaResponse = _mapper.Map<AreaOperacionResponse>(areaEntidad);
            return ApiResponse<AreaOperacionResponse>.SuccessResult(areaResponse);
        }
        public async Task<ApiResponse<AreaOperacionResponse>> BuscarPorNombreAsync(string nombreArea)
        {
            var areaEntidad = await _repository.BuscarPorNombreAsync(nombreArea);
            if (areaEntidad == null)
            {
                return ApiResponse<AreaOperacionResponse>.Fail("No se encontró un área de operación con el nombre proporcionado.");
            }
            var areaResponse = _mapper.Map<AreaOperacionResponse>(areaEntidad);
            return ApiResponse<AreaOperacionResponse>.SuccessResult(areaResponse);
        }

        public async Task<ApiResponse<string>> AgregarAreaAsync(AreaOperacionRequest request)
        {
            request.CodigoArea = request.CodigoArea.Trim().ToUpper();
            request.NombreArea = request.NombreArea.Trim().ToUpper();
            request.CodigoArea = Regex.Replace(request.CodigoArea, @"\s+", "");
        
            if (!Regex.IsMatch(request.CodigoArea, @"^[A-Z0-9]+-[A-Z0-9]+$"))
            {
                return ApiResponse<string>.Fail("El formato del código es inválido. Solo se permite LETRAS, NÚMEROS y un GUION (Ej: AREA-001) sin caracteres especiales.");
            }

            var existeArea = await _repository.BuscarPorCodigoAsync(request.CodigoArea);
            if (existeArea != null)
            {
                return ApiResponse<string>.Fail("El código de área ya está registrado en el sistema.");
            }

            var existeNombre = await _repository.BuscarPorNombreAsync(request.NombreArea);
            if (existeNombre != null)
            {
                return ApiResponse<string>.Fail("El nombre de área ya está registrado en el sistema.");
            }

            // Mapeamos el Request que envió el cliente hacia la entidad de la base de datos
            var nuevaArea = _mapper.Map<AreaOperacion>(request);

            // Guardamos a través del repositorio
            await _repository.AgregarAsync(nuevaArea);
            await _repository.GuardarAsync();

            return ApiResponse<string>.SuccessResult(nuevaArea.cod_area_ope, "Área de operación creada con éxito.");
        }

        public async Task<ApiResponse<string>> ActualizarAreaAsync(string codigoArea, AreaOperacionRequest request)
        {
            var entidad = await _repository.BuscarPorCodigoAsync(codigoArea.Trim().ToUpper());
            if (entidad == null)
                return ApiResponse<string>.Fail("No se encontró el área de operación a actualizar.");

            request.NombreArea = request.NombreArea.Trim().ToUpper();

            // Validar nombre duplicado excluyendo el propio registro
            var existeNombre = await _repository.BuscarPorNombreAsync(request.NombreArea);
            if (existeNombre != null && existeNombre.cod_area_ope != entidad.cod_area_ope)
                return ApiResponse<string>.Fail("El nombre de área ya está en uso por otra área de operación.");

            entidad.nomb_area = request.NombreArea;
            await _repository.ActualizarAsync(entidad);

            return ApiResponse<string>.SuccessResult(entidad.cod_area_ope, "Área de operación actualizada con éxito.");
        }
    }
}
