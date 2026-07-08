namespace ApiMantenimiento.Models.Entitys.MAlmacen
{
    public class Material
    {
        public int id_material { get; set; }
        public int id_unidad { get; set; }
        public int id_categoria { get; set; }
        public string cod_materia { get; set; } // The user enters this, e.g. based on category
        public string descripcion { get; set; }
        public decimal stock { get; set; }
        public decimal stock_minimo { get; set; }
        public decimal precio_actual { get; set; }
        public string estado { get; set; }

        public UnidadMedida UnidadMedida { get; set; }
        public CategoriaMaterial CategoriaMaterial { get; set; }
        public System.Collections.Generic.ICollection<HistorialPrecio> HistorialPrecios { get; set; } = new System.Collections.Generic.List<HistorialPrecio>();
    }
}
