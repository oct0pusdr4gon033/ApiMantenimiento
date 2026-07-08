namespace ApiMantenimiento.Models.Entitys.MAlmacen
{
    public class ValeMaterial
    {
        public int id_vale_material { get; set; }
        public int id_vale { get; set; }
        public int id_material { get; set; }

        public decimal cantidad_solicitada { get; set; }
        public decimal? cantidad_despachada { get; set; }

        // Navegación
        public Vale Vale { get; set; }
        public Material Material { get; set; }
    }
}
