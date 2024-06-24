namespace StackF5.DTOs.Comment;

public class CreateCommentDto
{
    public string Description { get; set; } = null!;
    public string? Photo { get; set; }
}