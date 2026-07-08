using System.Collections.Generic;

namespace ApiMantenimiento.Models.Entitys.MCompras
{
    public class CategoriaProveedor
    {
        public string cod_cat { get; set; } // PK
        public string nombre_cat { get; set; }

        public ICollection<ProveedorCategoria> ProveedorCategorias { get; set; } = new List<ProveedorCategoria>();
    }
}
