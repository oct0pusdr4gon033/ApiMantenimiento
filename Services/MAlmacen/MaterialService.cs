using ApiMantenimiento.Models.DTOS.MAlmacen;
using ApiMantenimiento.Models.Entitys.MAlmacen;
using ApiMantenimiento.Repositories.Interfaces.MAlmacen;
using ApiMantenimiento.Services.Interfaces.MAlmacen;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiMantenimiento.Services.MAlmacen
{
    public class MaterialService : IMaterialService
    {
        private readonly IMaterialRepository _repository;
        private readonly IUnidadMedidaRepository _unidadRepository;
        private readonly ICategoriaMaterialRepository _categoriaRepository;
        private readonly IMovimientoInventarioRepository _movRepository;

        public MaterialService(
            IMaterialRepository repository,
            IUnidadMedidaRepository unidadRepository,
            ICategoriaMaterialRepository categoriaRepository,
            IMovimientoInventarioRepository movRepository)
        {
            _repository = repository;
            _unidadRepository = unidadRepository;
            _categoriaRepository = categoriaRepository;
            _movRepository = movRepository;
        }

        public async Task<IEnumerable<MaterialResponse>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            return entities.Select(MapToResponse);
        }

        public async Task<IEnumerable<MaterialResponse>> BuscarMaterialesAsync(string cod_materia, string nombre, string estado, int? id_unidad, int? id_categoria)
        {
            var entities = await _repository.BuscarMaterialesAsync(cod_materia, nombre, estado, id_unidad, id_categoria);
            return entities.Select(MapToResponse);
        }

        public async Task<MaterialResponse> CreateAsync(MaterialRequest request)
        {
            if (await _repository.ExistsByCodMateriaAsync(request.cod_materia))
                throw new System.Exception("El código de material ya existe.");

            var unidad = await _unidadRepository.GetByIdAsync(request.id_unidad);
            if (unidad == null) throw new System.Exception("Unidad de Medida no encontrada.");

            var categoria = await _categoriaRepository.GetByIdAsync(request.id_categoria);
            if (categoria == null) throw new System.Exception("Categoría no encontrada.");

            var entity = new Material
            {
                id_unidad = request.id_unidad,
                id_categoria = request.id_categoria,
                cod_materia = request.cod_materia,
                descripcion = request.descripcion,
                stock = request.stock,
                estado = request.estado,
                UnidadMedida = unidad,
                CategoriaMaterial = categoria
            };

            await _repository.AddAsync(entity);

            // Registrar stock inicial en el Kardex si es mayor a cero
            if (entity.stock > 0)
            {
                var movimiento = new MovimientoInventario
                {
                    id_material = entity.id_material,
                    fecha = System.DateTime.Now,
                    tipo_movimiento = "ENTRADA",
                    cantidad = entity.stock,
                    saldo_stock = entity.stock,
                    origen_tipo = "INICIAL",
                    origen_referencia = "STOCK-INICIAL",
                    responsable = "Sistema (Creación)",
                    observaciones = "Registro de stock inicial al crear el material."
                };
                await _movRepository.AddAsync(movimiento);
            }

            return MapToResponse(entity);
        }

        public async Task<MaterialResponse> UpdateAsync(int id, MaterialUpdateRequest request)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) throw new System.Exception("Material no encontrado.");

            if (entity.cod_materia != request.cod_materia && await _repository.ExistsByCodMateriaAsync(request.cod_materia))
                throw new System.Exception("El código de material ya existe en otro registro.");

            var unidad = await _unidadRepository.GetByIdAsync(request.id_unidad);
            if (unidad == null) throw new System.Exception("Unidad de Medida no encontrada.");

            var categoria = await _categoriaRepository.GetByIdAsync(request.id_categoria);
            if (categoria == null) throw new System.Exception("Categoría no encontrada.");

            entity.id_unidad = request.id_unidad;
            entity.id_categoria = request.id_categoria;
            entity.cod_materia = request.cod_materia;
            entity.descripcion = request.descripcion;
            entity.estado = request.estado;
            
            entity.UnidadMedida = unidad;
            entity.CategoriaMaterial = categoria;

            // Notice we do NOT update 'stock' here, fulfilling the user's business rule.

            await _repository.UpdateAsync(entity);
            return MapToResponse(entity);
        }

        public async Task<IEnumerable<MovimientoInventarioResponse>> GetKardexAsync(int idMaterial)
        {
            return await _movRepository.GetByMaterialIdAsync(idMaterial);
        }

        public async Task<MaterialResponse> RegistrarEntradaStockAsync(int idMaterial, StockInflowRequest request)
        {
            var entity = await _repository.GetByIdAsync(idMaterial);
            if (entity == null) throw new System.Exception("Material no encontrado.");

            if (request.cantidad <= 0)
            {
                throw new System.ArgumentException("La cantidad a ingresar debe ser mayor que cero.");
            }

            // Aumentar stock
            entity.stock += request.cantidad;
            await _repository.UpdateAsync(entity);

            // Registrar movimiento de ENTRADA en Kardex
            var movimiento = new MovimientoInventario
            {
                id_material = idMaterial,
                fecha = System.DateTime.Now,
                tipo_movimiento = "ENTRADA",
                cantidad = request.cantidad,
                saldo_stock = entity.stock,
                origen_tipo = "ENTRADA_MANUAL",
                origen_referencia = "ENTRADA-MANUAL-" + System.DateTime.Now.ToString("yyyyMMdd-HHmmss"),
                responsable = request.responsable,
                observaciones = request.observaciones ?? "Entrada manual de stock."
            };

            await _movRepository.AddAsync(movimiento);

            return MapToResponse(entity);
        }

        private MaterialResponse MapToResponse(Material m)
        {
            return new MaterialResponse
            {
                id_material = m.id_material,
                id_unidad = m.id_unidad,
                nombre_unidad = m.UnidadMedida?.nombre_unidad,
                id_categoria = m.id_categoria,
                nombre_categoria = m.CategoriaMaterial?.nombre_categoria,
                cod_materia = m.cod_materia,
                descripcion = m.descripcion,
                stock = m.stock,
                estado = m.estado
            };
        }
    }
}
