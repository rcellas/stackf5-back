using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StackF5.DTOs.Tag;
using StackF5.Repository.Tag;

namespace StackF5.Controllers;

    [ApiController]
    [Route("[controller]")]
    public class tagController : ControllerBase
    {
        private readonly ITagRepository _tagRepository;
        private readonly IMapper _mapper;

        public tagController(ITagRepository tagRepository, IMapper mapper)
        {
            _tagRepository = tagRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<TagDto>>> GetAllTags()
        {
            var tags = await _tagRepository.GetAllTags();
            var tagDtos = _mapper.Map<List<TagDto>>(tags);
            return Ok(tagDtos);
        }
        
        [HttpPost]
        public async Task<ActionResult> CreateTag(TagDto tagDto)
        {
            var tag = _mapper.Map<Entity.Tag>(tagDto);
            var tagId = await _tagRepository.CreateTag(tag);
            return Ok(tagId);
        }

    }