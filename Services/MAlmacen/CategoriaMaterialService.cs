using ApiMantenimiento.Models.DTOS.MAlmacen;
using ApiMantenimiento.Models.Entitys.MAlmacen;
using ApiMantenimiento.Repositories.Interfaces.MAlmacen;
using ApiMantenimiento.Services.Interfaces.MAlmacen;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiMantenimiento.Services.MAlmacen
{
    public class CategoriaMaterialService : ICategoriaMaterialService
    {
        private readonly ICategoriaMaterialRepository _repository;

        public CategoriaMaterialService(ICategoriaMaterialRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<CategoriaMaterialResponse>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            return entities.Select(e => new CategoriaMaterialResponse
            {
                id_categoria = e.id_categoria,
                cod_cat = e.cod_cat,
                nombre_categoria = e.nombre_categoria
            });
        }

        public async Task<CategoriaMaterialResponse> CreateAsync(CategoriaMaterialRequest request)
        {
            if (await _repository.ExistsByCodCatAsync(request.cod_cat))
                throw new System.Exception("El código de categoría ya existe.");

            var entity = new CategoriaMaterial
            {
                cod_cat = request.cod_cat,
                nombre_categoria = request.nombre_categoria
            };

            await _repository.AddAsync(entity);

            return new CategoriaMaterialResponse
            {
                id_categoria = entity.id_categoria,
                cod_cat = entity.cod_cat,
                nombre_categoria = entity.nombre_categoria
            };
        }

        public async Task<CategoriaMaterialResponse> UpdateAsync(int id, CategoriaMaterialRequest request)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) throw new System.Exception("Categoría de Material no encontrada.");

            if (entity.cod_cat != request.cod_cat && await _repository.ExistsByCodCatAsync(request.cod_cat))
                throw new System.Exception("El código de categoría ya existe en otro registro.");

            entity.cod_cat = request.cod_cat;
            entity.nombre_categoria = request.nombre_categoria;

            await _repository.UpdateAsync(entity);

            return new CategoriaMaterialResponse
            {
                id_categoria = entity.id_categoria,
                cod_cat = entity.cod_cat,
                nombre_categoria = entity.nombre_categoria
            };
        }
    }
}
