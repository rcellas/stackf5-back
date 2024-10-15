namespace StackF5.Entity;

public class Incidence
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    
    //relations
    public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    
    // public List<Image> Images { get; set; } = new List<Image>();
}