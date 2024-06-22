namespace StackF5.Entity;

public class InicidenceTag
{
    public int IncidenceId { get; set; }
    public Incidence Incidence { get; set; } = null!;
    
    public int TagId { get; set; }
    public Tag Tag { get; set; } = null!;
}