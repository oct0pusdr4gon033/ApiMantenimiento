using System;
using System.Collections.Generic;

namespace ApiMantenimiento.Models.DTOS.MAlmacen
{
    public class ValeCreateRequest
    {
        public int? id_ot { get; set; }
        public string solicitado_por { get; set; }
        public string? observaciones { get; set; }
        public List<ValeMaterialRequest> materiales { get; set; } = new List<ValeMaterialRequest>();
    }

    public class ValeUpdateRequest
    {
        public string solicitado_por { get; set; }
        public string? observaciones { get; set; }
        public List<ValeMaterialRequest> materiales { get; set; } = new List<ValeMaterialRequest>();
    }

    public class ValeMaterialRequest
    {
        public int id_material { get; set; }
        public decimal cantidad_solicitada { get; set; }
    }

    public class ValeResponse
    {
        public int id_vale { get; set; }
        public string cod_vale { get; set; }
        public int? id_ot { get; set; }
        public string? cod_ot { get; set; }
        public string? cod_equipo { get; set; }
        public string estado { get; set; }
        public DateTime fecha_creacion { get; set; }
        public DateTime? fecha_despacho { get; set; }
        public string solicitado_por { get; set; }
        public string? despachado_por { get; set; }
        public string? observaciones { get; set; }
        public List<ValeMaterialResponse> materiales { get; set; } = new List<ValeMaterialResponse>();
    }

    public class ValeMaterialResponse
    {
        public int id_vale_material { get; set; }
        public int id_material { get; set; }
        public string cod_materia { get; set; }
        public string descripcion { get; set; }
        public decimal cantidad_solicitada { get; set; }
        public decimal? cantidad_despachada { get; set; }
    }

    public class ValeDispatchRequest
    {
        public string despachado_por { get; set; }
        public List<ValeDispatchItemRequest> materiales { get; set; } = new List<ValeDispatchItemRequest>();
    }

    public class ValeDispatchItemRequest
    {
        public int id_vale_material { get; set; }
        public decimal cantidad_despachada { get; set; }
    }

    public class ReservedMaterialResponse
    {
        public int id_material { get; set; }
        public string cod_materia { get; set; }
        public string descripcion { get; set; }
        public decimal cantidad_reservada { get; set; }
    }
}
