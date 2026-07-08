using ApiMantenimiento.Models.DTOS.MCompras;
using ApiMantenimiento.Models.Entitys.MCompras;
using ApiMantenimiento.Models.Entitys.MAlmacen;
using ApiMantenimiento.Repositories.Interfaces.MCompras;
using ApiMantenimiento.Repositories.Interfaces.MAlmacen;
using ApiMantenimiento.Services.Interfaces.MCompras;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiMantenimiento.Services.MCompras
{
    public class ComprasDocumentService : IComprasDocumentService
    {
        private readonly ISolicitudPedidoRepository _solpedRepository;
        private readonly ICotizacionRepository _cotizacionRepository;
        private readonly IOrdenCompraRepository _ordenCompraRepository;
        private readonly INotaIngresoRepository _notaIngresoRepository;
        private readonly IProveedorRepository _proveedorRepository;
        private readonly IMaterialRepository _materialRepository;
        private readonly ICategoriaMaterialRepository _categoriaRepository;
        private readonly IUnidadMedidaRepository _unidadRepository;
        private readonly IMovimientoInventarioRepository _movimientoRepository;

        public ComprasDocumentService(
            ISolicitudPedidoRepository solpedRepository,
            ICotizacionRepository cotizacionRepository,
            IOrdenCompraRepository ordenCompraRepository,
            INotaIngresoRepository notaIngresoRepository,
            IProveedorRepository proveedorRepository,
            IMaterialRepository materialRepository,
            ICategoriaMaterialRepository categoriaRepository,
            IUnidadMedidaRepository unidadRepository,
            IMovimientoInventarioRepository movimientoRepository)
        {
            _solpedRepository = solpedRepository;
            _cotizacionRepository = cotizacionRepository;
            _ordenCompraRepository = ordenCompraRepository;
            _notaIngresoRepository = notaIngresoRepository;
            _proveedorRepository = proveedorRepository;
            _materialRepository = materialRepository;
            _categoriaRepository = categoriaRepository;
            _unidadRepository = unidadRepository;
            _movimientoRepository = movimientoRepository;
        }

        // ==========================================
        // SOLICITUD DE PEDIDO (SOLPED)
        // ==========================================

        public async Task<IEnumerable<SolicitudPedidoResponse>> GetAllSolpedsAsync()
        {
            var entities = await _solpedRepository.GetAllAsync();
            return entities.Select(MapToSolpedResponse);
        }

        public async Task<SolicitudPedidoResponse> GetSolpedByIdAsync(int id)
        {
            var entity = await _solpedRepository.GetByIdAsync(id);
            if (entity == null) throw new Exception("Solicitud de Pedido no encontrada.");
            return MapToSolpedResponse(entity);
        }

        public async Task<SolicitudPedidoResponse> CrearSolicitudPedidoAsync(SolicitudPedidoRequest request)
        {
            if (string.IsNullOrEmpty(request.dni_empleado))
                throw new ArgumentException("El DNI de empleado es obligatorio.");

            // Create SOLPED code (e.g. SOL-yyyyMMdd-XXXX)
            var count = (await _solpedRepository.GetAllAsync()).Count();
            var code = $"SOL-{DateTime.Now:yyyyMMdd}-{count + 1:D4}";

            var entity = new SolicitudPedido
            {
                cod_solicitud = code,
                dni_empleado = request.dni_empleado,
                fecha_creacion = DateTime.Now,
                estado = "PENDIENTE"
            };

            foreach (var det in request.detalles)
            {
                string rucProv = det.ruc_proveedor;

                // Check if inline supplier needs registration
                if (det.nuevo_proveedor != null && !string.IsNullOrEmpty(det.nuevo_proveedor.ruc))
                {
                    var provExists = await _proveedorRepository.ExistsByRucAsync(det.nuevo_proveedor.ruc);
                    if (!provExists)
                    {
                        var newProv = new Proveedor
                        {
                            ruc = det.nuevo_proveedor.ruc,
                            razon_social = det.nuevo_proveedor.razon_social,
                            nombre_comercial = det.nuevo_proveedor.nombre_comercial,
                            direccion = det.nuevo_proveedor.direccion,
                            correo = det.nuevo_proveedor.correo,
                            telefono = det.nuevo_proveedor.telefono,
                            estado = "ACTIVO"
                        };
                        await _proveedorRepository.AddAsync(newProv);
                        rucProv = newProv.ruc;
                    }
                }

                entity.Detalles.Add(new SolicitudPedidoDetalle
                {
                    id_material = det.id_material,
                    cod_materia = det.cod_materia,
                    nombre = det.nombre,
                    id_categoria = det.id_categoria,
                    id_unidad = det.id_unidad,
                    stock_minimo = det.stock_minimo,
                    cantidad_pedida = det.cantidad_pedida,
                    ruc_proveedor = rucProv,
                    es_nuevo_producto = det.es_nuevo_producto,
                    especificaciones = det.especificaciones
                });
            }

            await _solpedRepository.AddAsync(entity);
            
            // Reload to get Empleado, Material, etc. navigation details
            var reloaded = await _solpedRepository.GetByIdAsync(entity.id_solicitud_pedido);
            return MapToSolpedResponse(reloaded);
        }

        public async Task<CotizacionResponse> AprobarSolicitudPedidoAsync(int id)
        {
            var solped = await _solpedRepository.GetByIdAsync(id);
            if (solped == null) throw new Exception("Solicitud de Pedido no encontrada.");

            if (solped.estado != "PENDIENTE")
                throw new Exception($"La solicitud ya está en estado {solped.estado}.");

            solped.estado = "APROBADO";

            // List of details to update and register new materials
            foreach (var detail in solped.Detalles)
            {
                if (detail.es_nuevo_producto && detail.id_material == null)
                {
                    // If no unit is specified, find a default UoM or use 1
                    int idUnidad = detail.id_unidad ?? 1;
                    var uom = await _unidadRepository.GetByIdAsync(idUnidad);
                    if (uom == null)
                    {
                        var allUom = await _unidadRepository.GetAllAsync();
                        if (allUom.Any())
                        {
                            idUnidad = allUom.First().id_unidad;
                        }
                        else
                        {
                            throw new Exception("No existen unidades de medida registradas en el sistema para registrar el nuevo producto.");
                        }
                    }

                    // Check category
                    int idCat = detail.id_categoria ?? 1;
                    var cat = await _categoriaRepository.GetByIdAsync(idCat);
                    if (cat == null)
                    {
                        var allCat = await _categoriaRepository.GetAllAsync();
                        if (allCat.Any())
                        {
                            idCat = allCat.First().id_categoria;
                        }
                        else
                        {
                            throw new Exception("No existen categorías de material registradas en el sistema.");
                        }
                    }

                    // Ensure unique cod_materia
                    string codMat = detail.cod_materia;
                    if (await _materialRepository.ExistsByCodMateriaAsync(codMat))
                    {
                        codMat = $"{codMat}-{DateTime.Now:ss}";
                    }

                    var material = new Material
                    {
                        cod_materia = codMat,
                        descripcion = detail.nombre, // Maps name to description in Material table
                        id_categoria = idCat,
                        id_unidad = idUnidad,
                        stock = 0, // Initial stock is 0
                        stock_minimo = detail.stock_minimo,
                        precio_actual = 0,
                        estado = "ACTIVO"
                    };

                    await _materialRepository.AddAsync(material);
                    detail.id_material = material.id_material;
                }
            }

            await _solpedRepository.UpdateAsync(solped);

            // Group SOLPED details by supplier to create Cotizaciones
            var detailsBySupplier = solped.Detalles.GroupBy(d => d.ruc_proveedor);
            Cotizacion firstCotizacion = null;

            foreach (var group in detailsBySupplier)
            {
                string ruc = group.Key;

                // If no supplier is set, default to the first seeded one
                if (string.IsNullOrEmpty(ruc))
                {
                    var firstProv = (await _proveedorRepository.GetAllAsync()).FirstOrDefault();
                    ruc = firstProv?.ruc ?? "20502123456"; // Sodimac default fallback
                }

                var count = (await _cotizacionRepository.GetAllAsync()).Count();
                var cotCode = $"COT-{DateTime.Now:yyyyMMdd}-{count + 1:D4}";

                var cotizacion = new Cotizacion
                {
                    nro_cotizacion = cotCode,
                    id_solicitud_pedido = solped.id_solicitud_pedido,
                    ruc_proveedor = ruc,
                    fecha_cotizacion = DateTime.Now,
                    estado = "PENDIENTE",
                    total = 0
                };

                foreach (var detail in group)
                {
                    if (detail.id_material.HasValue)
                    {
                        // Get current price of material to populate as estimate
                        var mat = await _materialRepository.GetByIdAsync(detail.id_material.Value);
                        decimal unitPrice = mat?.precio_actual ?? 0;

                        cotizacion.Detalles.Add(new CotizacionDetalle
                        {
                            id_material = detail.id_material.Value,
                            cantidad = detail.cantidad_pedida,
                            precio_unitario = unitPrice,
                            subtotal = detail.cantidad_pedida * unitPrice
                        });
                    }
                }

                cotizacion.total = cotizacion.Detalles.Sum(d => d.subtotal);
                await _cotizacionRepository.AddAsync(cotizacion);

                if (firstCotizacion == null)
                {
                    firstCotizacion = cotizacion;
                }
            }

            if (firstCotizacion == null)
                throw new Exception("No se pudieron generar cotizaciones debido a que no hay materiales válidos en la solicitud.");

            // Reload first generated quote to return
            var reloadedCot = await _cotizacionRepository.GetByIdAsync(firstCotizacion.id_cotizacion);
            return MapToCotizacionResponse(reloadedCot);
        }

        // ==========================================
        // COTIZACIONES
        // ==========================================

        public async Task<IEnumerable<CotizacionResponse>> GetAllCotizacionesAsync()
        {
            var entities = await _cotizacionRepository.GetAllAsync();
            return entities.Select(MapToCotizacionResponse);
        }

        public async Task<CotizacionResponse> GetCotizacionByIdAsync(int id)
        {
            var entity = await _cotizacionRepository.GetByIdAsync(id);
            if (entity == null) throw new Exception("Cotización no encontrada.");
            return MapToCotizacionResponse(entity);
        }

        public async Task<CotizacionResponse> CrearCotizacionAsync(CotizacionRequest request)
        {
            if (string.IsNullOrEmpty(request.ruc_proveedor))
                throw new ArgumentException("El proveedor es obligatorio.");

            var count = (await _cotizacionRepository.GetAllAsync()).Count();
            var cotCode = $"COT-{DateTime.Now:yyyyMMdd}-{count + 1:D4}";

            var entity = new Cotizacion
            {
                nro_cotizacion = cotCode,
                id_solicitud_pedido = request.id_solicitud_pedido,
                ruc_proveedor = request.ruc_proveedor,
                fecha_cotizacion = DateTime.Now,
                estado = "PENDIENTE"
            };

            foreach (var det in request.detalles ?? new List<CotizacionDetalleRequest>())
            {
                entity.Detalles.Add(new CotizacionDetalle
                {
                    id_material = det.id_material,
                    cantidad = det.cantidad,
                    precio_unitario = det.precio_unitario,
                    subtotal = det.cantidad * det.precio_unitario
                });
            }

            entity.total = entity.Detalles.Sum(d => d.subtotal);
            await _cotizacionRepository.AddAsync(entity);

            var reloaded = await _cotizacionRepository.GetByIdAsync(entity.id_cotizacion);
            return MapToCotizacionResponse(reloaded);
        }

        public async Task<CotizacionResponse> ActualizarCotizacionAsync(int id, CotizacionUpdateRequest request)
        {
            var cotizacion = await _cotizacionRepository.GetByIdAsync(id);
            if (cotizacion == null) throw new Exception("Cotización no encontrada.");

            if (cotizacion.estado != "PENDIENTE")
                throw new Exception($"No se puede editar la cotización porque está en estado {cotizacion.estado}.");

            foreach (var reqDet in request.detalles)
            {
                var detEntity = cotizacion.Detalles.FirstOrDefault(d => d.id_cotizacion_detalle == reqDet.id_cotizacion_detalle);
                if (detEntity != null)
                {
                    detEntity.precio_unitario = reqDet.precio_unitario;
                    detEntity.subtotal = detEntity.cantidad * reqDet.precio_unitario;
                }
            }

            cotizacion.total = cotizacion.Detalles.Sum(d => d.subtotal);
            await _cotizacionRepository.UpdateAsync(cotizacion);

            var reloaded = await _cotizacionRepository.GetByIdAsync(cotizacion.id_cotizacion);
            return MapToCotizacionResponse(reloaded);
        }

        public async Task<OrdenCompraResponse> AprobarCotizacionAsync(int id)
        {
            var cotizacion = await _cotizacionRepository.GetByIdAsync(id);
            if (cotizacion == null) throw new Exception("Cotización no encontrada.");

            if (cotizacion.estado != "PENDIENTE")
                throw new Exception($"La cotización ya está en estado {cotizacion.estado}.");

            cotizacion.estado = "APROBADO";
            await _cotizacionRepository.UpdateAsync(cotizacion);

            // Convert to Purchase Order
            var count = (await _ordenCompraRepository.GetAllAsync()).Count();
            var ocCode = $"OC-{DateTime.Now:yyyyMMdd}-{count + 1:D4}";

            var orden = new OrdenCompra
            {
                nro_orden = ocCode,
                id_cotizacion = cotizacion.id_cotizacion,
                ruc_proveedor = cotizacion.ruc_proveedor,
                fecha_orden = DateTime.Now,
                estado = "PENDIENTE",
                total = cotizacion.total
            };

            foreach (var det in cotizacion.Detalles)
            {
                orden.Detalles.Add(new OrdenCompraDetalle
                {
                    id_material = det.id_material,
                    cantidad = det.cantidad,
                    precio_unitario = det.precio_unitario,
                    subtotal = det.subtotal
                });
            }

            await _ordenCompraRepository.AddAsync(orden);

            var reloadedOc = await _ordenCompraRepository.GetByIdAsync(orden.id_orden_compra);
            return MapToOrdenCompraResponse(reloadedOc);
        }

        // ==========================================
        // ORDEN DE COMPRA
        // ==========================================

        public async Task<IEnumerable<OrdenCompraResponse>> GetAllOrdenesCompraAsync()
        {
            var entities = await _ordenCompraRepository.GetAllAsync();
            return entities.Select(MapToOrdenCompraResponse);
        }

        public async Task<OrdenCompraResponse> GetOrdenCompraByIdAsync(int id)
        {
            var entity = await _ordenCompraRepository.GetByIdAsync(id);
            if (entity == null) throw new Exception("Orden de Compra no encontrada.");
            return MapToOrdenCompraResponse(entity);
        }

        public async Task<OrdenCompraResponse> CrearOrdenCompraAsync(OrdenCompraRequest request)
        {
            if (string.IsNullOrEmpty(request.ruc_proveedor))
                throw new ArgumentException("El proveedor es obligatorio.");

            var count = (await _ordenCompraRepository.GetAllAsync()).Count();
            var ocCode = $"OC-{DateTime.Now:yyyyMMdd}-{count + 1:D4}";

            var entity = new OrdenCompra
            {
                nro_orden = ocCode,
                id_cotizacion = request.id_cotizacion,
                ruc_proveedor = request.ruc_proveedor,
                fecha_orden = DateTime.Now,
                estado = "PENDIENTE"
            };

            foreach (var det in request.detalles ?? new List<OrdenCompraDetalleRequest>())
            {
                entity.Detalles.Add(new OrdenCompraDetalle
                {
                    id_material = det.id_material,
                    cantidad = det.cantidad,
                    precio_unitario = det.precio_unitario,
                    subtotal = det.cantidad * det.precio_unitario
                });
            }

            entity.total = entity.Detalles.Sum(d => d.subtotal);
            await _ordenCompraRepository.AddAsync(entity);

            var reloaded = await _ordenCompraRepository.GetByIdAsync(entity.id_orden_compra);
            return MapToOrdenCompraResponse(reloaded);
        }

        public async Task<OrdenCompraResponse> AprobarOrdenCompraAsync(int id)
        {
            var orden = await _ordenCompraRepository.GetByIdAsync(id);
            if (orden == null) throw new Exception("Orden de Compra no encontrada.");

            if (orden.estado != "PENDIENTE")
                throw new Exception($"La orden de compra ya está en estado {orden.estado}.");

            orden.estado = "APROBADO";
            await _ordenCompraRepository.UpdateAsync(orden);
            return MapToOrdenCompraResponse(orden);
        }

        public async Task<NotaIngresoResponse> ProcesarNotaIngresoAsync(int idOrdenCompra, string observaciones)
        {
            var orden = await _ordenCompraRepository.GetByIdAsync(idOrdenCompra);
            if (orden == null) throw new Exception("Orden de Compra no encontrada.");

            if (orden.estado != "APROBADO")
                throw new Exception("La orden de compra debe estar APROBADA para poder ingresar a almacén.");

            orden.estado = "RECIBIDO";
            await _ordenCompraRepository.UpdateAsync(orden);

            // Generate Receiving Note
            var count = (await _notaIngresoRepository.GetAllAsync()).Count();
            var niCode = $"NI-{DateTime.Now:yyyyMMdd}-{count + 1:D4}";

            var nota = new NotaIngreso
            {
                nro_nota = niCode,
                id_orden_compra = orden.id_orden_compra,
                fecha_ingreso = DateTime.Now,
                estado = "PROCESADO",
                observaciones = observaciones ?? "Ingreso automático desde Orden de Compra."
            };

            foreach (var det in orden.Detalles)
            {
                var material = await _materialRepository.GetByIdAsync(det.id_material);
                if (material != null)
                {
                    // 1. Update stock
                    material.stock += det.cantidad;

                    // 2. Update current price
                    material.precio_actual = det.precio_unitario;
                    await _materialRepository.UpdateAsync(material);

                    // 3. Register price history
                    var precioHistorial = new HistorialPrecio
                    {
                        id_material = material.id_material,
                        precio = det.precio_unitario,
                        fecha_registro = DateTime.Now
                    };
                    material.HistorialPrecios.Add(precioHistorial);

                    // 4. Register warehouse movement (Kardex)
                    var movimiento = new MovimientoInventario
                    {
                        id_material = material.id_material,
                        fecha = DateTime.Now,
                        tipo_movimiento = "ENTRADA",
                        cantidad = det.cantidad,
                        saldo_stock = material.stock,
                        origen_tipo = "NOTA_INGRESO",
                        origen_referencia = niCode,
                        responsable = "Sistema (Ingreso Almacén)",
                        observaciones = $"Ingreso por Orden de Compra {orden.nro_orden}"
                    };
                    await _movimientoRepository.AddAsync(movimiento);
                }

                nota.Detalles.Add(new NotaIngresoDetalle
                {
                    id_material = det.id_material,
                    cantidad = det.cantidad,
                    precio_unitario = det.precio_unitario
                });
            }

            await _notaIngresoRepository.AddAsync(nota);

            var reloadedNi = await _notaIngresoRepository.GetByIdAsync(nota.id_nota_ingreso);
            return MapToNotaIngresoResponse(reloadedNi);
        }

        // ==========================================
        // NOTAS DE INGRESO
        // ==========================================

        public async Task<IEnumerable<NotaIngresoResponse>> GetAllNotasIngresoAsync()
        {
            var entities = await _notaIngresoRepository.GetAllAsync();
            return entities.Select(MapToNotaIngresoResponse);
        }

        public async Task<NotaIngresoResponse> GetNotaIngresoByIdAsync(int id)
        {
            var entity = await _notaIngresoRepository.GetByIdAsync(id);
            if (entity == null) throw new Exception("Nota de Ingreso no encontrada.");
            return MapToNotaIngresoResponse(entity);
        }

        // ==========================================
        // MAPPINGS
        // ==========================================

        private SolicitudPedidoResponse MapToSolpedResponse(SolicitudPedido s)
        {
            return new SolicitudPedidoResponse
            {
                id_solicitud_pedido = s.id_solicitud_pedido,
                cod_solicitud = s.cod_solicitud,
                dni_empleado = s.dni_empleado,
                nombre_empleado = s.Empleado != null ? $"{s.Empleado.nombre} {s.Empleado.apellido1} {s.Empleado.apellido2}".Trim() : string.Empty,
                fecha_creacion = s.fecha_creacion,
                estado = s.estado,
                detalles = s.Detalles.Select(d => new SolicitudPedidoDetalleResponse
                {
                    id_detalle = d.id_detalle,
                    id_material = d.id_material,
                    cod_materia = d.cod_materia,
                    nombre = d.nombre,
                    id_categoria = d.id_categoria,
                    nombre_categoria = d.CategoriaMaterial?.nombre_categoria,
                    id_unidad = d.id_unidad,
                    nombre_unidad = d.UnidadMedida?.nombre_unidad,
                    stock_minimo = d.stock_minimo,
                    cantidad_pedida = d.cantidad_pedida,
                    ruc_proveedor = d.ruc_proveedor,
                    razon_social_proveedor = d.Proveedor?.razon_social,
                    es_nuevo_producto = d.es_nuevo_producto,
                    especificaciones = d.especificaciones
                }).ToList()
            };
        }

        private CotizacionResponse MapToCotizacionResponse(Cotizacion c)
        {
            return new CotizacionResponse
            {
                id_cotizacion = c.id_cotizacion,
                nro_cotizacion = c.nro_cotizacion,
                id_solicitud_pedido = c.id_solicitud_pedido,
                cod_solicitud_pedido = c.SolicitudPedido?.cod_solicitud,
                ruc_proveedor = c.ruc_proveedor,
                razon_social_proveedor = c.Proveedor?.razon_social,
                fecha_cotizacion = c.fecha_cotizacion,
                estado = c.estado,
                total = c.total,
                detalles = c.Detalles.Select(d => new CotizacionDetalleResponse
                {
                    id_cotizacion_detalle = d.id_cotizacion_detalle,
                    id_material = d.id_material,
                    cod_materia = d.Material?.cod_materia,
                    descripcion_material = d.Material?.descripcion,
                    cantidad = d.cantidad,
                    precio_unitario = d.precio_unitario,
                    subtotal = d.subtotal
                }).ToList()
            };
        }

        private OrdenCompraResponse MapToOrdenCompraResponse(OrdenCompra o)
        {
            return new OrdenCompraResponse
            {
                id_orden_compra = o.id_orden_compra,
                nro_orden = o.nro_orden,
                id_cotizacion = o.id_cotizacion,
                nro_cotizacion = o.Cotizacion?.nro_cotizacion,
                ruc_proveedor = o.ruc_proveedor,
                razon_social_proveedor = o.Proveedor?.razon_social,
                fecha_orden = o.fecha_orden,
                estado = o.estado,
                total = o.total,
                detalles = o.Detalles.Select(d => new OrdenCompraDetalleResponse
                {
                    id_orden_detalle = d.id_orden_detalle,
                    id_material = d.id_material,
                    cod_materia = d.Material?.cod_materia,
                    descripcion_material = d.Material?.descripcion,
                    cantidad = d.cantidad,
                    precio_unitario = d.precio_unitario,
                    subtotal = d.subtotal
                }).ToList()
            };
        }

        private NotaIngresoResponse MapToNotaIngresoResponse(NotaIngreso n)
        {
            return new NotaIngresoResponse
            {
                id_nota_ingreso = n.id_nota_ingreso,
                nro_nota = n.nro_nota,
                id_orden_compra = n.id_orden_compra,
                nro_orden_compra = n.OrdenCompra?.nro_orden,
                fecha_ingreso = n.fecha_ingreso,
                estado = n.estado,
                observaciones = n.observaciones,
                detalles = n.Detalles.Select(d => new NotaIngresoDetalleResponse
                {
                    id_nota_detalle = d.id_nota_detalle,
                    id_material = d.id_material,
                    cod_materia = d.Material?.cod_materia,
                    descripcion_material = d.Material?.descripcion,
                    cantidad = d.cantidad,
                    precio_unitario = d.precio_unitario
                }).ToList()
            };
        }
    }
}
