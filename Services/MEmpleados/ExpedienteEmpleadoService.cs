using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiMantenimiento.Models.DTOS.MEmpleados;
using ApiMantenimiento.Models.Entitys.MEmpleados;
using ApiMantenimiento.Repositories.Interfaces.MEmpleados;
using ApiMantenimiento.Services.Interfaces.MEmpleados;

namespace ApiMantenimiento.Services.MEmpleados
{
    public class ExpedienteEmpleadoService : IExpedienteEmpleadoService
    {
        private readonly IExpedienteEmpleadoRepository _expedienteRepository;
        private readonly IEmpleadoRepository _empleadoRepository;

        public ExpedienteEmpleadoService(
            IExpedienteEmpleadoRepository expedienteRepository,
            IEmpleadoRepository empleadoRepository)
        {
            _expedienteRepository = expedienteRepository;
            _empleadoRepository = empleadoRepository;
        }

        public async Task<IEnumerable<ExpedienteEmpleadoResponse>> ObtenerTodosAsync()
        {
            var expedientes = await _expedienteRepository.ObtenerTodosAsync();
            return expedientes.Select(MapearAExpedienteResponse);
        }

        public async Task<ExpedienteEmpleadoResponse> ObtenerPorDniAsync(string dni)
        {
            var expediente = await _expedienteRepository.ObtenerPorDniAsync(dni);
            if (expediente == null)
            {
                throw new Exception($"No se encontró un expediente para el DNI: {dni}");
            }
            return MapearAExpedienteResponse(expediente);
        }

        public async Task<ExpedienteEmpleadoResponse> ObtenerPorCodigoAsync(string codigo)
        {
            var expediente = await _expedienteRepository.ObtenerPorCodigoAsync(codigo);
            if (expediente == null)
            {
                throw new Exception($"No se encontró el expediente con código: {codigo}");
            }
            return MapearAExpedienteResponse(expediente);
        }

        public async Task<ExpedienteEmpleadoResponse> CrearExpedienteAsync(ExpedienteEmpleadoRequest request)
        {
            // Validar que el empleado existe
            var empleado = await _empleadoRepository.ObtenerPorDniAsync(request.DniEmpleado);
            if (empleado == null)
            {
                throw new Exception($"El empleado con DNI {request.DniEmpleado} no existe.");
            }

            // Validar si ya tiene un expediente
            var existeExpediente = await _expedienteRepository.ObtenerPorDniAsync(request.DniEmpleado);
            if (existeExpediente != null)
            {
                throw new Exception("El empleado ya tiene un expediente registrado.");
            }

            var nuevoExpediente = new ExpedienteEmpleado
            {
                codigo_exp_emp = request.CodigoExpEmp,
                dni_empleado = request.DniEmpleado
            };

            await _expedienteRepository.InsertarExpedienteAsync(nuevoExpediente);
            await _expedienteRepository.GuardarCambiosAsync();

            return await ObtenerPorCodigoAsync(nuevoExpediente.codigo_exp_emp);
        }

        public async Task<IEnumerable<TipoDocumentoEmpleadoResponse>> ObtenerTiposDocumentoAsync()
        {
            var tipos = await _expedienteRepository.ObtenerTiposDocumentoAsync();
            return tipos.Select(x => new TipoDocumentoEmpleadoResponse
            {
                CodTipoDocumentoEmp = x.cod_tipo_doc_emp,
                NombreTipo = x.nombre_tipo
            });
        }

        public async Task<TipoDocumentoEmpleadoResponse> CrearTipoDocumentoAsync(TipoDocumentoEmpleadoRequest request)
        {
            var tipos = await _expedienteRepository.ObtenerTiposDocumentoAsync();
            if (tipos.Any(x => x.cod_tipo_doc_emp == request.CodTipoDocumentoEmp))
            {
                throw new Exception("Ya existe un tipo de documento con ese código.");
            }

            var nuevoTipo = new TipoDocumentoEmpleado
            {
                cod_tipo_doc_emp = request.CodTipoDocumentoEmp,
                nombre_tipo = request.NombreTipo
            };

            await _expedienteRepository.InsertarTipoDocumentoAsync(nuevoTipo);
            await _expedienteRepository.GuardarCambiosAsync();

            return new TipoDocumentoEmpleadoResponse
            {
                CodTipoDocumentoEmp = nuevoTipo.cod_tipo_doc_emp,
                NombreTipo = nuevoTipo.nombre_tipo
            };
        }

