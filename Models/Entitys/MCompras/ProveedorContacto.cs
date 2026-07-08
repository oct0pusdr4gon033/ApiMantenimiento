namespace ApiMantenimiento.Models.Entitys.MCompras
{
    public class ProveedorContacto
    {
        public int id_contacto { get; set; } // PK
        public string ruc_proveedor { get; set; } // FK to Proveedor
        public string nombre { get; set; }
        public string apellido1 { get; set; }
        public string apellido2 { get; set; }
        public string correo { get; set; }
        public string telefono { get; set; }
        public string estado { get; set; } // "ACTIVO" or "INACTIVO"

        public Proveedor Proveedor { get; set; }
    }
}
