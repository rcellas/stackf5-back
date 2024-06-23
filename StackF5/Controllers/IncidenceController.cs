using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StackF5.Data;
using StackF5.DTOs.Incidence;
using StackF5.Repository.Incidence;

namespace StackF5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IncidenceController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IIncindenceRepository _repository;
        private readonly IMapper _mapper;
        
        public IncidenceController(ApplicationDbContext context, IIncindenceRepository repository, IMapper mapper)
        {
            _repository= repository;
            _context = context;
            _mapper = mapper;
        }
        
        [HttpGet]
        public async Task<ActionResult<List<IncidenceDto>>> GetAllIncidence()
        {
            var incidences = await _repository.GetAllIncidence();
            var incidencesDto = _mapper.Map<List<IncidenceDto>>(incidences);
            return Ok(incidencesDto);
        }
        
        [HttpPost]
        public async Task<ActionResult<IncidenceDto>> CreateIncidence(CreateIncidenceDto createIncidenceDto)
        {
            var incidence = _mapper.Map<Entity.Incidence>(createIncidenceDto);
            await _repository.CreateIncidence(incidence);
            var incidenceDto = _mapper.Map<IncidenceDto>(incidence);
            return Ok(incidenceDto);
        }
        
        [HttpPut("{id}")]
        public async Task<ActionResult<IncidenceDto>> UpdateIncidence(int id, CreateIncidenceDto updateIncidenceDto)
        {
            var incidence = await _repository.GetIncidenceById(id);
            if(incidence == null) return NotFound();
            _mapper.Map(updateIncidenceDto, incidence);
            await _repository.UpdateIncidence(incidence);
            var incidenceDto = _mapper.Map<IncidenceDto>(incidence);
            return Ok(incidenceDto);
        }
        
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteIncidence(int id)
        {
            await _repository.DeleteIncidence(id);
            return NoContent();
        }
    }
}
