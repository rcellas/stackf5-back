namespace StackF5.DTOs.Comment;

public class CreateCommentDto
{
    public string Description { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string? Photo { get; set; }
}