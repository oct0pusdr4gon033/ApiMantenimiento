using System;
using System.Collections.Generic;

namespace ApiMantenimiento.Models.Entitys.MMantenimiento
{
    public class PlanMantenimiento
    {
        public int id_plan_mant { get; set; }
        public int id_estrategia { get; set; }
        public DateTime fecha_creacion { get; set; }
        public bool estado { get; set; }

        public Estrategia Estrategia { get; set; }

        public ICollection<PlanMantenimientoActividad> PlanMantenimientoActividades { get; set; }
        public ICollection<PlanMantenimientoPersonal> PlanMantenimientoPersonales { get; set; }
    }
}
