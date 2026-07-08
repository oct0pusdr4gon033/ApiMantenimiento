using System.Collections.Generic;

namespace ApiMantenimiento.Models.Entitys.MMantenimiento
{
    public class SubSistemaEquipo
    {
        public int id_subsistema { get; set; }
        public string cod_subsist { get; set; }
        public string nombre_subsist { get; set; }
        public int id_sistema { get; set; }

        // Navegación
        public SistemaEquipo Sistema { get; set; }
    }
}