        public async Task<IEnumerable<ExpedienteDocumentoEmpleadoResponse>> ObtenerDocumentosAsync(string codigoExp)
        {
            var documentos = await _expedienteRepository.ObtenerDocumentosPorExpedienteAsync(codigoExp);
            return documentos.Select(MapearADocumentoResponse);
        }

        public async Task<ExpedienteDocumentoEmpleadoResponse> AgregarDocumentoAsync(ExpedienteDocumentoEmpleadoRequest request)
        {
            var expediente = await _expedienteRepository.ObtenerPorCodigoAsync(request.CodigoExpEmp);
            if (expediente == null)
            {
                throw new Exception($"No se encontró el expediente con código: {request.CodigoExpEmp}");
            }

            var nuevoDoc = new ExpedienteDocumentoEmpleado
            {
                codigo_exp_emp = request.CodigoExpEmp,
                cod_tipo_doc_emp = request.CodTipoDocumentoEmp,
                fecha_registro = request.FechaRegistro,
                fecha_vencimiento = request.FechaVencimiento,
                documento_url = request.DocumentoUrl
            };

            await _expedienteRepository.InsertarDetalleDocumentoAsync(nuevoDoc);
            await _expedienteRepository.GuardarCambiosAsync();

            var docGuardado = await _expedienteRepository.ObtenerDetalleDocumentoAsync(nuevoDoc.id_exp_doc_emp);
            return MapearADocumentoResponse(docGuardado!);
        }

        public async Task<ExpedienteDocumentoEmpleadoResponse> ActualizarDocumentoAsync(int id, ExpedienteDocumentoEmpleadoRequest request)
        {
            var doc = await _expedienteRepository.ObtenerDetalleDocumentoAsync(id);
            if (doc == null)
            {
                throw new Exception($"No se encontró el documento con ID: {id}");
            }

            doc.cod_tipo_doc_emp = request.CodTipoDocumentoEmp;
            doc.fecha_registro = request.FechaRegistro;
            doc.fecha_vencimiento = request.FechaVencimiento;
            if (!string.IsNullOrEmpty(request.DocumentoUrl))
            {
                doc.documento_url = request.DocumentoUrl;
            }

            await _expedienteRepository.ActualizarDetalleDocumentoAsync(doc);
            await _expedienteRepository.GuardarCambiosAsync();

            var docActualizado = await _expedienteRepository.ObtenerDetalleDocumentoAsync(id);
            return MapearADocumentoResponse(docActualizado!);
        }

        public async Task EliminarDocumentoAsync(int idExpedienteDocumento)
        {
            await _expedienteRepository.EliminarDetalleDocumentoAsync(idExpedienteDocumento);
            await _expedienteRepository.GuardarCambiosAsync();
        }

        // --- MÉTODOS PRIVADOS DE MAPEO ---

        private ExpedienteEmpleadoResponse MapearAExpedienteResponse(ExpedienteEmpleado entity)
        {
            return new ExpedienteEmpleadoResponse
            {
                CodigoExpEmp = entity.codigo_exp_emp,
                DniEmpleado = entity.dni_empleado,
                CodigoEmpleado = entity.Empleado?.codigo_empleado ?? "",
                NombreCompleto = entity.Empleado != null 
                    ? $"{entity.Empleado.nombre} {entity.Empleado.apellido1} {entity.Empleado.apellido2}".Trim() 
                    : "",
                Documentos = entity.DetallesDocumento?
                    .Select(MapearADocumentoResponse)
                    .ToList() ?? new List<ExpedienteDocumentoEmpleadoResponse>()
            };
        }

        private ExpedienteDocumentoEmpleadoResponse MapearADocumentoResponse(ExpedienteDocumentoEmpleado entity)
        {
            return new ExpedienteDocumentoEmpleadoResponse
            {
                IdExpedienteDocumentoEmp = entity.id_exp_doc_emp,
                CodigoExpEmp = entity.codigo_exp_emp,
                CodTipoDocumentoEmp = entity.cod_tipo_doc_emp,
                NombreTipoDocumento = entity.TipoDocumentoEmpleado?.nombre_tipo ?? "",
                FechaRegistro = entity.fecha_registro,
                FechaVencimiento = entity.fecha_vencimiento,
                DocumentoUrl = entity.documento_url ?? ""
            };
        }
    }
}
