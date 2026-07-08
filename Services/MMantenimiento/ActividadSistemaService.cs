using ApiMantenimiento.Models.DTOS.MMantenimiento;
using ApiMantenimiento.Models.Entitys.MMantenimiento;
using ApiMantenimiento.Repositories.Interfaces.MMantenimiento;
using ApiMantenimiento.Services.Interfaces.MMantenimiento;
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiMantenimiento.Services.MMantenimiento
{
    public class ActividadSistemaService : IActividadSistemaService
    {
        private readonly IActividadSistemaRepository _repository;
        private readonly ISistemaEquipoRepository _sistemaRepository;
        private readonly IMapper _mapper;

        public ActividadSistemaService(
            IActividadSistemaRepository repository,
            ISistemaEquipoRepository sistemaRepository,
            IMapper mapper)
        {
            _repository = repository;
            _sistemaRepository = sistemaRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ActividadSistemaResponse>> GetAllAsync()
        {
            var actividades = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<ActividadSistemaResponse>>(actividades);
        }

        public async Task<IEnumerable<ActividadSistemaResponse>> BuscarPorSistemaONombreAsync(string termino)
        {
            var actividades = await _repository.BuscarPorSistemaONombreAsync(termino);
            return _mapper.Map<IEnumerable<ActividadSistemaResponse>>(actividades);
        }

        public async Task<ActividadSistemaResponse> CreateAsync(ActividadSistemaRequest request)
        {
            var sistema = await _sistemaRepository.GetByIdAsync(request.id_sistema);
            if (sistema == null)
                throw new System.Exception("Sistema de equipo no encontrado.");

            var actividad = _mapper.Map<ActividadSistema>(request);

            // Generación de código automático: ej. MOT-001
            var ultimoCodigo = await _repository.GetUltimoCodigoActividadAsync(request.id_sistema);
            int correlativo = 1;

            if (!string.IsNullOrEmpty(ultimoCodigo) && ultimoCodigo.Contains("-"))
            {
                var partes = ultimoCodigo.Split('-');
                if (partes.Length > 1 && int.TryParse(partes[1], out int lastNumber))
                {
                    correlativo = lastNumber + 1;
                }
            }

            actividad.cod_act = $"{sistema.cod_sist}-{correlativo:D3}";

            var creada = await _repository.AddAsync(actividad);
            return _mapper.Map<ActividadSistemaResponse>(creada);
        }

        public async Task<ActividadSistemaResponse> UpdateAsync(int id, ActividadSistemaUpdateRequest request)
        {
            var actividad = await _repository.GetByIdAsync(id);
            if (actividad == null)
                throw new System.Exception("Actividad no encontrada.");

            // Mapeamos las propiedades permitidas, EXCEPTUANDO cod_act
            _mapper.Map(request, actividad);

            await _repository.UpdateAsync(actividad);
            return _mapper.Map<ActividadSistemaResponse>(actividad);
        }
    }
}
