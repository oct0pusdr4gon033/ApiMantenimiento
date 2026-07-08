using ApiMantenimiento.Models.DTOS.MCompras;
using ApiMantenimiento.Models.Entitys.MCompras;
using ApiMantenimiento.Repositories.Interfaces.MCompras;
using ApiMantenimiento.Services.Interfaces.MCompras;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiMantenimiento.Services.MCompras
{
    public class ProveedorService : IProveedorService
    {
        private readonly IProveedorRepository _repository;

        public ProveedorService(IProveedorRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ProveedorResponse>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            return entities.Select(MapToResponse);
        }

        public async Task<ProveedorResponse> GetByRucAsync(string ruc)
        {
            var entity = await _repository.GetByRucAsync(ruc);
            if (entity == null) throw new System.Exception("Proveedor no encontrado.");
            return MapToResponse(entity);
        }

        public async Task<IEnumerable<ProveedorResponse>> BuscarProveedoresAsync(string? ruc, string? razonSocial, string? codCat)
        {
            var entities = await _repository.BuscarProveedoresAsync(ruc, razonSocial, codCat);
            return entities.Select(MapToResponse);
        }

        public async Task<ProveedorResponse> CreateAsync(ProveedorRequest request)
        {
            if (await _repository.ExistsByRucAsync(request.ruc))
                throw new System.Exception("El RUC del proveedor ya existe.");

            var entity = new Proveedor
            {
                ruc = request.ruc,
                razon_social = request.razon_social,
                nombre_comercial = request.nombre_comercial,
                direccion = request.direccion,
                correo = request.correo,
                telefono = request.telefono,
                estado = request.estado ?? "ACTIVO"
            };

            if (request.categorias != null && request.categorias.Any())
            {
                foreach (var codCat in request.categorias)
                {
                    var cat = await _repository.GetCategoriaByCodAsync(codCat);
                    if (cat != null)
                    {
                        entity.ProveedorCategorias.Add(new ProveedorCategoria
                        {
                            ruc = entity.ruc,
                            cod_cat = codCat
                        });
                    }
                }
            }

            await _repository.AddAsync(entity);
            return MapToResponse(entity);
        }

        public async Task<ProveedorResponse> UpdateAsync(string ruc, ProveedorRequest request)
        {
            var entity = await _repository.GetByRucAsync(ruc);
            if (entity == null) throw new System.Exception("Proveedor no encontrado.");

            entity.razon_social = request.razon_social;
            entity.nombre_comercial = request.nombre_comercial;
            entity.direccion = request.direccion;
            entity.correo = request.correo;
            entity.telefono = request.telefono;
            entity.estado = request.estado;

            // Update Categories:
            entity.ProveedorCategorias.Clear();
            if (request.categorias != null && request.categorias.Any())
            {
                foreach (var codCat in request.categorias)
                {
                    var cat = await _repository.GetCategoriaByCodAsync(codCat);
                    if (cat != null)
                    {
                        entity.ProveedorCategorias.Add(new ProveedorCategoria
                        {
                            ruc = ruc,
                            cod_cat = codCat
                        });
                    }
                }
            }

            await _repository.UpdateAsync(entity);
            return MapToResponse(entity);
        }

        public async Task<ProveedorContactoResponse> AddContactoAsync(string ruc, ProveedorContactoRequest request)
        {
            var provider = await _repository.GetByRucAsync(ruc);
            if (provider == null) throw new System.Exception("Proveedor no encontrado.");

            var contact = new ProveedorContacto
            {
                ruc_proveedor = ruc,
                nombre = request.nombre,
                apellido1 = request.apellido1,
                apellido2 = request.apellido2,
                correo = request.correo,
                telefono = request.telefono,
                estado = request.estado ?? "ACTIVO"
            };

            provider.Contactos.Add(contact);
            await _repository.UpdateAsync(provider);

            // Get the saved contact (which will have id_contacto populated)
            var savedContact = provider.Contactos.FirstOrDefault(c => c.nombre == contact.nombre && c.correo == contact.correo);
            return MapToContactoResponse(savedContact ?? contact);
        }

        public async Task<ProveedorContactoResponse> UpdateContactoAsync(int idContacto, ProveedorContactoRequest request)
        {
            var allProviders = await _repository.GetAllAsync();
            ProveedorContacto contact = null;
            Proveedor parentProvider = null;

            foreach (var prov in allProviders)
            {
                var match = prov.Contactos.FirstOrDefault(c => c.id_contacto == idContacto);
                if (match != null)
                {
                    contact = match;
                    parentProvider = prov;
                    break;
                }
            }

            if (contact == null) throw new System.Exception("Contacto no encontrado.");

            contact.nombre = request.nombre;
            contact.apellido1 = request.apellido1;
            contact.apellido2 = request.apellido2;
            contact.correo = request.correo;
            contact.telefono = request.telefono;
            contact.estado = request.estado;

            await _repository.UpdateAsync(parentProvider);
            return MapToContactoResponse(contact);
        }

        public async Task<IEnumerable<CategoriaProveedorResponse>> GetCategoriasAsync()
        {
            var entities = await _repository.GetCategoriasAsync();
            return entities.Select(c => new CategoriaProveedorResponse
            {
                cod_cat = c.cod_cat,
                nombre_cat = c.nombre_cat
            });
        }

        public async Task<CategoriaProveedorResponse> CreateCategoriaAsync(CategoriaProveedorRequest request)
        {
            var existing = await _repository.GetCategoriaByCodAsync(request.cod_cat);
            if (existing != null) throw new System.Exception("La categoría ya existe.");

            var entity = new CategoriaProveedor
            {
                cod_cat = request.cod_cat.ToUpper(),
                nombre_cat = request.nombre_cat
            };

            await _repository.AddCategoriaAsync(entity);
            return new CategoriaProveedorResponse
            {
                cod_cat = entity.cod_cat,
                nombre_cat = entity.nombre_cat
            };
        }

        private ProveedorResponse MapToResponse(Proveedor p)
        {
            return new ProveedorResponse
            {
                ruc = p.ruc,
                razon_social = p.razon_social,
                nombre_comercial = p.nombre_comercial,
                direccion = p.direccion,
                correo = p.correo,
                telefono = p.telefono,
                estado = p.estado,
                categorias = p.ProveedorCategorias.Select(pc => pc.cod_cat).ToList(),
                contactos = p.Contactos.Select(MapToContactoResponse).ToList()
            };
        }

        private ProveedorContactoResponse MapToContactoResponse(ProveedorContacto c)
        {
            return new ProveedorContactoResponse
            {
                id_contacto = c.id_contacto,
                ruc_proveedor = c.ruc_proveedor,
                nombre = c.nombre,
                apellido1 = c.apellido1,
                apellido2 = c.apellido2,
                correo = c.correo,
                telefono = c.telefono,
                estado = c.estado
            };
        }
    }
}
