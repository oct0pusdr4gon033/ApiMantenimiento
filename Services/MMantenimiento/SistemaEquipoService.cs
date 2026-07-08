using ApiMantenimiento.Models.DTOS.MMantenimiento;
using ApiMantenimiento.Models.Entitys.MMantenimiento;
using ApiMantenimiento.Repositories.Interfaces.MMantenimiento;
using ApiMantenimiento.Services.Interfaces.MMantenimiento;
using ApiMantenimiento.Data.Context;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiMantenimiento.Services.MMantenimiento
{
    public class SistemaEquipoService : ISistemaEquipoService
    {
        private readonly ISistemaEquipoRepository _repository;
        private readonly IMapper _mapper;
        private readonly MantenimientoDbContext _context;

        public SistemaEquipoService(ISistemaEquipoRepository repository, IMapper mapper, MantenimientoDbContext context)
        {
            _repository = repository;
            _mapper = mapper;
            _context = context;
        }

        public async Task<IEnumerable<SistemaEquipoResponse>> GetAllAsync()
        {
            await SeedSubSistemasAsync();
            var sistemas = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<SistemaEquipoResponse>>(sistemas);
        }

        public async Task<SistemaEquipoResponse> CreateAsync(SistemaEquipoRequest request)
        {
            var sistema = _mapper.Map<SistemaEquipo>(request);
            var creado = await _repository.AddAsync(sistema);
            return _mapper.Map<SistemaEquipoResponse>(creado);
        }

        public async Task<SistemaEquipoResponse> UpdateAsync(int id, SistemaEquipoUpdateRequest request)
        {
            var sistema = await _repository.GetByIdAsync(id);
            if (sistema == null)
                throw new System.Exception("Sistema no encontrado");

            // Solo actualizamos el nombre
            sistema.nombre_sist = request.nombre_sist;
            
            await _repository.UpdateAsync(sistema);
            return _mapper.Map<SistemaEquipoResponse>(sistema);
        }

        public async Task<IEnumerable<SubSistemaResponse>> GetSubSistemasBySistemaAsync(int idSistema)
        {
            var subsistemas = await _repository.GetSubSistemasBySistemaAsync(idSistema);
            return _mapper.Map<IEnumerable<SubSistemaResponse>>(subsistemas);
        }

        public async Task<SubSistemaResponse> CreateSubSistemaAsync(SubSistemaRequest request)
        {
            var subsistema = _mapper.Map<SubSistemaEquipo>(request);
            var creado = await _repository.AddSubSistemaAsync(subsistema);
            return _mapper.Map<SubSistemaResponse>(creado);
        }

        private async Task SeedSubSistemasAsync()
        {
            try
            {
                var sistemas = await _context.SistemasEquipos.ToListAsync();
                if (!sistemas.Any())
                {
                    // Si no hay sistemas, sembrar los 5 básicos
                    sistemas = new List<SistemaEquipo>
                    {
                        new SistemaEquipo { cod_sist = "MOTOR", nombre_sist = "Sistema del Motor" },
                        new SistemaEquipo { cod_sist = "HIDRAULICO", nombre_sist = "Sistema Hidráulico" },
                        new SistemaEquipo { cod_sist = "ELECTRICO", nombre_sist = "Sistema Eléctrico" },
                        new SistemaEquipo { cod_sist = "FRENOS", nombre_sist = "Sistema de Frenos" },
                        new SistemaEquipo { cod_sist = "TRANSMISION", nombre_sist = "Sistema de Transmisión" }
                    };
                    await _context.SistemasEquipos.AddRangeAsync(sistemas);
                    await _context.SaveChangesAsync();
                }

                var idMotor = sistemas.FirstOrDefault(s => s.cod_sist.Trim().ToUpper() == "MOTOR")?.id_sistema ?? 0;
                var idHidraulico = sistemas.FirstOrDefault(s => s.cod_sist.Trim().ToUpper() == "HIDRAULICO" || s.cod_sist.Trim().ToUpper() == "HIDR")?.id_sistema ?? 0;
                var idElectrico = sistemas.FirstOrDefault(s => s.cod_sist.Trim().ToUpper() == "ELECTRICO" || s.cod_sist.Trim().ToUpper() == "ELEC")?.id_sistema ?? 0;
                var idFrenos = sistemas.FirstOrDefault(s => s.cod_sist.Trim().ToUpper() == "FRENOS" || s.cod_sist.Trim().ToUpper() == "FREN")?.id_sistema ?? 0;
                var idTransmision = sistemas.FirstOrDefault(s => s.cod_sist.Trim().ToUpper() == "TRANSMISION" || s.cod_sist.Trim().ToUpper() == "TRANS")?.id_sistema ?? 0;

                var subsistemas = new List<SubSistemaEquipo>();

                void AddSub(string cod, string nomb, int idSist)
                {
                    if (idSist > 0 && !_context.SubSistemasEquipos.Any(x => x.cod_subsist == cod && x.id_sistema == idSist))
                    {
                        subsistemas.Add(new SubSistemaEquipo { cod_subsist = cod, nombre_subsist = nomb, id_sistema = idSist });
                    }
                }

                // Motor (4)
                AddSub("FILT_MOT", "Filtros de Motor (Aire/Aceite/Combustible)", idMotor);
                AddSub("INYECC", "Sistema de Inyección / Inyectores", idMotor);
                AddSub("TURBO", "Turbocompresor", idMotor);
                AddSub("REFRIG_M", "Radiador y Sistema de Refrigeración", idMotor);

                // Hidráulico (4)
                AddSub("MANG_HID", "Mangueras y Conexiones Hidráulicas", idHidraulico);
                AddSub("BOM_HID", "Bombas Hidráulicas", idHidraulico);
                AddSub("VALV_HID", "Válvulas y Distribuidores", idHidraulico);
                AddSub("CIL_HID", "Cilindros Hidráulicos", idHidraulico);

                // Eléctrico (4)
                AddSub("BATERIA", "Baterías y Acumuladores", idElectrico);
                AddSub("ARRANQ", "Motor de Arranque", idElectrico);
                AddSub("ALTERN", "Alternador", idElectrico);
                AddSub("CABLEADO", "Arneses y Cableado Eléctrico", idElectrico);

                // Frenos (4)
                AddSub("PASTIL", "Pastillas y Zapatas de Freno", idFrenos);
                AddSub("DISCOS", "Discos y Tambores", idFrenos);
                AddSub("CIL_FR", "Cilindros de Freno / Calipers", idFrenos);
                AddSub("SERV_FR", "Servo Freno y Líneas de Presión", idFrenos);

                // Transmisión (4)
                AddSub("EMBRAG", "Kit de Embrague / Convertidor", idTransmision);
                AddSub("CARDAN", "Eje Cardán y Juntas Homocinéticas", idTransmision);
                AddSub("CORONAS", "Diferencial y Coronas", idTransmision);
                AddSub("SIST_MAR", "Selectores de Marcha / Palancas", idTransmision);

                if (subsistemas.Any())
                {
                    await _context.SubSistemasEquipos.AddRangeAsync(subsistemas);
                    await _context.SaveChangesAsync();
                }
            }
            catch (System.Exception)
            {
                // Silencioso
            }
        }
    }
}
