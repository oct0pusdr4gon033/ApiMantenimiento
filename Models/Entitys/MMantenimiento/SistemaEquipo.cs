using System.Collections.Generic;

namespace ApiMantenimiento.Models.Entitys.MMantenimiento
{
    public class SistemaEquipo
    {
        public int id_sistema { get; set; }
        public string cod_sist { get; set; }
        public string nombre_sist { get; set; }

        public ICollection<ActividadSistema> Actividades { get; set; } = new List<ActividadSistema>();
        public ICollection<SubSistemaEquipo> SubSistemas { get; set; } = new List<SubSistemaEquipo>();
    }
}
