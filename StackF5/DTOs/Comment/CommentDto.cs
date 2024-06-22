namespace StackF5.DTOs.Comment;

public class CommentDto
{
    public int Id { get; set; }
    public string Description { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string? Photo { get; set; }
    
    public int IncidenceId { get; set; }
}