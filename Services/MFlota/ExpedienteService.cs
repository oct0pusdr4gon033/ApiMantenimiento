using ApiMantenimiento.Models.DTOS.MFlota;
using ApiMantenimiento.Models.Entitys.MFlota;
using ApiMantenimiento.Models.Responses;
using ApiMantenimiento.Repositories.Interfaces.MFlota;
using ApiMantenimiento.Services.Interfaces.MFlota;
using AutoMapper;

namespace ApiMantenimiento.Services.MFlota
{
    public class ExpedienteService : IExpedienteService
    {
        private readonly IExpedienteRepository _expedienteRepo;
        private readonly IExpedienteDocumentoRepository _detalleRepo;
        private readonly ITipoDocumentoRepository _tipoDocRepo;
        private readonly IEquipoRepository _equipoRepo;
        private readonly IMapper _mapper;

        public ExpedienteService(
            IExpedienteRepository expedienteRepo,
            IExpedienteDocumentoRepository detalleRepo,
            ITipoDocumentoRepository tipoDocRepo,
            IEquipoRepository equipoRepo,
            IMapper mapper)
        {
            _expedienteRepo = expedienteRepo;
            _detalleRepo = detalleRepo;
            _tipoDocRepo = tipoDocRepo;
            _equipoRepo = equipoRepo;
            _mapper = mapper;
        }

        // ──────────────────────────────────────────────
        // CONSULTAS
        // ──────────────────────────────────────────────

        public async Task<ApiResponse<IEnumerable<ExpedienteResponse>>> ListarAsync()
        {
            var entidades = await _expedienteRepo.ListarAsync();
            var respuesta = _mapper.Map<IEnumerable<ExpedienteResponse>>(entidades);
            return ApiResponse<IEnumerable<ExpedienteResponse>>.SuccessResult(respuesta);
        }

        public async Task<ApiResponse<ExpedienteResponse>> BuscarPorCodigoAsync(string codigo)
        {
            var entidad = await _expedienteRepo.BuscarPorCodigoAsync(codigo.Trim().ToUpper());
            if (entidad == null)
                return ApiResponse<ExpedienteResponse>.Fail("No se encontró un expediente con ese código.");
            return ApiResponse<ExpedienteResponse>.SuccessResult(_mapper.Map<ExpedienteResponse>(entidad));
        }

        public async Task<ApiResponse<ExpedienteResponse>> BuscarPorEquipoAsync(int idEquipo)
        {
            var entidad = await _expedienteRepo.BuscarPorEquipoAsync(idEquipo);
            if (entidad == null)
                return ApiResponse<ExpedienteResponse>.Fail($"No se encontró un expediente para el equipo con ID {idEquipo}.");
            return ApiResponse<ExpedienteResponse>.SuccessResult(_mapper.Map<ExpedienteResponse>(entidad));
        }

        // ──────────────────────────────────────────────
        // MUTACIONES
        // ──────────────────────────────────────────────

        public async Task<ApiResponse<string>> CrearExpedienteAsync(ExpedienteRequest request)
        {
            request.CodigoExp = request.CodigoExp.Trim().ToUpper();

            // Validar que el equipo existe
            var equipo = await _equipoRepo.BuscarPorIdAsync(request.IdEquipo);
            if (equipo == null)
                return ApiResponse<string>.Fail($"No existe un equipo con ID {request.IdEquipo}.");

            // Validar que el equipo no tenga ya un expediente (relación 1 a 1)
            var expedienteExistente = await _expedienteRepo.BuscarPorEquipoAsync(request.IdEquipo);
            if (expedienteExistente != null)
                return ApiResponse<string>.Fail($"El equipo con ID {request.IdEquipo} ya tiene un expediente asignado ({expedienteExistente.codigo_exp}).");

            // Validar código único
            var codigoExistente = await _expedienteRepo.BuscarPorCodigoAsync(request.CodigoExp);
            if (codigoExistente != null)
                return ApiResponse<string>.Fail($"El código de expediente '{request.CodigoExp}' ya está registrado.");

            var nuevo = _mapper.Map<Expediente>(request);
            await _expedienteRepo.AgregarAsync(nuevo);

            return ApiResponse<string>.SuccessResult(nuevo.codigo_exp, "Expediente creado con éxito.");
        }

        public async Task<ApiResponse<string>> InsertarDocumentoAsync(ExpedienteDocumentoRequest request)
        {
            request.CodigoExp = request.CodigoExp.Trim().ToUpper();
            request.CodTipoDocumento = request.CodTipoDocumento.Trim().ToUpper();

            // Validar que las fechas sean coherentes
            if (request.FechaVencimiento <= request.FechaRegistro)
                return ApiResponse<string>.Fail("La fecha de vencimiento debe ser posterior a la fecha de registro.");

            // Validar que el expediente existe
            var expediente = await _expedienteRepo.BuscarPorCodigoAsync(request.CodigoExp);
            if (expediente == null)
                return ApiResponse<string>.Fail($"No existe un expediente con código '{request.CodigoExp}'.");

            // Validar que el tipo de documento existe
            var tipoDoc = await _tipoDocRepo.BuscarPorCodigoAsync(request.CodTipoDocumento);
            if (tipoDoc == null)
                return ApiResponse<string>.Fail($"No existe un tipo de documento con código '{request.CodTipoDocumento}'.");

            var detalle = _mapper.Map<ExpedienteDocumento>(request);
            var resultado = await _detalleRepo.AgregarAsync(detalle);

            return ApiResponse<string>.SuccessResult(
                resultado.id_expediente_documento.ToString(),
                "Documento insertado al expediente con éxito.");
        }

        public async Task<ApiResponse<ExpedienteDocumentoResponse>> ObtenerDocumentoPorIdAsync(int id)
        {
            var entidad = await _detalleRepo.BuscarPorIdAsync(id);
            if (entidad == null)
                return ApiResponse<ExpedienteDocumentoResponse>.Fail($"No se encontró el documento con ID {id}.");
            return ApiResponse<ExpedienteDocumentoResponse>.SuccessResult(_mapper.Map<ExpedienteDocumentoResponse>(entidad));
        }

        public async Task<ApiResponse<string>> ActualizarDocumentoAsync(int idDocumento, ExpedienteDocumentoRequest request)
        {
            var documento = await _detalleRepo.BuscarPorIdAsync(idDocumento);
            if (documento == null)
                return ApiResponse<string>.Fail($"No se encontró el documento con ID {idDocumento}.");

            if (documento.cod_tipo_documento != request.CodTipoDocumento)
            {
                var tipoDoc = await _tipoDocRepo.BuscarPorCodigoAsync(request.CodTipoDocumento);
                if (tipoDoc == null)
                    return ApiResponse<string>.Fail($"No existe un tipo de documento con código '{request.CodTipoDocumento}'.");
            }

            if (request.FechaVencimiento <= request.FechaRegistro)
                return ApiResponse<string>.Fail("La fecha de vencimiento debe ser posterior a la fecha de registro.");

            documento.cod_tipo_documento = request.CodTipoDocumento;
            documento.fecha_registro = request.FechaRegistro;
            documento.fecha_vencimiento = request.FechaVencimiento;
            documento.documento_url = request.DocumentoUrl;

            await _detalleRepo.GuardarAsync();
            return ApiResponse<string>.SuccessResult("Documento actualizado con éxito.");
        }
    }
}
