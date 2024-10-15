using Microsoft.EntityFrameworkCore;
using StackF5.Data;
using StackF5.Entity;

namespace StackF5.Repository.Incidence;

public class IncidenceRepository : IIncidenceRepository
{
    private readonly ApplicationDbContext _context;
    
    public IncidenceRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<Entity.Incidence>> GetAllIncidence()
    {
        return await _context.Incidences.Include(i=>i.Comments).ToListAsync();
    }
    
    public async Task<Entity.Incidence?> GetIncidenceById(int id)
    {
        return await _context.Incidences.FirstOrDefaultAsync(x=>x.Id == id);
    }
    
    public async Task<int> CreateIncidence(Entity.Incidence incidence)
    {
        incidence.CreatedAt = DateTime.Now;
        incidence.UpdatedAt = DateTime.Now;
        _context.Add(incidence);
        await _context.SaveChangesAsync();
        return incidence.Id;
    }
    
    public async Task<bool> ExistIncidence(int id)
    {
        return await _context.Incidences.AnyAsync(x=>x.Id == id);
    }
    
    public async Task UpdateIncidence(Entity.Incidence incidence)
    {
        incidence.UpdatedAt = DateTime.Now.Date;
        _context.Update(incidence);
        await _context.SaveChangesAsync();
    }
    
    public async Task DeleteIncidence(int id)
    {
        var incidence = await GetIncidenceById(id);
        if(incidence == null) return;
        _context.Remove(incidence);
        await _context.SaveChangesAsync();
    }
}