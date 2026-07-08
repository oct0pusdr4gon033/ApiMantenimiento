namespace ApiMantenimiento.Models.Entitys.MMantenimiento
{
    /// <summary>
    /// Snapshot de un material dentro de una OT.
    /// cod_materia y descripcion_material son snapshots intencionales (3FN cumplida).
    /// cantidad_utilizada se ingresa manualmente al cerrar la OT.
    /// </summary>
    public class OTMaterial
    {
        public int id_ot_material { get; set; }
        public int id_ot { get; set; }

        /// <summary>Referencia suave al material original.</summary>
        public int? id_material_ref { get; set; }

        // Snapshot — estado del catálogo al momento de crear la OT
        public string cod_materia { get; set; }
        public string descripcion_material { get; set; }

        public decimal cantidad_requerida { get; set; }

        /// <summary>Cantidad real usada — se registra al cerrar la OT.</summary>
        public decimal? cantidad_utilizada { get; set; }

        // Navegación
        public OrdenTrabajo OrdenTrabajo { get; set; }
    }
}
