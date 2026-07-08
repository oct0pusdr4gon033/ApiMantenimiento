using System;
using System.Collections.Generic;

namespace ApiMantenimiento.Models.DTOS.MMantenimiento
{
    // ══════════════════════════════════════════════════════════════
    //  REQUESTS
    // ══════════════════════════════════════════════════════════════

    public class OrdenTrabajoCreateRequest
    {
        /// <summary>PREVENTIVA | CORRECTIVA</summary>
        public string tipo_ot { get; set; }
        public int id_equipo { get; set; }
        public int id_plan_mant { get; set; }
        public string? observaciones { get; set; }
        public string? creado_por { get; set; }

        /// <summary>
        /// Para CORRECTIVA: descripción libre de la falla.
        /// Para PREVENTIVA manual: lista de PMs a cubrir.
        /// </summary>
        public List<int> ids_detalle_estrg { get; set; } = new();

        // Nuevos campos para asignación manual en la creación
        public List<string> personal_dni { get; set; } = new();
        public List<OTMaterialManualRequest> materiales { get; set; } = new();

        // Campos específicos para OT Correctiva
        public DateTime? hora_intervencion { get; set; }
        public string? descripcion_falla { get; set; }
        public int? id_sistema { get; set; }
        public int? id_subsistema { get; set; }
        public decimal? horometro_falla { get; set; }
    }

    public class OTMaterialManualRequest
    {
        public int id_material_ref { get; set; }
        public string cod_materia { get; set; }
        public string descripcion_material { get; set; }
        public decimal cantidad_requerida { get; set; }
    }

    public class CambiarEstadoOTRequest
    {
        /// <summary>Nuevo estado: ACTIVA | EN_REVISION | CERRADA | INACTIVA</summary>
        public string nuevo_estado { get; set; }

        /// <summary>Horómetro actual del equipo (requerido al CERRAR).</summary>
        public decimal? horometro_cierre { get; set; }

        public string? observaciones { get; set; }

        /// <summary>Cantidades reales de materiales utilizados (al CERRAR).</summary>
        public List<OTMaterialCierreRequest> materiales_utilizados { get; set; } = new();

        /// <summary>Actividades marcadas como completadas (al CERRAR).</summary>
        public List<int> ids_actividades_completadas { get; set; } = new();

        /// <summary>Hora de inicio de intervención (opcional hasta el cierre, requerido al cerrar OT Correctiva).</summary>
        public DateTime? hora_intervencion { get; set; }
    }

    public class OTMaterialCierreRequest
    {
        public int id_ot_material { get; set; }
        public decimal cantidad_utilizada { get; set; }
    }

    public class AgregarActividadOTRequest
    {
        public string nombre_actividad { get; set; }
        public string cod_sistema { get; set; }
        public string tipo_pm { get; set; }
    }

    public class AgregarMaterialOTRequest
    {
        public int id_material_ref { get; set; }
        public decimal cantidad_requerida { get; set; }
    }

    public class AgregarPersonalOTRequest
    {
        public string dni_empleado { get; set; }
    }

    public class ConfiguracionFlotaRequest
    {
        public int? id_flota { get; set; }
        public decimal horas_diarias_estimadas { get; set; }
        public string actualizado_por { get; set; }
    }

    // ══════════════════════════════════════════════════════════════
    //  RESPONSES
    // ══════════════════════════════════════════════════════════════

    public class OrdenTrabajoResponse
    {
        public int id_ot { get; set; }
        public string cod_ot { get; set; }
        public string tipo_ot { get; set; }
        public string forma_generacion { get; set; }
        public string estado { get; set; }
        public decimal horometro_al_momento { get; set; }
        public decimal? horometro_corte { get; set; }
        public DateTime fecha_creacion { get; set; }
        public DateTime? fecha_atencion { get; set; }
        public string observaciones { get; set; }
        public string creado_por { get; set; }

        // Campos específicos para OT Correctiva
        public DateTime? hora_intervencion { get; set; }
        public string? descripcion_falla { get; set; }
        public int? id_sistema { get; set; }
        public string? nombre_sistema { get; set; }
        public int? id_subsistema { get; set; }
        public string? nombre_subsistema { get; set; }
        public decimal? horometro_falla { get; set; }

        // Equipo
        public int id_equipo { get; set; }
        public string cod_equipo { get; set; }
        public string placa_equipo { get; set; }
        public string num_serie { get; set; }
        public string nombre_flota { get; set; }
        public string marca { get; set; }
        public string modelo { get; set; }

        // Plan
        public int id_plan_mant { get; set; }
        public string titulo_estrategia { get; set; }

        // PMs incluidos
        public List<OTPlanDetalleResponse> pms_incluidos { get; set; } = new();

        // Hoja de ruta
        public List<OTActividadResponse> actividades { get; set; } = new();
        public List<OTMaterialResponse> materiales { get; set; } = new();
        public List<OTPersonalResponse> personal { get; set; } = new();
    }

    public class OTPlanDetalleResponse
    {
        public int id_detalle_estrg { get; set; }
        public string tipo_pm { get; set; }
        public decimal umbral_mant { get; set; }
        public string uni_med { get; set; }
    }

    public class OTActividadResponse
    {
        public int id_ot_actividad { get; set; }
        public string nombre_actividad { get; set; }
        public string cod_sistema { get; set; }
        public string tipo_pm { get; set; }
        public string estado_ejecucion { get; set; }
        public string observacion_tecnica { get; set; }
    }

    public class OTMaterialResponse
    {
        public int id_ot_material { get; set; }
        public string cod_materia { get; set; }
        public string descripcion_material { get; set; }
        public decimal cantidad_requerida { get; set; }
        public decimal? cantidad_utilizada { get; set; }
    }

    public class OTPersonalResponse
    {
        public int id_ot_personal { get; set; }
        public string dni_empleado { get; set; }
        public string nombre_empleado { get; set; }
        public string rol { get; set; }
    }

    public class EmpleadoDisponibleResponse
    {
        public string dni_empleado { get; set; }
        public string codigo_empleado { get; set; }
        public string nombre { get; set; }
        public string apellido1 { get; set; }
        public string apellido2 { get; set; }
        public int id_rol { get; set; }
        public string nombreRol { get; set; }
        public bool disponible { get; set; }
        public string motivo_no_disponible { get; set; }
        public int ots_hoy { get; set; }
    }

    public class CalendarioProyeccionResponse
    {
        public int id_equipo { get; set; }
        public string cod_equipo { get; set; }
        public string placa_equipo { get; set; }
        public decimal horometro_actual { get; set; }
        public List<CalendarioPMResponse> proyecciones { get; set; } = new();
    }

    public class CalendarioPMResponse
    {
        public int id_detalle_estrg { get; set; }
        public string tipo_pm { get; set; }
        public decimal umbral_mant { get; set; }
        public string uni_med { get; set; }
        public decimal horometro_corte_base { get; set; }
        public decimal proximo_umbral { get; set; }
        public decimal horas_faltantes { get; set; }
        public double dias_estimados { get; set; }
        public DateTime fecha_estimada { get; set; }
        public bool ejecutado { get; set; }
        public DateTime? fecha_real_ejecucion { get; set; }
        public int? id_ot { get; set; }
        public string estado_ot { get; set; }
    }

    public class ConfiguracionFlotaResponse
    {
        public int id_configuracion { get; set; }
        public int? id_flota { get; set; }
        public string nombre_flota { get; set; }
        public decimal horas_diarias_estimadas { get; set; }
        public DateTime fecha_actualizacion { get; set; }
    }
}
