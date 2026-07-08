using System.Collections.Generic;

namespace ApiMantenimiento.Models.Entitys.MCompras
{
    public class Proveedor
    {
        public string ruc { get; set; } // Primary Key (e.g. 11 digits in Peru)
        public string razon_social { get; set; }
        public string nombre_comercial { get; set; }
        public string direccion { get; set; }
        public string correo { get; set; }
        public string telefono { get; set; }
        public string estado { get; set; } // "ACTIVO" or "INACTIVO"

        public ICollection<ProveedorContacto> Contactos { get; set; } = new List<ProveedorContacto>();
        public ICollection<ProveedorCategoria> ProveedorCategorias { get; set; } = new List<ProveedorCategoria>();
    }
}
