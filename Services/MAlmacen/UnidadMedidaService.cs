using ApiMantenimiento.Models.DTOS.MAlmacen;
using ApiMantenimiento.Models.Entitys.MAlmacen;
using ApiMantenimiento.Repositories.Interfaces.MAlmacen;
using ApiMantenimiento.Services.Interfaces.MAlmacen;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiMantenimiento.Services.MAlmacen
{
    public class UnidadMedidaService : IUnidadMedidaService
    {
        private readonly IUnidadMedidaRepository _repository;

        public UnidadMedidaService(IUnidadMedidaRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<UnidadMedidaResponse>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            return entities.Select(e => new UnidadMedidaResponse
            {
                id_unidad = e.id_unidad,
                nombre_unidad = e.nombre_unidad,
                abreviatura = e.abreviatura
            });
        }

        public async Task<UnidadMedidaResponse> CreateAsync(UnidadMedidaRequest request)
        {
            var entity = new UnidadMedida
            {
                nombre_unidad = request.nombre_unidad,
                abreviatura = request.abreviatura
            };

            await _repository.AddAsync(entity);

            return new UnidadMedidaResponse
            {
                id_unidad = entity.id_unidad,
                nombre_unidad = entity.nombre_unidad,
                abreviatura = entity.abreviatura
            };
        }

        public async Task<UnidadMedidaResponse> UpdateAsync(int id, UnidadMedidaRequest request)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) throw new System.Exception("Unidad de Medida no encontrada.");

            entity.nombre_unidad = request.nombre_unidad;
            entity.abreviatura = request.abreviatura;

            await _repository.UpdateAsync(entity);

            return new UnidadMedidaResponse
            {
                id_unidad = entity.id_unidad,
                nombre_unidad = entity.nombre_unidad,
                abreviatura = entity.abreviatura
            };
        }
    }
}
