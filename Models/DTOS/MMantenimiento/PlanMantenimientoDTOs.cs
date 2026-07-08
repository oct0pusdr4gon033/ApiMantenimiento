using System;
using System.Collections.Generic;

namespace ApiMantenimiento.Models.DTOS.MMantenimiento
{
    // ── Requests ─────────────────────────────────────────────────

    public class PlanMantenimientoRequest
    {
        public int id_estrategia { get; set; }
        public DateTime? fecha_creacion { get; set; }
        public bool estado { get; set; }

        public List<PlanMantenimientoActividadRequest> Actividades { get; set; } = new List<PlanMantenimientoActividadRequest>();
        public List<PlanMantenimientoPersonalRequest> Personales { get; set; } = new List<PlanMantenimientoPersonalRequest>();
    }

    public class PlanMantenimientoUpdateRequest
    {
        public int id_estrategia { get; set; }
        public bool estado { get; set; }

        public List<PlanMantenimientoActividadRequest> Actividades { get; set; } = new List<PlanMantenimientoActividadRequest>();
        public List<PlanMantenimientoPersonalRequest> Personales { get; set; } = new List<PlanMantenimientoPersonalRequest>();
    }

    /// <summary>
    /// Request de una actividad dentro del plan.
    /// Si id_actividad == 0, se busca/crea usando nombre_actividad + cod_sistema.
    /// El material se envía opcionalmente con id_material y cantidad.
    /// </summary>
    public class PlanMantenimientoActividadRequest
    {
        public int id_actividad { get; set; }
        public string? nombre_actividad { get; set; }   // para crear actividad nueva desde Excel
        public string? cod_sistema { get; set; }         // para resolver actividad nueva desde Excel
        public int id_detalle_estrg { get; set; }

        // Material (opcional — máx. 1 por actividad)
        public int? id_material { get; set; }
        public decimal? cantidad { get; set; }
    }

    // ── Responses ────────────────────────────────────────────────

    public class PlanMantenimientoResponse
    {
        public int id_plan_mant { get; set; }
        public int id_estrategia { get; set; }
        public DateTime fecha_creacion { get; set; }
        public bool estado { get; set; }

        public EstrategiaResponse Estrategia { get; set; }

        public List<PlanMantenimientoActividadResponse> Actividades { get; set; } = new List<PlanMantenimientoActividadResponse>();
        public List<PlanMantenimientoPersonalResponse> Personales { get; set; } = new List<PlanMantenimientoPersonalResponse>();
    }

    /// <summary>
    /// Respuesta de una actividad. Incluye el material aplanado (máx. 1) 
    /// para mantener compatibilidad con el frontend actual.
    /// </summary>
    public class PlanMantenimientoActividadResponse
    {
        public int id_plan_mant { get; set; }
        public int id_actividad { get; set; }
        public string nombre_actividad { get; set; }
        public string descripcion_actividad { get; set; }
        public string cod_sistema { get; set; }

        public int id_detalle_estrg { get; set; }
        public string tipo_pm { get; set; }

        // Material aplanado (primer material de la lista, o null si no tiene)
        public int? id_material { get; set; }
        public string cod_materia { get; set; }
        public string descripcion_material { get; set; }
        public decimal? cantidad { get; set; }
    }

    public class PlanMantenimientoPersonalRequest
    {
        public int id_rol { get; set; }
        public int cantidad { get; set; }
    }

    public class PlanMantenimientoPersonalResponse
    {
        public int id_plan_personal { get; set; }
        public int id_rol { get; set; }
        public string nombre_rol { get; set; }
        public int cantidad { get; set; }
    }
}
