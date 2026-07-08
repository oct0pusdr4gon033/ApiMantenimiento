namespace ApiMantenimiento.Models.Entitys.MCompras
{
    public class ProveedorCategoria
    {
        public string ruc { get; set; }
        public string cod_cat { get; set; }

        public Proveedor Proveedor { get; set; }
        public CategoriaProveedor CategoriaProveedor { get; set; }
    }
}
