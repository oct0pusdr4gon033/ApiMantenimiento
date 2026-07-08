namespace ApiMantenimiento.Models.Entitys.MMantenimiento
{
    /// <summary>
    /// Snapshot de una actividad dentro de una OT.
    /// Se copia al crear la OT para preservar el estado del plan en ese momento.
    /// Los campos nombre_actividad, cod_sistema y tipo_pm son snapshots intencionales
    /// y NO constituyen dependencias transitivas (3FN cumplida).
    /// </summary>
    public class OTActividad
    {
        public int id_ot_actividad { get; set; }
        public int id_ot { get; set; }

        /// <summary>Referencia suave a la actividad original (puede ser null si fue creada ad-hoc).</summary>
        public int? id_actividad_ref { get; set; }

        // Snapshot — estado del plan al momento de crear la OT
        public string nombre_actividad { get; set; }
        public string cod_sistema { get; set; }
        public string tipo_pm { get; set; }

        // Estado de ejecución: PENDIENTE | COMPLETADA
        public string estado_ejecucion { get; set; } = "PENDIENTE";

        public string observacion_tecnica { get; set; }

        // Navegación
        public OrdenTrabajo OrdenTrabajo { get; set; }
    }
}
