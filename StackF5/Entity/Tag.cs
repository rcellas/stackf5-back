namespace StackF5.Entity;

public class Tag
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    
    public List<InicidenceTag> IncidenceTags { get; set; } = new List<InicidenceTag>();
}