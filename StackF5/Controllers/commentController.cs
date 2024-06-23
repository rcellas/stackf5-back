using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StackF5.Repository.Comment;

namespace StackF5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class commentController : ControllerBase
    {
        private readonly ICommentRepository _repository;
        private readonly IMapper _mapper;
        public commentController(ICommentRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        
    }
}
