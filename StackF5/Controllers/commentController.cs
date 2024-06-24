using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StackF5.DTOs.Comment;
using StackF5.Repository.Comment;
using StackF5.Repository.Incidence;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StackF5.Entity;

namespace StackF5.Controllers;
    [Route("api/incidences/{incidenceId}/comments")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentRepository _repository;
        private readonly IMapper _mapper;
        private readonly IIncidenceRepository _incidenceRepository;

        public CommentsController(ICommentRepository repository, IMapper mapper, IIncidenceRepository incidenceRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _incidenceRepository = incidenceRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<CommentDto>>> GetAllComments(int incidenceId)
        {
            var comments = await _repository.GetAllComments(incidenceId, _incidenceRepository);
            var commentsDto = _mapper.Map<List<CommentDto>>(comments);
            return Ok(commentsDto);
        }

        [HttpPost]
        public async Task<ActionResult<CommentDto>> CreateComment(int incidenceId, [FromBody] CreateCommentDto createCommentDto)
        {
            var comment = _mapper.Map<Comment>(createCommentDto);
            comment.IncidenceId = incidenceId;
            await _repository.CreateComment(comment);
            return Ok(comment);
        }
    }
