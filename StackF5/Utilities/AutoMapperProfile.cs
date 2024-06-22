using AutoMapper;
using StackF5.DTOs.Comment;
using StackF5.DTOs.Incidence;
using StackF5.DTOs.Tag;
using StackF5.Entity;

namespace StackF5.Utilities;

public class AutoMapperProfile :Profile
{
    public AutoMapperProfile()
    {
        CreateMap<CreateCommentDto, Comment>();
        CreateMap<Comment, CommentDto>();
        
        CreateMap<CreateIncidenceDto, Incidence>();
        CreateMap<Incidence, IncidenceDto>();

        CreateMap<CreateTagDto, Tag>();
        CreateMap<Tag, TagDto>();
    }
}