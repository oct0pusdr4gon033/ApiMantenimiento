using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ApiMantenimiento.Models.Entitys.MMantenimiento;
using ApiMantenimiento.Models.DTOS.MMantenimiento;
using ApiMantenimiento.Repositories.Interfaces.MMantenimiento;
using ApiMantenimiento.Services.Interfaces.MMantenimiento;

namespace ApiMantenimiento.Services.MMantenimiento
{
    public class EstrategiaService : IEstrategiaService
    {
        private readonly IEstrategiaRepository _repository;
        private readonly IMapper _mapper;

        public EstrategiaService(IEstrategiaRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EstrategiaResponse>> GetAllEstrategiasAsync()
        {
            var estrategias = await _repository.GetAllEstrategiasAsync();
            return _mapper.Map<IEnumerable<EstrategiaResponse>>(estrategias);
        }

        public async Task<EstrategiaResponse> GetEstrategiaByIdAsync(int id)
        {
            var estrategia = await _repository.GetEstrategiaByIdAsync(id);
            if (estrategia == null)
            {
                return null; // O lanzar excepción personalizada
            }
            return _mapper.Map<EstrategiaResponse>(estrategia);
        }

        public async Task<EstrategiaResponse> CreateEstrategiaAsync(EstrategiaRequest request)
        {
            var estrategiaEntity = _mapper.Map<Estrategia>(request);
            var createdEntity = await _repository.AddEstrategiaAsync(estrategiaEntity);
            return _mapper.Map<EstrategiaResponse>(createdEntity);
        }

        public async Task<EstrategiaResponse> UpdateEstrategiaAsync(int id, EstrategiaUpdateRequest request)
        {
            var existingEntity = await _repository.GetEstrategiaByIdAsync(id);
            if (existingEntity == null) return null;

            existingEntity.titulo_estrategia = request.titulo_estrategia;
            existingEntity.estado = request.estado;

            var updatedEntity = await _repository.UpdateEstrategiaAsync(existingEntity);
            return _mapper.Map<EstrategiaResponse>(updatedEntity);
        }
    }
}
