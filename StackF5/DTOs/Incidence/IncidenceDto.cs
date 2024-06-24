using StackF5.DTOs.Comment;
using DateTime = System.DateTime;

namespace StackF5.DTOs.Incidence;

public class IncidenceDto
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string? Photo { get; set; }
    
    //relations
    public List<CommentDto> Comments { get; set; }= new List<CommentDto>();
}