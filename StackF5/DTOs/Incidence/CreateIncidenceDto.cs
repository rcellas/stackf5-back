namespace StackF5.DTOs.Incidence;

public class CreateIncidenceDto
{
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string? Photo { get; set; }
}