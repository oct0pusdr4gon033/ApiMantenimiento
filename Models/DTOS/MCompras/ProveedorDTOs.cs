using System.Collections.Generic;

namespace ApiMantenimiento.Models.DTOS.MCompras
{
    public class ProveedorRequest
    {
        public string ruc { get; set; }
        public string razon_social { get; set; }
        public string nombre_comercial { get; set; }
        public string direccion { get; set; }
        public string correo { get; set; }
        public string telefono { get; set; }
        public string estado { get; set; } // "ACTIVO" / "INACTIVO"
        public List<string> categorias { get; set; } = new List<string>(); // List of cod_cat codes
    }

    public class ProveedorResponse
    {
        public string ruc { get; set; }
        public string razon_social { get; set; }
        public string nombre_comercial { get; set; }
        public string direccion { get; set; }
        public string correo { get; set; }
        public string telefono { get; set; }
        public string estado { get; set; }
        public List<string> categorias { get; set; } = new List<string>();
        public List<ProveedorContactoResponse> contactos { get; set; } = new List<ProveedorContactoResponse>();
    }

    public class ProveedorContactoRequest
    {
        public string nombre { get; set; }
        public string apellido1 { get; set; }
        public string apellido2 { get; set; }
        public string correo { get; set; }
        public string telefono { get; set; }
        public string estado { get; set; }
    }

    public class ProveedorContactoResponse
    {
        public int id_contacto { get; set; }
        public string ruc_proveedor { get; set; }
        public string nombre { get; set; }
        public string apellido1 { get; set; }
        public string apellido2 { get; set; }
        public string correo { get; set; }
        public string telefono { get; set; }
        public string estado { get; set; }
    }

    public class CategoriaProveedorRequest
    {
        public string cod_cat { get; set; }
        public string nombre_cat { get; set; }
    }

    public class CategoriaProveedorResponse
    {
        public string cod_cat { get; set; }
        public string nombre_cat { get; set; }
    }
}
