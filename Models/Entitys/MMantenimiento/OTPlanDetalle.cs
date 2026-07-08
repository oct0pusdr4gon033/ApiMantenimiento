namespace ApiMantenimiento.Models.Entitys.MMantenimiento
{
    /// <summary>
    /// Tabla de unión: relaciona una OT con los niveles PM que cubre.
    /// Una OT puede combinar PM1+PM2 si coinciden en la misma ventana.
    /// PK compuesta: (id_ot, id_detalle_estrg)
    /// </summary>
    public class OTPlanDetalle
    {
        public int id_ot { get; set; }
        public int id_detalle_estrg { get; set; }

        // Navegación
        public OrdenTrabajo OrdenTrabajo { get; set; }
        public EstrategiaDetalle EstrategiaDetalle { get; set; }
    }
}
